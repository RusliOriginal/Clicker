using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour, IHitMarker
{
    [SerializeField] RectTransform _hitMark;
    [SerializeField] Image _hitMarkImage;

    public void Hit(Vector3 hitMarkPosition)
    {
        _hitMark.position = hitMarkPosition;
        _hitMark.localScale = new Vector3(1f, 1f, 1f);
        _hitMarkImage.color = new Color(_hitMarkImage.color.r, _hitMarkImage.color.g, _hitMarkImage.color.b, 1f);
    }

    private void Update()
    {
        if (_hitMark.localScale != new Vector3(2f, 2f, 2f))
            _hitMark.localScale = Vector3.MoveTowards(_hitMark.localScale, new Vector3(2f, 2f, 2f), Time.deltaTime * 4);

        if (_hitMarkImage.color.a != 0f)
            _hitMarkImage.color = new Color(_hitMarkImage.color.r, _hitMarkImage.color.g, _hitMarkImage.color.b, Mathf.MoveTowards(_hitMarkImage.color.a, 0f, Time.deltaTime * 4));
    }
}
