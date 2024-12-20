﻿using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balls.Source.Infrastructure.Services.Level
{
    public sealed class LevelService : ILevelService
    {
        private const string BootstrapSceneName = "Scene_Bootstrap";
        private const string GameplaySceneName = "Scene_Gameplay";
        
        public async UniTask LoadLevel(LevelId levelId)
        {
            Scene scene = SceneManager.GetActiveScene();

            string targetSceneName = GetSceneNameByID(levelId);

            if (levelId == LevelId.Bootstrap)
            {
                if (IsLevelExist(LevelId.Bootstrap))
                    throw new InvalidOperationException("Bootstrap is already exist");
                
                await SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
                return;
            }
            
            if (IsLevelExist(LevelId.Bootstrap) == false)
                throw new InvalidOperationException($"Cannot load level {levelId} because {BootstrapSceneName} doesn't exist");
                
            if (scene.name != BootstrapSceneName)
                await SceneManager.UnloadSceneAsync(scene);
        
            await SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetSceneName));
        }
        
        public bool IsLevelExist(LevelId levelId)
        {
            Scene bootstrapScene = SceneManager.GetSceneByName(GetSceneNameByID(levelId));

            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i) == bootstrapScene)
                    return true;
            }

            return false;
        }

        private string GetSceneNameByID(LevelId levelId)
        {
            return levelId switch
            {
                LevelId.Bootstrap => BootstrapSceneName,
                LevelId.Gameplay => GameplaySceneName,
                _ => throw new ArgumentOutOfRangeException(nameof(levelId))
            };
        }
    }
}