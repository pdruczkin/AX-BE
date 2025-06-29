using AX_BE.Domain.Abstractions;

namespace AX_BE.Domain.Errors;

public static class TicketErrors
{
    public static readonly Error TicketNotFound = new("Ticket.NotFound", "Can't find ticket");
    public static readonly Error TicketStatusResolved = new("Ticket.StatusResolved", "Ticket can't be created with status resolved");
}