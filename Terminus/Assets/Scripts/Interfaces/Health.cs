using Player;

namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(int amount, PainTypes painType = PainTypes.Hit);
    }
}