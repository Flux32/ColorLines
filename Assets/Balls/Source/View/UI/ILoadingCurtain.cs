using Cysharp.Threading.Tasks;
using System.Threading;

namespace Balls.View.UI
{
    public interface ILoadingCurtain
    {
        UniTask Close(CancellationToken cancellationToken);
        UniTask Open(CancellationToken cancellationToken);
        void SetOpenedState();
    }
}