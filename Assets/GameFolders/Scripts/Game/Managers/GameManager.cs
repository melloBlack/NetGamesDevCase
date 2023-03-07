using System;
using GameFolders.Scripts.Core;
using Photon.Pun;
using UnityEngine;

namespace GameFolders.Scripts.Game.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public bool RoomOwner => PhotonNetwork.IsMasterClient;

        private void Awake()
        {
            Singleton(true);
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        public void CreatePlayer()
        {
            PhotonNetwork.Instantiate("Player", GetPlayerStartPosition(), Quaternion.identity);
        }

        private Vector3 GetPlayerStartPosition()
        {
            float xPos;
            
            if (RoomOwner)
            {
                xPos = -4.5f;
            }
            else
            {
                xPos = 4.5f;
            }

            return Vector3.right * xPos;
        }
    }
}
