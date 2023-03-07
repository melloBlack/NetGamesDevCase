using GameFolders.Scripts.Game.General;

namespace GameFolders.Scripts.Game.Components
{
    public interface IDamageable
    {
        public GameEnum.BelongsTo BelongsTo { get; set; }
        public int Health { get; set; }
        public void TakeDamage(int damage);
    }
}
