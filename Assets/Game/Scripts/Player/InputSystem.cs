using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    private IWeapon _iWeapon;

    private void OnValidate()
    {
        if (_weapon)

            if (_weapon.GetComponent<IWeapon>() == null)
            {
                _weapon = null;

                Debug.LogError("InputSystem : _weapon must contain the IWeapon interface");
            }
    }

    private void Start()
    {
        if (_weapon)
            if (_weapon.GetComponent<IWeapon>() != null)
                _iWeapon = _weapon.GetComponent<IWeapon>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _iWeapon?.Shoot(Input.mousePosition);
        }
    }
}
