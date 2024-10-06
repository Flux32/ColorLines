using System;
using UnityEngine;

namespace Balls.Source.Infrastructure.Services.Input
{
    public interface IGameBoardInputService
    {
        event Action<Vector2> CursorMoved;
        event Action<Vector2> CursorPressed;
        event Action<Vector2> CursorReleased;
    }
}