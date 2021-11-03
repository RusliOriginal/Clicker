
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public event HealthValue OnHealthValue;
    public event Hit OnHit;
    public event Death OnDeath;

    [SerializeField] private int _healthValue;
    [SerializeField] private int _maxHealthValue;

    private bool _dead;

    public void Damage(int damage)
    {
        if (!_dead)
        {
            if (damage > 0)
            {
                _healthValue = Mathf.Clamp(_healthValue - damage, 0, _maxHealthValue);

                OnHealthValue?.Invoke(_healthValue);
            }

            OnHit?.Invoke();

            if (_healthValue == 0)
            {
                _dead = true;

                OnDeath?.Invoke();
            }
        }
    }
}
