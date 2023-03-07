using System;
using GameFolders.Scripts.Game.Components;
using GameFolders.Scripts.Game.General;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFolders.Scripts.Game.Player
{
    public class Bullet :  MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] private SpriteRenderer[] colorChangeableSprites;
        [SerializeField] private GameObject[] bodies;
        
        private PlayerData _playerData;
        private ColorData _colorData;
        private Rigidbody2D _rigidbody2D;
        private PlayerController _playerController;
        private PhotonView _photonView;

        private GameEnum.BelongsTo _belongsTo = GameEnum.BelongsTo.Enemy;

        private bool _isThrowing;
        private float _currentTime;

        private bool _isFiring;

        public bool IsFiring
        {
            get => _isFiring;
            set
            {
                _isFiring = value;
                SetActiveBodies(_isFiring);
            }
        }
        
        private void Awake()
        {
            _playerData = Resources.Load("Data/PlayerData") as PlayerData;
            _colorData = Resources.Load("Data/ColorData") as ColorData;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            SetBelongsTo(_photonView.IsMine);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamageable iDamageable))
            {
                if (iDamageable.BelongsTo != _belongsTo)
                {
                    if (!_photonView.IsMine)
                    {
                        iDamageable.TakeDamage(_playerData.Damage);
                    }
                    else
                    {
                        if (iDamageable.BelongsTo == GameEnum.BelongsTo.AI)
                        {
                            iDamageable.TakeDamage(_playerData.Damage);
                        }
                        ReturnToQueue();
                    }
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
            IsFiring = false;
            _isThrowing = false;
            _currentTime = 0;
            _rigidbody2D.velocity = Vector2.zero;
            
            if (_photonView.IsMine)
            {
                _playerController.AddQueue(this);
            }
        }
        
        private void SetActiveBodies(bool active)
        {
            foreach (GameObject body in bodies)
            {
                body.SetActive(active);
            }
        }

        public void Initiate(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void SetBelongsTo(bool isMine)
        {
            _belongsTo = isMine ? GameEnum.BelongsTo.Player : GameEnum.BelongsTo.Enemy;
            foreach (SpriteRenderer colorChangeableSprite in colorChangeableSprites)
            {
                colorChangeableSprite.color = isMine ? _colorData.PlayerBulletColor : _colorData.EnemyBulletColor;
            }
        }
        
        public void Throw(Vector2 startPosition, Vector2 direction)
        {
            _rigidbody2D.velocity = Vector2.zero;
            transform.position = startPosition;
            IsFiring = true;
            _rigidbody2D.AddForce(direction * _playerData.ForcePower);
            _isThrowing = true;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(IsFiring);
                stream.SendNext(transform.position);
            }
            else
            {
                // Network player, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
                this.transform.position = (Vector3)stream.ReceiveNext();
            }
        }
    }
}
