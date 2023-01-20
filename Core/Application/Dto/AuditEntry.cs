using Domain.Models.Audit;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using SharedKernel.Helpers.Enum;

namespace Application.Dto;

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }
    public EntityEntry Entry { get; }
    public string? UserId { get; set; }
    public string? IpAddress { get; set; }
    public string? TableName { get; set; }
    public Dictionary<string, object?> KeyValues { get; } = new Dictionary<string, object?>();
    public Dictionary<string, object?> OldValues { get; } = new Dictionary<string, object?>();
    public Dictionary<string, object?> NewValues { get; } = new Dictionary<string, object?>();
    public AuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    public AuditTrail ToAudit()
    {
        var audit = new AuditTrail
        {
            UserId = UserId,
            Type = AuditType.ToString(),
            TableName = TableName,
            DateTime = DateTime.Now,
            PrimaryKey = JsonConvert.SerializeObject(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
            AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns)
        };
        return audit;
    }
}