using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _health;
    private IHealth _iHealth;
    [Space]
    [SerializeField] private Text _healthValueText;
    [SerializeField] private RectTransform _healthBarWindow;
    [Space]
    [SerializeField] private int _middleHealthValue;
    [SerializeField] private int _lowHealthValue;
    [Space]
    [SerializeField] private Color _fullHealthColor;
    [SerializeField] private Color _middleHealthColor;
    [SerializeField] private Color _lowHealthColor;

    private void OnValidate()
    {
        if (_health)

            if (_health.GetComponent<IHealth>() == null)
            {
                _health = null;

                Debug.LogError("HealthUI : _health must contain the iHealth interface");
            }
    }

    private void Start()
    {
        if (_health != null)
            _iHealth = _health.GetComponent<IHealth>();

        if (_iHealth != null)
        {
            _iHealth.OnHealthValue += HealthValueView;
            _iHealth.OnDeath += DeathView;
        }

        _healthValueText.color = _fullHealthColor;
    }

    private void HealthValueView(int healthValue)
    {
        _healthValueText.text = "" + healthValue;

        _healthBarWindow.localScale = new Vector3(1.6f, 1.6f, 1.6f);

        if (healthValue <= _middleHealthValue)
            if (_healthValueText.color != _middleHealthColor)
                _healthValueText.color = _middleHealthColor;

        if (healthValue <= _lowHealthValue)
            if (_healthValueText.color != _lowHealthColor)
                _healthValueText.color = _lowHealthColor;
    }

    private void DeathView()
    {
        if (_iHealth != null)
        {
            _iHealth.OnHealthValue -= HealthValueView;
            _iHealth.OnDeath -= DeathView;
        }

        if (_healthBarWindow)
            _healthBarWindow.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_healthBarWindow)
            if (_healthBarWindow.gameObject.activeSelf)
                if (_healthBarWindow.localScale != new Vector3(1, 1, 1))
                    _healthBarWindow.localScale = Vector3.MoveTowards(_healthBarWindow.localScale, new Vector3(1, 1, 1), Time.deltaTime * 4f);
    }
}
