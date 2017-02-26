using System.Collections;
using System;
using UnityEngine;

public class AmbientMovement : MonoBehaviour 
{

    [System.Serializable]
    public class MovementComponent
    {
        public bool enable;
        public Vector3 delta;
        public float duration;
        [HideInInspector]
        public Vector3 prevOffset;
    }

    public MovementComponent position;
    public MovementComponent rotation;
    public MovementComponent scale;

    private Transform _transform;


    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        StartCoroutine(AnimatePosition());
        StartCoroutine(AnimateRotation());
        StartCoroutine(AnimateScale());
    }

    private IEnumerator AnimatePosition()
    {
        while (position.enable && position.duration != 0f)
        {
            yield return Tween(position.duration, (t) => { _transform.localPosition += TweenComponent(position, t); });
        }
    }

    private IEnumerator AnimateRotation()
    {
        while (rotation.enable && rotation.duration != 0f)
        {
            yield return Tween(rotation.duration, (t) => { _transform.localEulerAngles += TweenComponent(rotation, t); });
        }
    }

    private IEnumerator AnimateScale()
    {
        while (scale.enable && scale.duration != 0f)
        {
            yield return Tween(scale.duration, (t) => { _transform.localScale += TweenComponent(scale, t); });
        }
    }



    private Vector3 TweenComponent(MovementComponent comp, float t)
    {
        var targetOffset = Mathf.Sin(t * Mathf.PI * 2f) * comp.delta;
        var delta = targetOffset - comp.prevOffset;
        // _transform.localPosition += delta;
        comp.prevOffset = targetOffset;
        return delta;
    }

    private IEnumerator Tween(float duration, Action<float> action)
    {
        var elapsed = 0f;
        while (true)
        {
            elapsed += Time.deltaTime;
            if (elapsed > duration) elapsed -= duration;
            action(elapsed / duration);
            yield return null;
        }
    }
}
