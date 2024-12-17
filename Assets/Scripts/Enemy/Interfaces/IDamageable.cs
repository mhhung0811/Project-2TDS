using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Die();
    void Damage(float damage);
    int MaxHealth { get; set; }
    IntVariable CurrentHealth { get; set; }
}
