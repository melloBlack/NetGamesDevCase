using System;
using UnityEngine;

namespace GameFolders.Scripts.Core
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
    public class EventData : ScriptableObject
    {
        public Action OnLoseMatch { get; set; }
        public Action OnWinMatch { get; set; }
        public Action OnClickedAttackButton { get; set; }
        public Action OnClickedSpeedUpButton { get; set; }
        public Action OnClickedUltimateButton { get; set; }
        public Action OnResetMatch { get; set; }
    }
}