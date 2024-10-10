using Cysharp.Threading.Tasks;
using System.Threading;

namespace Balls.Source
{
    public interface ILevelBootstrapper
    {
        public UniTask Bootstrap(CancellationToken token = default);
    }
}