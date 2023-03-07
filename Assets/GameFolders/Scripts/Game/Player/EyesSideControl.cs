using System;
using GameFolders.Scripts.Game.Managers;
using Photon.Pun;
using UnityEngine;

namespace GameFolders.Scripts.Game.Player
{
    public class EyesSideControl : MonoBehaviour
    {
        [SerializeField] private GameObject leftEyes;
        [SerializeField] private GameObject rightEyes;

        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            bool amRoomOwner = GameManager.Instance.RoomOwner;

            if (_photonView.IsMine)
            {
                rightEyes.SetActive(amRoomOwner);
                leftEyes.SetActive(!amRoomOwner);
            }
            else
            {
                rightEyes.SetActive(!amRoomOwner);
                leftEyes.SetActive(amRoomOwner);
            }
            
        }
    }
}
