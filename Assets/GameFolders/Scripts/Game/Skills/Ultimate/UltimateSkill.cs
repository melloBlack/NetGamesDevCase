using System;
using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.General;
using Photon.Pun;
using UnityEngine;

namespace GameFolders.Scripts.Game.Skills.Ultimate
{
    [RequireComponent(typeof(PhotonView))]
    public abstract class UltimateSkill : MonoBehaviourPunCallbacks
    {
        public GameEnum.BelongsTo BelongsTo { get; set; }
        
        protected PhotonView _photonView;
        
        protected virtual void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public abstract void Play(Vector2 startPosition);
    }
}
