namespace SharedKernel.Resources.Contracts.IServices;

public interface IPasswordGenerator 
{
    string Generate(int length = 6);
}