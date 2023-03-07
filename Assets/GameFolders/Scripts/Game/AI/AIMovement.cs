using System;
using GameFolders.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Game.AI
{
    public class AIMovement : MonoBehaviour
    {
        [Header("Borders")]
        [SerializeField] private Vector2 xBorders;
        [SerializeField] private Vector2 yBorders;
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed;

        private EventData _eventData;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _currentMovePoint;
        private bool _canMove;
        
        private void Awake()
        {
            _eventData = Resources.Load("Data/EventData") as EventData;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _eventData.OnWinMatch += OnFinishGame;
            _eventData.OnLoseMatch += OnFinishGame;
        }

        private void Start()
        {
            _canMove = true;
            _currentMovePoint = GetNewRandomPoint();
        }

        private void FixedUpdate()
        {
            if(!_canMove) return;
            
            Vector2 direction = (_currentMovePoint - transform.position).normalized;
            
            _rigidbody2D.MovePosition(_rigidbody2D.position + direction * (moveSpeed * Time.deltaTime));
        }

        private void Update()
        {
            if(!_canMove) return;

            if (Vector3.Distance(transform.position, _currentMovePoint) < 0.1f)
            {
                _currentMovePoint = GetNewRandomPoint();
            }
        }

        private void OnDisable()
        {
            _eventData.OnWinMatch -= OnFinishGame;
            _eventData.OnLoseMatch -= OnFinishGame;
        }

        private Vector3 GetNewRandomPoint()
        {
            float x = Random.Range(xBorders.x, xBorders.y);
            float y = Random.Range(yBorders.x, yBorders.y);

            return new Vector3(x, y, 0);
        }

        private void OnFinishGame()
        {
            _canMove = false;
        }
    }
}
