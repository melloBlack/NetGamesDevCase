using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.Components;
using GameFolders.Scripts.Game.General;
using GameFolders.Scripts.Game.Player;
using UnityEngine;

namespace GameFolders.Scripts.Game.AI
{
    public class AIHealthControl : MonoBehaviour, IDamageable
    {
        private EventData _eventData;
        private PlayerData _playerData;

        private int _health;

        public GameEnum.BelongsTo BelongsTo { get; set; }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;

                HealthBarController.Instance.UpdateEnemyHealth(Health);

                if (_health == 0)
                {
                    OnDead();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            Health = Mathf.Clamp(Health - damage, 0, _playerData.Health);
        }

        private void Awake()
        {
            _eventData = Resources.Load("Data/EventData") as EventData;
            _playerData = Resources.Load("Data/PlayerData") as PlayerData;
        }

        private void Start()
        {
            BelongsTo = GameEnum.BelongsTo.AI;
            Health = _playerData.Health;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamaging iDamaging))
            {
                TakeDamage(iDamaging.Damage);
                iDamaging.StopAction();
            }
        }

        private void OnDead()
        {
            _eventData.OnWinMatch?.Invoke();
        }
    }
}