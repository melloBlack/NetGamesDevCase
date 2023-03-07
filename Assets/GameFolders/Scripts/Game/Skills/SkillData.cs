using UnityEngine;

namespace GameFolders.Scripts.Game.Skills
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Data/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("Attack")] 
        [SerializeField] private float attackCooldown;
        
        [Header("SpeedUp")] 
        [SerializeField] private float speedUpCooldown;
        
        [Header("Ultimate")] 
        [SerializeField] private float ultimateCooldown;

        public float GetAttackCooldown()
        {
            return attackCooldown;
        }

        public float GetSpeedUpCooldown()
        {
            return speedUpCooldown;
        }

        public float GetUltimateCooldown()
        {
            return ultimateCooldown;
        }
    }
}
