using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField] private GameObject _hitMarker;
    private IHitMarker _iHitMarker;
    [Space]
    [SerializeField] private int _damage;
    [Space]
    [SerializeField] private LayerMask _shootLayerMask;
    [Space]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _hitSound;

    private void OnValidate()
    {
        if (_damage < 0)
        {
            _damage = 0;

            Debug.LogError("Shoot : Damage cannot be negative");
        }

        if (_hitMarker)

            if (_hitMarker.GetComponent<IHitMarker>() == null)
            {
                _hitMarker = null;

                Debug.LogError("Pistol : _hitMarker must contain the IHitMarker interface");
            }
    }

    private void Start()
    {
        if(_hitMarker.GetComponent<IHitMarker>() != null)
        _iHitMarker = _hitMarker?.GetComponent<IHitMarker>();
    }

    public void Shoot(Vector3 mousePosition)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit, _shootLayerMask))
        {
            if (hit.transform.GetComponent<IHealth>() != null)
            {
                hit.transform.GetComponent<IHealth>().Damage(_damage);
                _audioSource.PlayOneShot(_hitSound);

                if (_hitMarker != null)
                    _iHitMarker.Hit(mousePosition);
            }
        }

        _audioSource.PlayOneShot(_shootSound);
    }
}
