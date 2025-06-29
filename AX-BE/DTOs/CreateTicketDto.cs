using AX_BE.Domain.Models;

namespace AX_BE.DTOs;

public class CreateTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Status Status { get; set; } = Status.Open;
}