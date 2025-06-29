namespace AX_BE.Domain.Abstractions;

public record Error(string Code, string? Message = null)
{
    public static readonly Error None = new(string.Empty);
}