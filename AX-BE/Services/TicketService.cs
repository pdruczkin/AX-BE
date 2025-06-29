using AX_BE.Domain.Abstractions;
using AX_BE.Domain.Errors;
using AX_BE.Domain.Models;
using AX_BE.DTOs;
using AX_BE.Mappers;

namespace AX_BE.Services;

public class TicketService : ITicketService
{
    private readonly TicketMapper _mapper;
    
    private static readonly List<Ticket> Tickets =
        [new Ticket { Id = 1, Title = "Ticket 01", Description = "First ticket description.", Status = Status.Open }];

    public TicketService(TicketMapper mapper)
    {
        _mapper = mapper;
    }
    
    public Result<int> AddTicket(CreateTicketDto ticketDto)
    {
        var ticket = _mapper.CreateTicketDtoToTicket(ticketDto);
        
        if(ticket.Status == Status.Resolved) 
            return Result<int>.Failure(TicketErrors.TicketStatusResolved); 
        
        ticket.Id = Tickets.Count != 0 ? Tickets.Max(u => u.Id) + 1 : 1;
        
        Tickets.Add(ticket);
        
        return Result<int>.Success(ticket.Id);
    }

    public Result<bool> UpdateStatus(int id, UpdateTicketDto updateTicketDto)
    {
        var ticket = Tickets.FirstOrDefault(t => t.Id == id);

        if (ticket is null)
            return Result<bool>.Failure(TicketErrors.TicketNotFound);
        
        ticket.Status = updateTicketDto.Status;
        return Result<bool>.Success(true);
    }

    public Result<List<TicketDto>> GetAllTickets(Status? status = null)
    {
        var filteredTickets = status.HasValue 
            ? Tickets.Where(t => t.Status == status.Value).ToList() 
            : Tickets.ToList();

        var tickets = _mapper.TicketToTicketDto(filteredTickets);
    
        return Result<List<TicketDto>>.Success(tickets);
    }

    public Result<TicketDto> GetTicketById(int id)
    {
        var ticket = Tickets.FirstOrDefault(t => t.Id == id);

        if (ticket is null) 
            return Result<TicketDto>.Failure(TicketErrors.TicketNotFound);
        
        var ticketDto = _mapper.TicketToTicketDto(ticket);
        
        return Result<TicketDto>.Success(ticketDto);
    }
}