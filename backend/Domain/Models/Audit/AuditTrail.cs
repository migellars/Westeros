namespace Domain.Models.Audit;

public record AuditTrail
{
    public int Id { get; init; }
    public string? UserId { get; init; }
    public string? IpAddress { get; init; }
    public string? Type { get; init; }
    public string? TableName { get; init; }
    public DateTime DateTime { get; init; }
    public string? OldValues { get; init; }
    public string? NewValues { get; init; }
    public string? AffectedColumns { get; init; }
    public string? PrimaryKey { get; init; }
}