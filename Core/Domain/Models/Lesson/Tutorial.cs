using Domain.Models.Authors;

namespace Domain.Models.Lesson;

public class Tutorial: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public Guid? AuthorId { get; set; }
    public virtual Author Author { get; set; }
}