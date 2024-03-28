namespace Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int id, string userName,string role);
    }
}