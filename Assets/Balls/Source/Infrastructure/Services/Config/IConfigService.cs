using Balls.Source.Logic.Score;
using Cysharp.Threading.Tasks;

namespace Balls.Source.Infrastructure.Services.Config
{
    public interface IConfigService
    {
        public GameBoardSettings GameBoardSettings { get; }
        public ScoreSettings ScoreSettings { get; }

        public UniTask Load();
    }
}