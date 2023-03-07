using System;
using System.Collections;
using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.General;
using GameFolders.Scripts.Game.Skills;
using GameFolders.Scripts.Skills;
using Mono.Cecil;
using Photon.Pun;
using UnityEngine;

namespace GameFolders.Scripts.Game.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Vector2 xBorders;
        [SerializeField] private Vector2 yBorders;
        
        private EventData _eventData;
        private PlayerData _playerData;
        private Rigidbody2D _rigidbody2D;
        private PhotonView _photonView;
        private Vector2 _inputDirection;

        private float _speed;
    
        private void Awake()
        {
            _eventData = Resources.Load("Data/EventData") as EventData;
            _playerData = Resources.Load("Data/PlayerData") as PlayerData;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _photonView = GetComponent<PhotonView>();
        }

        private void OnEnable()
        {
            if (!_photonView.IsMine) return;

            _eventData.OnClickedSpeedUpButton += SpeedUp;
        }

        private void Start()
        {
            _speed = _playerData.MoveSpeed;
        }

        private void FixedUpdate()
        {
            if (!_photonView.IsMine) return;

            if (_rigidbody2D.position.x >= xBorders.y && _inputDirection.x > 0 || _rigidbody2D.position.x <= xBorders.x && _inputDirection.x < 0)
            {
                _inputDirection.x = 0;
            }

            if (_rigidbody2D.position.y >= yBorders.y && _inputDirection.y > 0 || _rigidbody2D.position.y <= yBorders.x && _inputDirection.y < 0)
            {
                _inputDirection.y = 0;
            }
            _rigidbody2D.MovePosition(_rigidbody2D.position + _inputDirection * (_speed * Time.deltaTime));
        }

        private void Update()
        {
            if (!_photonView.IsMine) return;

            _inputDirection = GetDirection();
        }

        private void OnDisable()
        {
            if (!_photonView.IsMine) return;

            _eventData.OnClickedSpeedUpButton -= SpeedUp;
        }

        private void SpeedUp()
        {
            if (!_photonView.IsMine) return;

            StartCoroutine(SpeedUpCoroutine());
        }

        private Vector2 GetDirection()
        {
            return _playerData.InputType switch
            {
                GameEnum.InputType.Joystick => InputManager.Instance.GetDirection(),
                GameEnum.InputType.Keyboard => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")),
                _ => Vector2.zero
            };
        }

        private IEnumerator SpeedUpCoroutine()
        {
            _speed = _playerData.SpeedUpSpeed;

            yield return new WaitForSeconds(_playerData.SpeedUpTime);

            _speed = _playerData.MoveSpeed;
        }
    }
}
