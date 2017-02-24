using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Camera levelCamera;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private LeanTweenType transitionEase = LeanTweenType.easeOutBack;

    private int _currentLevel = 0;

    void Start()
    {
        SetLevel(0);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            SetLevel((int)Mathf.Repeat(_currentLevel + 1, levels.Length));
        }
    }

    void SetLevel(int level)
    {
        _currentLevel = level;
        var levelGO = levels[_currentLevel];

        LeanTween.cancel(levelCamera.gameObject);
        LeanTween.moveX(levelCamera.gameObject, levelGO.transform.position.x, transitionTime)
            .setEase(transitionEase)
            .setOnComplete(() =>
            {
                var flowchart = levelGO.GetComponentInChildren<Flowchart>();
                flowchart.ExecuteBlock("Start");
            });
    }
}
