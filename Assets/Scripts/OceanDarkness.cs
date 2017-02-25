using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanDarkness : MonoBehaviour 
{

    public GameObject mask;
    public GameObject glowMask;
    public Vector3 glowOffset;

    public void SetDarkness(float val)
    {
        _darkness = val;
        SetMaskAlpha(Mathf.Lerp(0f, 0.9f, val));
    }

    public void SetIsGlow(bool val)
    {
        _isGlow = val;
        mask.SetActive(!_isGlow);
        glowMask.SetActive(_isGlow);
        var current = _isGlow ? glowMask : mask;
        _sprite = current.GetComponent<SpriteRenderer>();
        _maskTransform = current.transform;
    }

    public void Move(Vector2 pos)
    {
        _maskTransform.position = new Vector3(pos.x, pos.y, _maskTransform.position.z) + glowOffset;
    }

    private float _darkness;
    private bool _isGlow;

    private SpriteRenderer _sprite;
    private Transform _maskTransform;

    private void Awake()
    {
        SetIsGlow(false);
    }

    private void SetMaskAlpha(float val)
    {
        var c = _sprite.color;
        c.a = val;
        _sprite.color = c;
    }

}
