using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISendGridEmailService
    {
        Task SendAuthCodeAsync(string to, string username, int code);
        Task SendResetPasswordEmailAsync(string email, string userName, string token);
    }
}