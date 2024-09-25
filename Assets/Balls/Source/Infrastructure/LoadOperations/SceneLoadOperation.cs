using System;
using Balls.Infrastructure.LoadOperations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balls.Source.Infrastructure.LoadOperations
{
    public sealed class SceneLoadOperation : ILoadOperation
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
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_sceneName);
            await loadOperation;
            loadOperation.allowSceneActivation = true;
        }
    }
}