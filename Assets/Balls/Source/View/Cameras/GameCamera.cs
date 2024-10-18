using Balls.Source.View.GameBoard;
using Cinemachine;
using Reflex.Attributes;
using UnityEngine;

namespace Balls.Source.View.Cameras
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private float _orthoOffset;
        [SerializeField] private CinemachineVirtualCamera _gameBoardCamera;
        
        private Camera _camera;
        private BoardView _gameBoardView;

        [Inject]
        private void Constructor(Camera mainCamera, BoardView gameBoardView)
        {
            _camera = mainCamera;
            _gameBoardView = gameBoardView;
        }
        
        public void Fit()
        {
            Vector3 cameraCenter = _gameBoardView.Grid.Bounds.center;
            cameraCenter.z = -100;
            _gameBoardCamera.transform.position = cameraCenter;
            
            Vector3 gameBoardSize = _gameBoardView.Grid.Bounds.size;
            _gameBoardCamera.m_Lens.OrthographicSize = _orthoOffset + GetOrthoSizeByWidthAndHeight(gameBoardSize.x, gameBoardSize.y);
        }

        private float GetOrthoSizeByWidthAndHeight(float width, float height)
        {
            if (_camera.aspect >= 1)
                return GetOrthoSizeByHeight(height);

            return GetOrthoSizeByWidth(width);
        }
        
        private float GetOrthoSizeByWidth(float width)
        {
            return width / (2f * _camera.aspect);
        }

        private float GetOrthoSizeByHeight(float height)
        {
            return height / 2;
        }
    }
}