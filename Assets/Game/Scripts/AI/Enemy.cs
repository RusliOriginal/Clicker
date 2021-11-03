using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _health;
    private IHealth _iHealth;
    [Space]
    [SerializeField] private Collider _enemyCollider;
    [Space]
    [SerializeField] private Animator _animator;

    private void OnValidate()
    {
        if(_health)

            if (_health.GetComponent<IHealth>() == null)
            {
                _health = null;

                Debug.LogError("Enemy : _health must contain the iHealth interface");
            }
    }

    private void Start()
    {
        if (_health)
            _iHealth = _health?.GetComponent<IHealth>();

        if (_health?.GetComponent<IHealth>() != null)
        {
            _iHealth.OnHit += Hit;
            _iHealth.OnDeath += Death;
        }
    }

    private void Hit()
    {
        _animator.SetTrigger(nameof(Hit));
    }

    private void Death()
    {
        _animator.SetTrigger(nameof(Death));

        if (_health?.GetComponent<IHealth>() != null)
        {
            _iHealth.OnHit -= Hit;
            _iHealth.OnDeath -= Death;
        }

        if (_enemyCollider)
            _enemyCollider.enabled = false;
    }
}
