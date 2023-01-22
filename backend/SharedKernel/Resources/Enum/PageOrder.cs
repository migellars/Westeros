namespace SharedKernel.Resources.Enum
{
    public class PageOrder: Enumeration
    {
        public static StatusEnum ASC = new(-1, nameof(ASC));
        public static StatusEnum DESC = new(-1, nameof(DESC));

        public PageOrder(int id, string name) : base(id, name)
        {
        }
    }
}