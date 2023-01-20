namespace Application.Dto;

public class TutorialDto
{
    protected TutorialDto(string name, string description, int status)
    {
        Name = name;
        Description = description;
        Status = status;
    }

    public TutorialDto()
    {
        
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
}

