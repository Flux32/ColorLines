using Cysharp.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Balls.Source.Infrastructure.Data.Storages
{
    public class PlayerPrefsJsonStorage : IDataStorage
    {
        public UniTask Save<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, json);
            return UniTask.CompletedTask;
        }

        public UniTask<T> Load<T>(string key)
        {
            return UniTask.FromResult(JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key)));
        }
    }

    public interface IDataStorage
    {
        UniTask Save<T>(string key, T value);
        UniTask<T> Load<T>(string key);
    }
}