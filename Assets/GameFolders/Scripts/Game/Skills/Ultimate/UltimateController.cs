using GameFolders.Scripts.Core;
using GameFolders.Scripts.Game.Managers;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace GameFolders.Scripts.Game.Skills.Ultimate
{
    public class UltimateController : MonoBehaviour
    {
        [SerializeField] private UltimateSkill ultimateSkillPrefab;

        private EventData _eventData;
        private PhotonView _photonView;
        
        private void Awake()
        {
            _eventData = Resources.Load("Data/EventData") as EventData;
            _photonView = GetComponentInParent<PhotonView>();
        }

        private void OnEnable()
        {
            if (!_photonView.IsMine) return;

            _eventData.OnClickedUltimateButton += OnStartUltimate;
        }

        private void OnDisable()
        {
            if (!_photonView.IsMine) return;

            _eventData.OnClickedUltimateButton -= OnStartUltimate;
        }

        private void OnStartUltimate()
        {
            Vector3 direction = GameManager.Instance.RoomOwner ? Vector3.right : Vector3.left;
            GameObject ultimateSkillObject = PhotonNetwork.Instantiate(ultimateSkillPrefab.name, GameController.Instance.PlayerTransform.position + direction,
                Quaternion.identity);
        }
    }
}
