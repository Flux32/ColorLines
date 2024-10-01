using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Balls.Source.Infrastructure.Storages
{
    public class PlayerPrefsJsonStorage
    {
        public void Save<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, json);
        }

        public T Load<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
        }
    }
}