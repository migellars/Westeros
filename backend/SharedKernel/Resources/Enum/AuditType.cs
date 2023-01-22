namespace SharedKernel.Resources.Enum;

public class AuditType: Enumeration
{
    private AuditType(int id, string name) : base(id, name)
    {
    }
    public static AuditType None = new(-1, nameof(None));
    public static AuditType Create = new(0, nameof(Create));
    public static AuditType Update = new(1, nameof(Update));
    public static AuditType Delete = new(2, nameof(Delete));
}