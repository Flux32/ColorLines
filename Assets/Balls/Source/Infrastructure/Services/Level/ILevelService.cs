using Cysharp.Threading.Tasks;

namespace Balls.Source.Infrastructure.Services.Level
{
    public interface ILevelService
    {
        bool IsLevelExist(LevelId levelId);
        UniTask LoadLevel(LevelId levelId);
    }
}