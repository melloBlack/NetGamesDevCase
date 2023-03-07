using System;
using System.Collections;
using GameFolders.Scripts.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Network
{
    public class LauncherUIManager : MonoBehaviour
    {
        [Header("Loading Panel")]
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private TextMeshProUGUI serverConnectionMassageText;
        [SerializeField] private TextMeshProUGUI searchingTimeText;
        
        [Header("Lobby")]
        [SerializeField] private GameObject lobbyPanel;
        [SerializeField] private Button playButton;

        private bool _isSearchingMatch;
        private float _searchingTime;
        
        private void OnEnable()
        {
            LauncherEventManager.Instance.OnJoinedLobby += OnJoinedLobby;
            LauncherEventManager.Instance.OnCreateRoomFailed += OnCreateRoomFailed;
        }

        private void Start()
        {
            loadingPanel.SetActive(true);
            lobbyPanel.SetActive(false);
            serverConnectionMassageText.text = "Loading...";
        }

        private void Update()
        {
            if (!_isSearchingMatch) return;

            _searchingTime += Time.deltaTime;
            searchingTimeText.text = $"{Mathf.Round(_searchingTime)}";
        }

        private void OnDisable()
        {
            LauncherEventManager.Instance.OnJoinedLobby -= OnJoinedLobby;
            LauncherEventManager.Instance.OnCreateRoomFailed -= OnCreateRoomFailed;
        }

        private void OnJoinedLobby()
        {
            loadingPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            playButton.onClick.AddListener(OnClickPlayButton);
        }

        private void OnCreateRoomFailed()
        {
            serverConnectionMassageText.text = $"Please check your connection and try again.";
        }
        
        private void OnClickPlayButton()
        {
            LauncherEventManager.Instance.OnSearchMatch?.Invoke();
            serverConnectionMassageText.text = $"Searching...";
            _isSearchingMatch = true;
            lobbyPanel.SetActive(false);
            loadingPanel.SetActive(true);
        }
    }
}
 