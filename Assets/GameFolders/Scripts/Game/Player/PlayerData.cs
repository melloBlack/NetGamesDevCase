using GameFolders.Scripts.Game.General;
using UnityEngine;

namespace GameFolders.Scripts.Game.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Movement")] 
        [SerializeField] private GameEnum.InputType inputType;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float speedUpSpeed;
        [SerializeField] private float speedUpTime;

        [Header("Stats")] 
        [SerializeField] private int health;

        [Header("Bullet")] 
        [SerializeField] private int damage;
        [SerializeField] private float forcePower;
        [SerializeField] private float maxBulletLifeTime;

        public GameEnum.InputType InputType => inputType;
        public float MoveSpeed => moveSpeed;
        public float SpeedUpSpeed => speedUpSpeed;
        public float SpeedUpTime => speedUpTime;
        public int Health => health;
        public int Damage => damage;
        public float ForcePower => forcePower;
        public float MaxBulletLifeTime => maxBulletLifeTime;
    }
}