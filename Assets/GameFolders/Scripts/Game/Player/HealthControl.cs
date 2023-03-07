using System;
using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.Components;
using GameFolders.Scripts.Game.General;
using GameFolders.Scripts.Game.Skills.Ultimate;
using Photon.Pun;
using UnityEngine;

namespace GameFolders.Scripts.Game.Player
{
    public class HealthControl : MonoBehaviourPunCallbacks, IDamageable, IPunObservable
    {
        private PhotonView _photonView;
        private PlayerData _playerData;
        private EventData _eventData;
        
        private int _health;
        
        public GameEnum.BelongsTo BelongsTo { get; set; }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                
                switch (BelongsTo)
                {
                    case GameEnum.BelongsTo.Player:
                        HealthBarController.Instance.UpdatePlayerHealth(Health);
                        break;
                    case GameEnum.BelongsTo.Enemy:
                        HealthBarController.Instance.UpdateEnemyHealth(Health);
                        break;
                    default:
                        break;
                }

                if (_health == 0)
                {
                    OnDead();
                }
            }
        }

        private void Awake()
        {
            _playerData = Resources.Load("Data/PlayerData") as PlayerData;
            _eventData = Resources.Load("Data/EventData") as EventData;
            _photonView = GetComponent<PhotonView>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _eventData.OnResetMatch += OnResetMatch;
        }

        private void Start()
        {
            Health = _playerData.Health;
            HealthBarController.Instance.SetMaxHealths(Health);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_photonView.IsMine) return;
 
            if (col.TryGetComponent(out IDamaging iDamaging))
            {
                if (BelongsTo != iDamaging.BelongsTo)
                {
                    TakeDamage(iDamaging.Damage);
                    iDamaging.StopAction();
                }
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _eventData.OnResetMatch -= OnResetMatch;
        }

        private void OnResetMatch()
        {
            Health = _playerData.Health;
            switch (BelongsTo)
            {
                case GameEnum.BelongsTo.Player:
                    HealthBarController.Instance.UpdatePlayerHealth(Health);
                    break;
                case GameEnum.BelongsTo.Enemy:
                    HealthBarController.Instance.UpdateEnemyHealth(Health);
                    break;
                default:
                    break;
            }
        }
        
        private void OnDead()
        {
            if (BelongsTo == GameEnum.BelongsTo.Player)
            {
                _eventData.OnLoseMatch?.Invoke();
            }
            else
            {
                _eventData.OnWinMatch?.Invoke();
            }
        }

        public void SetBelongsTo(bool isMine)
        {
            BelongsTo = isMine ? GameEnum.BelongsTo.Player : GameEnum.BelongsTo.Enemy;
        }

        public void TakeDamage(int damage)
        {
            Health = Health = Mathf.Clamp(Health - damage, 0, _playerData.Health);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(Health);
            }
            else
            {
                // Network player, receive data
                this.Health = (int)stream.ReceiveNext();
            }
        }
    }
}


// Damaegi ben almalıyım isMine player almalı