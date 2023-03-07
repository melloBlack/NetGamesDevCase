using GameFolders.Scripts.Core;
using UnityEngine;

namespace GameFolders.Scripts.Game.Managers
{
    public class UIController : MonoSingleton<UIController>
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        private EventData _eventData;

        private void Awake()
        {
            _eventData = Resources.Load("Data/EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnWinMatch += OnWinMatch;
            _eventData.OnLoseMatch += OnLoseMatch;
        }

        private void Start()
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        private void OnDisable()
        {
            _eventData.OnWinMatch -= OnWinMatch;
            _eventData.OnLoseMatch -= OnLoseMatch;
        }

        private void OnWinMatch()
        {
            winPanel.SetActive(true);
        }

        private void OnLoseMatch()
        {
            losePanel.SetActive(true);
        }
    }
}
