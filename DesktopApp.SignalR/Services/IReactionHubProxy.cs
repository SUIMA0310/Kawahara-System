using System;
using System.Threading.Tasks;

using DesktopApp.Models;

namespace DesktopApp.Services
{
    public interface IReactionHubProxy : IHubProxyService
    {
        string PresentationID { get; set; }

        event Action<string, string> PresentationIDChanged;

        System.IObservable<(eReactionType, Color)> OnReceiveReaction();

        Task<Result> AddListener();

        Task<Result> AddListener( string presentationId );

        Task<Result> RemoveListener();

        Task<Result> RemoveListener( string presentationId );

        Task SendFun( Color color );

        Task SendFun( string presentationId, Color color );

        Task SendGood( Color color );

        Task SendGood( string presentationId, Color color );

        Task SendNice( Color color );

        Task SendNice( string presentationId, Color color );

        Task SendReaction( eReactionType reactionType, Color color );

        Task SendReaction( string presentationId, eReactionType reactionType, Color color );
    }
}