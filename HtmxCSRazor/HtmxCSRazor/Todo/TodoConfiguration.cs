using System.ComponentModel.DataAnnotations;

namespace HtmxCSRazor.Todo;

public class TodoConfiguration
{
    [Required] public required string ConnectionStringPostgres { get; set; }
    [Required] public required string ConnectionStringMssql { get; set; }
}