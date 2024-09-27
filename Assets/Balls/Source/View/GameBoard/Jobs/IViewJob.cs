using System.Threading;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard.Jobs
{
    public interface IViewJob
    {
        public UniTask Execute(CancellationToken cancellationToken = default);
    }
}