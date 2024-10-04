using Cysharp.Threading.Tasks;

namespace Balls.Source.Infrastructure.Services.Level
{
    public interface ILevelService
    {
        UniTask LoadLevel(LevelId levelId);
    }
}