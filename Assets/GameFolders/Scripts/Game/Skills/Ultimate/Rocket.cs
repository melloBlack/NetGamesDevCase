using System;
using GameFolders.Scripts.Game.Components;
using GameFolders.Scripts.Game.General;
using GameFolders.Scripts.Game.Managers;
using GameFolders.Scripts.Game.Player;
using Photon.Pun;
using UnityEngine;

namespace GameFolders.Scripts.Game.Skills.Ultimate
{
    public class Rocket : UltimateSkill, IDamaging
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private int damage = 50;

        private Rigidbody2D _rigidbody2D;
        private Vector3 _startPosition;
        
        public int Damage { get; set; }
        
        protected override void Awake()
        {
            base.Awake();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Damage = damage;
            BelongsTo = _photonView.IsMine ? GameEnum.BelongsTo.Player : GameEnum.BelongsTo.Enemy;
        }

        private void Update()
        {
            Vector3 targetPosition = _photonView.IsMine ? GameController.Instance.EnemyTransform.position : GameController.Instance.PlayerTransform.position;

            float distance = Vector3.Distance(transform.position, targetPosition);
            
            if (distance > 0.5f)
            {
                targetPosition.x = targetPosition.x - transform.position.x;
                targetPosition.y = targetPosition.y - transform.position.y;
                float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            
            if (!_photonView.IsMine) return;

            float step =  moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, GameController.Instance.EnemyTransform.position, step);
        }

        [PunRPC]
        private void Dead()
        {
            if (_photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        
        public void StopAction()
        {
            _photonView.RPC("Dead", RpcTarget.All);
        }
        
        public override void Play(Vector2 startPosition)
        {
            transform.position = startPosition;
        }
    }
}
