using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjectIndicator : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;

    private ParticleSystem _particle;
    private bool _shouldPlay;
    private Camera _levelCamera;

	void Start ()
    {
        _particle = Instantiate(particlePrefab);
        _levelCamera = GameObject.FindObjectOfType<LevelManager>().LevelCamera;
    }
	
	void Update ()
    {
        bool playParticle = false;
        var rayStart = _levelCamera.ScreenToWorldPoint(Input.mousePosition);
        rayStart.z = -10;
        RaycastHit2D hitInfo = Physics2D.Raycast(rayStart, Vector3.forward);
        if (hitInfo.transform != null)
        {
            Clickable2D click = hitInfo.transform.GetComponent<Clickable2D>();
            if (click == null && hitInfo.transform.parent != null)
            {
                click = hitInfo.transform.parent.GetComponent<Clickable2D>();
                playParticle = true;
            }
            playParticle = click != null && click.enabled;
        }
        if (_shouldPlay != playParticle)
        {
            _shouldPlay = playParticle;
            if (_shouldPlay)
                _particle.Play();
            else
                _particle.Stop();
        }
        var pos = _levelCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -10;
        _particle.transform.position = pos;
    }
}
