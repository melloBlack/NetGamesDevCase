using System;
using System.Collections.Generic;
using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.Managers;
using GameFolders.Scripts.Game.Player;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Game.AI
{
    public class AIController : MonoBehaviourPunCallbacks
    {    
        [SerializeField] private AIBullet bulletPrefab;
        [SerializeField] private Vector2 attackTimeRange; 

        private readonly Queue<AIBullet> _bullets = new Queue<AIBullet>();

        private EventData _eventData;
        private PlayerData _playerData;
        private SpriteRenderer _spriteRenderer;
        private GameObject _bulletsParent;

        private float _currentTime;
        private float _currentAttackTime;
        private bool _canAttack;

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            _eventData.OnResetMatch?.Invoke();
            foreach (AIBullet aiBullet in _bullets)
            {
                Destroy(aiBullet);
            }
            Destroy(gameObject);
        }

        private void Awake()
        {
            _eventData = Resources.Load("Data/EventData") as EventData;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _eventData.OnLoseMatch += OnFinishGame;
            _eventData.OnWinMatch += OnFinishGame;
        }

        private void Start()
        {
            if (!GameManager.Instance.RoomOwner)
            {
                _eventData.OnResetMatch?.Invoke();
                
                foreach (AIBullet aiBullet in _bullets)
                {
                    Destroy(aiBullet);
                }
                
                Destroy(gameObject);
            }

            GameController.Instance.EnemyTransform = transform;

            _currentAttackTime = Random.Range(attackTimeRange.x, attackTimeRange.y);
            _canAttack = true;
            _bulletsParent = new GameObject
            {
                name = "AIBullets",
            };
        }

        private void Update()
        {
            if (!_canAttack) return;
                
            _currentTime += Time.deltaTime;

            if (!(_currentTime >= _currentAttackTime)) return;
            
            _currentTime = 0;
            _currentAttackTime = Random.Range(attackTimeRange.x, attackTimeRange.y);
            Attack();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _eventData.OnLoseMatch -= OnFinishGame;
            _eventData.OnWinMatch -= OnFinishGame;
        }

        private void Attack()
        {
            if (_bullets.Count == 0)
            {
                AIBullet newBullet = Instantiate(bulletPrefab, transform.position + Vector3.left, Quaternion.identity);
                newBullet.transform.parent = _bulletsParent.transform;
                newBullet.Initiate(this);
                _bullets.Enqueue(newBullet);
            }

            _bullets.Dequeue().Throw((Vector2)transform.position + Vector2.left);
        }

        private void OnFinishGame()
        {
            _canAttack = false;
        }

        public void AddQueue(AIBullet bullet)
        {
            _bullets.Enqueue(bullet);
        }
    }
}
