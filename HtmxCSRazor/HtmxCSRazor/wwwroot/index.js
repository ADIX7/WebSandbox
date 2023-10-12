class TodoItem extends HTMLElement {

    // connect component
    connectedCallback() {
        console.log('TodoItem connectedCallback');
        
        this.isDisplayVisible = true;

        const displayContainer = document.createElement('div');
        displayContainer.id = 'displayContainer';
        displayContainer.innerHTML = `<slot name="display"></slot>`;

        const editContainer = document.createElement('div');
        editContainer.id = 'editContainer';
        editContainer.style.display = 'none';
        editContainer.innerHTML = `<slot name="edit"></slot>`;

        const shadowRoot = this.attachShadow({mode: "open"});
        shadowRoot.appendChild(displayContainer);
        shadowRoot.appendChild(editContainer);

        this.addEventListener('toggleTodoState', function (e) {
            e.stopPropagation();
            
            console.log('toggleTodoState', this.isDisplayVisible, e)
            
            if (this.isDisplayVisible) {
                displayContainer.style.display = 'none';
                editContainer.style.display = 'block';
            } else {
                displayContainer.style.display = 'block';
                editContainer.style.display = 'none';
            }
            this.isDisplayVisible = !this.isDisplayVisible;
        });
    }

}

customElements.define('todo-item', TodoItem);