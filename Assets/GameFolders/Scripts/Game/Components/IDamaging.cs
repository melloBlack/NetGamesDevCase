using GameFolders.Scripts.Game.General;
using UnityEngine;

namespace GameFolders.Scripts.Game.Components
{
    public interface IDamaging
    {
        public int Damage { get; set; }
        public void StopAction();
        public GameEnum.BelongsTo BelongsTo { get; set; }
    }
}
