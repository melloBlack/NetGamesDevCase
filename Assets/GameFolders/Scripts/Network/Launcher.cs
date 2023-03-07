using GameFolders.Scripts.Game.Managers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFolders.Scripts.Network
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float matchSearchTime;

        private bool _isJoinedLobby;
        private bool _isSearchingRoom;
        private float _startSearchTime;

        public override void OnEnable()
        {
            base.OnEnable();
            LauncherEventManager.Instance.OnSearchMatch += OnSearchMatch;
        }

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        private void Update()
        {
            if (!_isJoinedLobby || !_isSearchingRoom) return;

            if (Time.time - _startSearchTime >= matchSearchTime)
            {
                RoomOptions roomOptions = new RoomOptions
                {
                    MaxPlayers = 2
                };
                PhotonNetwork.CreateRoom($"Room{PhotonNetwork.CountOfRooms}", roomOptions);
                _isSearchingRoom = false;
                return;
            }

            if (PhotonNetwork.JoinRandomRoom())
            {
                _isSearchingRoom = false;
            }
        }

        private void OnSearchMatch()
        {
            _isSearchingRoom = true;
            _startSearchTime = Time.time;
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            LauncherEventManager.Instance.OnJoinedLobby?.Invoke();
            _isJoinedLobby = true;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _isSearchingRoom = false;
            PhotonNetwork.LoadLevel(1);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            LauncherEventManager.Instance.OnCreateRoomFailed?.Invoke();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            _isSearchingRoom = true;
        }
    }
}