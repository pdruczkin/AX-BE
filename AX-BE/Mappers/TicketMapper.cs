using AX_BE.Domain.Models;
using AX_BE.DTOs;
using Riok.Mapperly.Abstractions;

namespace AX_BE.Mappers;

[Mapper]
public partial class TicketMapper
{
    public partial TicketDto TicketToTicketDto(Ticket ticket);
    public partial List<TicketDto> TicketToTicketDto(List<Ticket> tickets);
    
    [MapperIgnoreTarget(nameof(Ticket.Id))]
    public partial Ticket CreateTicketDtoToTicket(CreateTicketDto createTicketDto);
    
}