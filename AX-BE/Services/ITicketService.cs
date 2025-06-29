using AX_BE.Domain.Abstractions;
using AX_BE.Domain.Models;
using AX_BE.DTOs;

namespace AX_BE.Services;

public interface ITicketService
{
    Result<int> AddTicket(CreateTicketDto ticketDto);
    Result<bool> UpdateStatus(int id, UpdateTicketDto updateTicketDto);
    Result<List<TicketDto>> GetAllTickets(Status? status = null);
    Result<TicketDto> GetTicketById(int id);
}