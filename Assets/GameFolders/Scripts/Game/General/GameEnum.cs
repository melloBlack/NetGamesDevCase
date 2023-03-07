using UnityEngine;

namespace GameFolders.Scripts.Game.General
{
    public static class GameEnum
    {
        public enum InputType
        {
            Joystick,
            Keyboard
        }
        public enum BelongsTo
        {
            Player,
            Enemy,
            AI
        }
        
        public enum Direction
        {
            Right,
            Left
        }
    }
}
