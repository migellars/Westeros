namespace SharedKernel.Resources.Enum;

public class StatusEnum: Enumeration
{
    public StatusEnum(int id, string name) : base(id, name)
    {
    }
    public static StatusEnum Failed = new(-1, nameof(Failed));
    public static StatusEnum Pending = new(0, nameof(Pending));
    public static StatusEnum Success = new(1, nameof(Success));
    public static StatusEnum Approved = new(2, nameof(Approved));
    public static StatusEnum Treated = new(3, nameof(Treated));
}