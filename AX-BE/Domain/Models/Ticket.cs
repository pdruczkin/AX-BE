namespace AX_BE.Domain.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Status Status { get; set; } = Status.Open;
}