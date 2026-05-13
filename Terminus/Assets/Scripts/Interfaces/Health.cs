using Player;

namespace Interfaces
{
    public interface IDamageable
    {
        public bool TakeDamage(int amount, PainTypes painType = PainTypes.Hit);
        public bool IsAlive();
    }
}