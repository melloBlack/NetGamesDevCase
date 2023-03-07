using GameFolders.Scripts.Game.Components;
using GameFolders.Scripts.Game.General;
using GameFolders.Scripts.Game.Player;
using UnityEngine;

namespace GameFolders.Scripts.Game.AI
{
    public class AIBullet : MonoBehaviour
    {
        [SerializeField] private GameObject[] bodies;
    
        private AIController _aiController;
        private Rigidbody2D _rigidbody2D;
        private PlayerData _playerData;

        private bool _isThrowing;
        private float _currentTime;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerData = Resources.Load("Data/PlayerData") as PlayerData;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamageable iDamageable))
            {
                if (iDamageable.BelongsTo == GameEnum.BelongsTo.Player)
                {
                    ReturnToQueue();

                    iDamageable.TakeDamage(_playerData.Damage);
                }
            }
        }

        private void Update()
        {
            if (!_isThrowing) return;

            _currentTime += Time.deltaTime;

            if (!(_currentTime > _playerData.MaxBulletLifeTime)) return;

            ReturnToQueue();
        }

        private void ReturnToQueue()
        {
            _isThrowing = false;
            _currentTime = 0;
            _rigidbody2D.velocity = Vector2.zero;
            
            SetActiveBodies(false);

            _aiController.AddQueue(this);
        }

        private void SetActiveBodies(bool active)
        {
            foreach (GameObject body in bodies)
            {
                body.SetActive(active);
            }
        }
    
        public void Initiate(AIController aiController)
        {
            _aiController = aiController;
        }

        public void Throw(Vector2 startPosition)
        {
            _rigidbody2D.velocity = Vector2.zero;
            transform.position = startPosition;
            
            SetActiveBodies(true);
            
            _rigidbody2D.AddForce(Vector2.left * _playerData.ForcePower);
            _isThrowing = true;
        }
    }
}