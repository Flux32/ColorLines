using System.Collections.Generic;
using System.Threading;
using Balls.Source.View.GameBoard.Jobs;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard
{
    public interface IJobExecutor
    {
        UniTask Execute(CancellationToken cancellationToken = default, params IViewJob[] jobs);
    }
}