using GameFolders.Scripts.Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Game.Components
{
    public class HealthBarColorController : MonoBehaviour
    {
        [SerializeField] private Image fillBar;
        [SerializeField] private Sprite roomOwnerSourceImage;
        [SerializeField] private Sprite guestSourceImage;
        [SerializeField] private Image healthImage;
        [SerializeField] private Color roomOwnerColor;
        [SerializeField] private Color guestColor;
    
        private void Start()
        {
            if (GameManager.Instance.RoomOwner)
            {
                fillBar.sprite = roomOwnerSourceImage;
                healthImage.color = roomOwnerColor;
            }
            else
            {
                fillBar.sprite = guestSourceImage;
                healthImage.color = guestColor;
            }
        }
    }
}
