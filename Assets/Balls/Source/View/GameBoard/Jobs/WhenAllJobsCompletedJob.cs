using System.Threading;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard.Jobs
{
    public class WhenAllJobsCompletedJob : IViewJob
    {
        private readonly IViewJob[] _jobs;

        public WhenAllJobsCompletedJob(params IViewJob[] jobs)
        {
            _jobs = jobs;
        }
        
        public UniTask Execute(CancellationToken cancellationToken)
        {
            UniTask[] tasks = new UniTask[_jobs.Length];

            for (int i = 0; i < _jobs.Length; i++)
                tasks[i] = _jobs[i].Execute(cancellationToken);

            return UniTask.WhenAll(tasks).AttachExternalCancellation(cancellationToken);
        }
    }
}