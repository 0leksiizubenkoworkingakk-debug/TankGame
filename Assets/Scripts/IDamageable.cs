using UnityEngine;
using System;

public interface IDamageable
{
    void TakeDamage(int amount);
    public event Action Died;
}
