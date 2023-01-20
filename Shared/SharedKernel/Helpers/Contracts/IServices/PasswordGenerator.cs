namespace SharedKernel.Helpers.Contracts.IServices;

public class PasswordGenerator : IPasswordGenerator
{
    public string Generate(int length)
    {
        var strippedText = Guid.NewGuid().ToString().Replace("-", string.Empty);
        return strippedText[..length];
    }
}