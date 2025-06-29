using AX_BE.Domain.Errors;
using AX_BE.Domain.Models;
using AX_BE.DTOs;
using AX_BE.Mappers;
using AX_BE.Services;

namespace AX_BETests.Services;

public class TicketServiceTests
{
    private readonly TicketService _service;

    public TicketServiceTests()
    {
        var mapper = new TicketMapper();
        _service = new TicketService(mapper);
    }

    [Fact]
    public void AddTicket_WithValidData_ShouldAssignIdAndAddTicket()
    {
        var createDto = new CreateTicketDto
        {
            Title = "New Ticket",
            Description = "Test Description",
            Status = Status.Open
        };

        var result = _service.AddTicket(createDto);

        Assert.True(result.IsSuccess);
        Assert.True(result.Data > 0);
    }

    [Fact]
    public void AddTicket_WithResolvedStatus_ShouldFail()
    {
        var createDto = new CreateTicketDto
        {
            Title = "New Ticket",
            Description = "Test Description",
            Status = Status.Resolved
        };

        var result = _service.AddTicket(createDto);

        Assert.False(result.IsSuccess);
        Assert.Equal(TicketErrors.TicketStatusResolved, result.Error);
    }

    [Fact]
    public void GetTicketById_WhenTicketExists_ShouldReturnTicketDto()
    {
        var createDto = new CreateTicketDto
        {
            Title = "Sample Ticket",
            Description = "Desc",
            Status = Status.Open
        };

        var addResult = _service.AddTicket(createDto);

        var getResult = _service.GetTicketById(addResult.Data);

        Assert.True(getResult.IsSuccess);
        Assert.NotNull(getResult.Data);
        Assert.Equal(createDto.Title, getResult.Data.Title);
    }

    [Fact]
    public void GetTicketById_WhenTicketDoesNotExist_ShouldFail()
    {
        var getResult = _service.GetTicketById(999);

        Assert.False(getResult.IsSuccess);
        Assert.Equal(TicketErrors.TicketNotFound, getResult.Error);
    }

    [Fact]
    public void UpdateStatus_WithValidTicket_ShouldUpdateStatus()
    {
        var createDto = new CreateTicketDto
        {
            Title = "Ticket for Update",
            Description = "Desc",
            Status = Status.Open
        };

        var addResult = _service.AddTicket(createDto);

        var updateDto = new UpdateTicketDto
        {
            Status = Status.Resolved
        };
        
        var updateResult = _service.UpdateStatus(addResult.Data, updateDto);

        Assert.True(updateResult.IsSuccess);

        var getResult = _service.GetTicketById(addResult.Data);
        Assert.NotNull(getResult.Data);
        Assert.Equal(Status.Resolved, getResult.Data.Status);
    }

    [Fact]
    public void UpdateStatus_WhenTicketDoesNotExist_ShouldFail()
    {
        var updateDto = new UpdateTicketDto
        {
            Status = Status.Resolved
        };
        
        var updateResult = _service.UpdateStatus(999, updateDto);

        Assert.False(updateResult.IsSuccess);
        Assert.Equal(TicketErrors.TicketNotFound, updateResult.Error);
    }

    [Fact]
    public void GetAllTickets_ForNoFilter_ShouldReturnAll()
    {
        var createDto1 = new CreateTicketDto { Title = "T1", Description = "D1", Status = Status.Open };
        var createDto2 = new CreateTicketDto { Title = "T2", Description = "D2", Status = Status.InProgress };

        _service.AddTicket(createDto1);
        _service.AddTicket(createDto2);

        var result = _service.GetAllTickets();

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Count >= 2); 
    }

    [Fact]
    public void GetAllTickets_WithStatusFilter_ShouldReturnMatchingTickets()
    {
        var createDto1 = new CreateTicketDto { Title = "T1", Description = "D1", Status = Status.Open };
        var createDto2 = new CreateTicketDto { Title = "T2", Description = "D2", Status = Status.InProgress };

        _service.AddTicket(createDto1);
        _service.AddTicket(createDto2);

        var result = _service.GetAllTickets(Status.Open);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.All(result.Data, t => Assert.Equal(Status.Open, t.Status));
    }
}