using System.Threading.Tasks;
using Common.Commands;

namespace Common.Events
{
    public interface IEventHandler <in T>where T : IEvent
    {
        Task HandleASync(T @event);
    }
}