using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolFish : MonoBehaviour 
{
    public float circuitTime;
    public GameObject bubbles;
    public Vector3 orbit = new Vector3(150f, -50f, 40f);
    public Vector3 orbitCenter;
    private bool _isSchooling;
    private float _randomSeed;
    private Vector3 _ogScale;

    private void Awake()
    {
        _ogScale = transform.localScale;
        SetScaleForDistance();
    }

    public void StartSchooling()
    {
        Logger.Log(1);
        _isSchooling = true;
        _randomSeed = Random.value;
        bubbles.SetActive(true);
    }

    private void Update()
    {
        if (!_isSchooling) return;
        var angle = ((Time.time + _randomSeed * circuitTime) % circuitTime) / circuitTime;
        angle *= Mathf.PI * 2f;
        var pos = new Vector3(Mathf.Cos(angle) * orbit.x, Mathf.Sin(angle) * orbit.y, -Mathf.Sin(angle) * orbit.z);
        pos += orbitCenter;
        pos = Vector3.Lerp(transform.localPosition, pos, Time.deltaTime * 1f);
        transform.localPosition = pos;

        SetScaleForDistance();
        var scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        // scale.x *= Mathf.Sin(angle);
        if (pos.z > orbitCenter.z) scale.x = -scale.x;
        // scale.x = Mathf.Lerp(transform.localScale.x, scale.x, Time.deltaTime);
        transform.localScale = scale;
    }

    private void SetScaleForDistance()
    {
        var scale = _ogScale * GetScaleFactor(transform.localPosition.z);
        transform.localScale = scale;
    }

    private float GetScaleFactor(float z)
    {
        var scaleFactor = Mathf.Clamp(Mathf.InverseLerp(200f, 10f, z), 0.5f, 1f);
        scaleFactor *= scaleFactor;
        return scaleFactor;
    }
}
