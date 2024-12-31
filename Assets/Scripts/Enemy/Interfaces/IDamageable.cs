using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Die();
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
}
