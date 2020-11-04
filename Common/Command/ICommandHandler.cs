using System.Threading.Tasks;

namespace Common.Commands
{
    public interface ICommandHandle<in T> where T: ICommand
    {
        Task HandleAsync(T command);
    }
}