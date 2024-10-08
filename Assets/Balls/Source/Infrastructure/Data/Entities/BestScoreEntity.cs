using System;
using UnityEngine.Serialization;

namespace Balls.Source.Infrastructure.Data.Entities
{
    [Serializable]
    public class BestScoreEntity
    {
        [FormerlySerializedAs("Score")] public int Value;
        public DateTime Date;
    }
}
