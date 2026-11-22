using System;
namespace DefaultNamespace
{
    public interface IDamageable
    {
        event Action Died;
        void TakeDamage(int damage);
    }
}