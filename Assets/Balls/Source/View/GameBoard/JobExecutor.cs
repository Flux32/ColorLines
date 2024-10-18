using System.Collections.Generic;
using System.Threading;
using Balls.Source.View.GameBoard.Jobs;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard
{
    public class JobExecutor : IJobExecuter
    {
        public async UniTask Execute(CancellationToken cancellationToken = default, params IViewJob[] jobs)
        {
            foreach (IViewJob job in jobs)
                await job.Execute(cancellationToken);
        }
    }
}