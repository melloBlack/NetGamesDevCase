using System;
using GameFolders.Scripts.Game.General;
using UnityEngine;

namespace GameFolders.Scripts.Game.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RotateWithKinematicRigidbody : MonoBehaviour
    {
        [SerializeField] private GameEnum.Direction direction;
        [SerializeField] private float speed;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody2D.angularVelocity = GetDirection() * speed;
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
