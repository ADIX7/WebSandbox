using System.Collections.Concurrent;
using Lib.AspNetCore.ServerSentEvents;

namespace HtmxCSRazor.Services;

public class MessageService(IServerSentEventsService serverSentEventsService)
{
    private readonly ConcurrentQueue<string> _messages = new();

    public async Task AddMessage(string message)
    {
        _messages.Enqueue(message);
        
        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-message",
            Type = "update-message",
            Data = new List<string> {""}
        });
    }

    public IEnumerable<string> GetMessages()
    {
        return _messages;
    }
}