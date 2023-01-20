namespace Application.Dto;

public class TokenDto
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public TimeSpan ExpiresIn { get; set; }
}