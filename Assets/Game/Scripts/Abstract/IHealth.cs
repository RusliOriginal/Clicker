using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthValue(int healthValue);
public delegate void Hit();
public delegate void Death();

public interface IHealth
{
    event HealthValue OnHealthValue;
    event Hit OnHit;
    event Death OnDeath;

    void Damage(int damage);
}
