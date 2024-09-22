using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balls.Infrastructure.LoadOperations
{
    public class SceneLoadOperation : ILoadOperation
    {
        private readonly string _sceneName;

        public SceneLoadOperation(OperationID operationID, string sceneName)
        {
            OperationID = operationID;
            _sceneName = sceneName;
        }

        public OperationID OperationID { get; private set; }
        public float Progress { get; private set; }

        public async UniTask Load(Action<OperationID, float> progressChanged)
        {
            Debug.Log("Niiice");

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_sceneName);
            await loadOperation;
            loadOperation.allowSceneActivation = true;
        }
    }
}