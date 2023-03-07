using System.Collections;
using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.General;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFolders.Scripts.Game.Managers
{
    public class GameController : MonoSingleton<GameController>
    {
        public GameEnum.BelongsTo BelongsTo { get; set; }

        public Transform EnemyTransform { get; set; }
        public Transform PlayerTransform { get; set; }
        
        private EventData _eventData;

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("Data/EventData") as EventData;

        }

        private void OnEnable()
        {
            _eventData.OnLoseMatch += OnFinishGame;
            _eventData.OnWinMatch += OnFinishGame;
        }

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.CreatePlayer();
        }

        private void OnDisable()
        {
            _eventData.OnLoseMatch -= OnFinishGame;
            _eventData.OnWinMatch -= OnFinishGame;
        }

        private void OnFinishGame()
        {
            StartCoroutine(LoadMainMenuAfterDelay());
        }
        
        private void LoadMainMenu()
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }

        private IEnumerator LoadMainMenuAfterDelay()
        {
            yield return new WaitForSeconds(2);
            LoadMainMenu();
        }
    }
}
