using System;
using GameFolders.Scripts.Game.General;
using UnityEngine;

namespace GameFolders.Scripts.Game.Components
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private Space space;
        [SerializeField] private GameEnum.Direction direction;
        [SerializeField] private float speed;
        
        private void Update()
        {
            transform.Rotate(Vector3.back * (GetDirection() * speed * Time.deltaTime), space);
        }

        private int GetDirection()
        {
            return direction switch
            {
                GameEnum.Direction.Right => 1,
                GameEnum.Direction.Left => -1,
                _ => 1
            };
        }
    }
}
