using System;
using System.Collections.Generic;
using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.General;
using GameFolders.Scripts.Game.Managers;
using GameFolders.Scripts.Skills;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace GameFolders.Scripts.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;

        private readonly Queue<Bullet> _bullets = new Queue<Bullet>();

        private EventData _eventData;
        private ColorData _colorData;
        private PlayerData _playerData;
        private PhotonView _photonView;
        private SpriteRenderer _spriteRenderer;
        private GameObject _bulletsParent;

        private void Awake()
        {
            _eventData = Resources.Load("Data/EventData") as EventData;
            _colorData = Resources.Load("Data/ColorData") as ColorData;
            _playerData = Resources.Load("Data/PlayerData") as PlayerData;
            _photonView = GetComponent<PhotonView>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _eventData.OnClickedAttackButton += Attack;
        }

        private void Start()
        {
            _spriteRenderer.color = _photonView.IsMine ? _colorData.PlayerColor : _colorData.EnemyColor;
            _bulletsParent = new GameObject
            {
                name = "Bullets",
            };

            if (!_photonView.IsMine)
            {
                GameController.Instance.EnemyTransform = transform;
            }
            else
            {
                GameController.Instance.PlayerTransform = transform;
            }
            
            GetComponent<HealthControl>().SetBelongsTo(_photonView.IsMine);
        }

        private void OnDisable()
        {
            _eventData.OnClickedAttackButton -= Attack;
        }

        private void Attack()
        {
            if (!_photonView.IsMine) return;

            if (_bullets.Count == 0)
            {
                GameObject newBulletObject = PhotonNetwork.Instantiate("Bullet", transform.position + (Vector3)GetDirection(), Quaternion.identity);
                Bullet newBullet = newBulletObject.GetComponent<Bullet>();
                newBullet.transform.parent = _bulletsParent.transform;
                newBullet.Initiate(this);
                _bullets.Enqueue(newBullet);
            }

            _bullets.Dequeue().Throw((Vector2)transform.position + GetDirection(), GetDirection());
        }
        
        private Vector2 GetDirection()
        {
            return GameManager.Instance.RoomOwner ? Vector3.right : Vector3.left;
        }

        public void AddQueue(Bullet bullet)
        {
            _bullets.Enqueue(bullet);
        }
    }
}
