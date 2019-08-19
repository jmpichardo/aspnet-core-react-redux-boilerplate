using Renetdux.Infrastructure.Commands.Users.Models;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Interfaces
{
    public interface IGenerateJwtCommand
    {
        Task<ICommandResult<TokenDTO>> ExecuteAsync(string email, string password);
        Task<ICommandResult<TokenDTO>> ExecuteAsync(string refreshToken);
    }
}
