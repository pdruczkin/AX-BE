using AX_BE.Domain.Models;
using AX_BE.DTOs;
using AX_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace AX_BE.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    
    [HttpPost]
    public IResult AddTicket([FromBody] CreateTicketDto createTicketDto)
    {
        var result = _ticketService.AddTicket(createTicketDto);

        return result.IsSuccess switch
        {
            true => Results.Ok(result.Data),
            _ => Results.BadRequest(result.Error.Message)
        };
    }
    
    [HttpPut("{id:int}/status")]
    public IResult UpdateTicketStatus(int id, [FromBody] UpdateTicketDto updateTicketDto)
    {
        var result = _ticketService.UpdateStatus(id, updateTicketDto);

        return result.IsSuccess switch
        {
            true => Results.Ok(result.Data),
            _ => Results.BadRequest(result.Error.Message)
        };
    }
    
    [HttpGet("{id:int}")]
    public IResult GetTicketById(int id)
    {
        var result = _ticketService.GetTicketById(id);

        return result.IsSuccess switch
        {
            true => Results.Ok(result.Data),
            _ => Results.BadRequest(result.Error.Message)
        };
    }
    
    [HttpGet]
    public IResult GetAllTickets([FromQuery] Status? status = null)
    {
        var result = _ticketService.GetAllTickets(status);

        return result.IsSuccess switch
        {
            true => Results.Ok(result.Data),
            _ => Results.BadRequest(result.Error.Message)
        };
    }
}