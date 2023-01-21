namespace SharedKernel.Resources.JwtAuthenticationManager.Models;

public class TokenClaims
{
    public Guid UserId { get; set; }
    public string Picture { get; set; }
    public string Username { get; set; }   
    public string UserType { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
}