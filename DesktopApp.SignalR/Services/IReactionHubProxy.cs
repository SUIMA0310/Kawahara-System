using System.Threading.Tasks;
using DesktopApp.Models;

namespace DesktopApp.Services
{
    public interface IReactionHubProxy : IHubProxyService
    {
        Task<Result> AddListener(string presentationId);
        System.IObservable<(eReactionType, Color)> OnReceiveReaction();
        Task<Result> RemoveListener(string presentationId);
        Task SendFun(string presentationId, Color color);
        Task SendGood(string presentationId, Color color);
        Task SendNice(string presentationId, Color color);
        Task SendReaction(string presentationId, eReactionType reactionType, Color color);
    }
}