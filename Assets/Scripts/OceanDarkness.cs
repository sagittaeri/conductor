using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class OceanDarkness : MonoBehaviour 
{

    public Flowchart flowchart;
    public GameObject mask;
    public GameObject glowMask;
    public Vector3 glowOffset;
    public string clickMessage;
    public Transform unblocker;

    public void SetDarkness(float val, float duration)
    {
        StartCoroutine(AnimateDarkness(val, duration));
    }

    public void SetDarkness(float val)
    {
        _darkness = val;
        SetMaskAlpha(Mathf.Lerp(0f, 1f, val));
    }

    private IEnumerator AnimateDarkness(float val, float duration)
    {
        var elapsed = 0f;
        var start = _darkness;
        while (elapsed < duration)
        {
            SetDarkness(Mathf.Lerp(start, val, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetDarkness(val);
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

    public void Move(Vector3 pos)
    {
        _maskTransform.position = new Vector3(pos.x, pos.y, _maskTransform.position.z) + glowOffset;
    }

    public void Move(Vector3 pos, float duration)
    {
        StartCoroutine(AnimateMove(pos, duration));
    }

    private IEnumerator AnimateMove(Vector3 pos, float duration)
    {
        var elapsed = 0f;
        var start = _maskTransform.position;
        var target = new Vector3(pos.x, pos.y, _maskTransform.position.z) + glowOffset;
        while (elapsed < duration)
        {
            _maskTransform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _maskTransform.position = target;
    }

    private float _darkness = 1f;
    private bool _isGlow;

    private SpriteRenderer _sprite;
    private Transform _maskTransform;

    private void Awake()
    {
        SetIsGlow(false);
        var trigger = GetComponent<BoxCollider2D>();
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        trigger.size = new Vector2(width, height);
    }

    private void SetMaskAlpha(float val)
    {
        var c = _sprite.color;
        c.a = val;
        _sprite.color = c;
    }

    public void CheckForCloseToSeahorse(GameObject thing)
    {
        if (IsCloseTo(thing))
            flowchart.SetBooleanVariable("hasFoundSwordfish", true);
    }

    public void CheckForCloseToSunBlocker(GameObject thing)
    {
        if (IsCloseTo(thing))
            flowchart.SetBooleanVariable("hasFoundSunBlocker", true);
    }
    private bool IsCloseTo(GameObject thing)
    {
        var v = flowchart.GetVariable<VariableBase<Vector3>>("jellyTarget");
        var pos = v.Value;
        var dist = (pos - thing.transform.position).magnitude;
        Logger.Log(dist);
        return (dist < 2f);
    }

    private void OnMouseDown()
    {

        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0f;
        var v = flowchart.GetVariable<VariableBase<Vector3>>("jellyTarget");
        v.Value = target;
        flowchart.SendFungusMessage(clickMessage);
    }
}
