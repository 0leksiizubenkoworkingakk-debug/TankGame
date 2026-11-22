using System;

namespace DefaultNamespace
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
        event Action Died;
    }
}