namespace Api.Payload;

public class UpdateLessonVm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
}