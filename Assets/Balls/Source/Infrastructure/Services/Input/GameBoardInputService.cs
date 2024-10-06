using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Balls.Source.Infrastructure.Services.Input
{
    public class GameBoardInputService : IDisposable, IGameBoardInputService
    {
        private readonly PlayerInput _playerInput;
        private readonly Camera _camera;
        
        public GameBoardInputService(PlayerInput playerInput, Camera camera)
        {
            _playerInput = playerInput;
            _camera = camera;
            
            _playerInput.GameBoard.Cursor.performed += OnCursorMoved;
            _playerInput.GameBoard.CursorPress.started += OnCursorPressed; 
            _playerInput.GameBoard.CursorPress.performed += OnCursorReleased;
            
            _playerInput.Enable();
        }

        public event Action<Vector2> CursorMoved;
        public event Action<Vector2> CursorPressed;
        public event Action<Vector2> CursorReleased;

        public void Dispose()
        {
            _playerInput.GameBoard.Cursor.performed -= OnCursorMoved;
            _playerInput.GameBoard.CursorPress.started -= OnCursorPressed; 
            _playerInput.GameBoard.CursorPress.performed -= OnCursorReleased; 
            
            _playerInput.Disable();
        }
        
        private void OnCursorPressed(InputAction.CallbackContext context)
        {
            CursorPressed?.Invoke(GetCursorWorldPosition());
        }

        private void OnCursorReleased(InputAction.CallbackContext context)
        {
            CursorReleased?.Invoke(GetCursorWorldPosition());
        }

        private void OnCursorMoved(InputAction.CallbackContext context)
        {
            CursorMoved?.Invoke(GetCursorWorldPosition());
        }
        
        private Vector2 GetCursorWorldPosition()
        {
            Vector2 screenPosition = _playerInput.GameBoard.Cursor.ReadValue<Vector2>();
            return _camera.ScreenToWorldPoint(screenPosition);
        }
    }
}