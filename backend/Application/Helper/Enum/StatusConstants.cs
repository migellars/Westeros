using SharedKernel.Resources.Enum;

namespace Application.Helper.Enum;

public class StatusConstants: Enumeration
{
    public StatusConstants(int id, string name) : base(id, name)
    {
    }

    public static StatusConstants Published = new StatusConstants(1, nameof(Published));
    public static StatusConstants Pending = new StatusConstants(2, nameof(Pending));
    public static StatusConstants Unpublished = new StatusConstants(3, nameof(Unpublished));
}