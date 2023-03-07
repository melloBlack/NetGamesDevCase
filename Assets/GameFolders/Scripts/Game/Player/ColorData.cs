using UnityEngine;

namespace GameFolders.Scripts.Game.Player
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "Data/Color Data")]
    public class ColorData : ScriptableObject
    {
        [Header("Player")]
        [SerializeField] private Color playerColor;
        [SerializeField] private Color playerBulletColor;
        
        [Header("Enemy")]
        [SerializeField] private Color enemyColor;
        [SerializeField] private Color enemyBulletColor;

        public Color PlayerColor => playerColor;
        public Color PlayerBulletColor => playerBulletColor;
        public Color EnemyColor => enemyColor;
        public Color EnemyBulletColor => enemyBulletColor;
    }
}
