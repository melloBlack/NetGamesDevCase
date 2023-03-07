using System;
using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.Player;
using GameFolders.Scripts.Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Game.Components
{
    public class HealthBarController : MonoSingleton<HealthBarController>
    {
        [Header("Player")]
        [SerializeField] private Image roomOwnerHealthBar;
        [SerializeField] private TextMeshProUGUI roomOwnerHealthText;
        
        [Header("Enemy")]
        [SerializeField] private Image guestHealthBar;
        [SerializeField] private TextMeshProUGUI guestHealthText;

        private PlayerData _playerData;
        private EventData _eventData;

        private float _maxHealth;

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("Data/EventData") as EventData;
        }

        private void Start()
        {
            if (!GameManager.Instance.RoomOwner)
            {
                roomOwnerHealthBar.transform.localScale = new Vector3(-1, 1, 1);
                guestHealthBar.transform.localScale = new Vector3(-1, 1, 1);
                roomOwnerHealthBar.fillOrigin = 1;
                guestHealthBar.fillOrigin = 0;
            }
        }

        public void SetMaxHealths(int health)
        {
            _maxHealth = health;
            roomOwnerHealthText.text = $"{health}";
            roomOwnerHealthBar.fillAmount = 1;
            guestHealthText.text = $"{health}";
            guestHealthBar.fillAmount = health / _maxHealth;
        }
        
        public void UpdatePlayerHealth(int health)
        {
            if (GameManager.Instance.RoomOwner)
            {
                roomOwnerHealthText.text = $"{health}";
                roomOwnerHealthBar.fillAmount = health / _maxHealth;
            }
            else
            {
                guestHealthText.text = $"{health}";
                guestHealthBar.fillAmount = health / _maxHealth;
            }
        }
        
        public void UpdateEnemyHealth(int health)
        {
            if (!GameManager.Instance.RoomOwner)
            {
                roomOwnerHealthText.text = $"{health}";
                roomOwnerHealthBar.fillAmount = health / _maxHealth;
            }
            else
            {
                guestHealthText.text = $"{health}";
                guestHealthBar.fillAmount = health / _maxHealth;
            }
        }
    }
}
