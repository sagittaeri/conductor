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
    private Flowchart _currentFlowchart;
    private bool _executingBlock;

    private Clickable2D[] clickables;

    public Camera LevelCamera { get { return levelCamera; } }

    void Start()
    {
        SetLevel(0);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            SetLevel((int)Mathf.Repeat(_currentLevel + 1, levels.Length));
        }
#endif

        if (_currentFlowchart != null)
        {
            bool executing = _currentFlowchart.HasExecutingBlocks();
            if (_executingBlock != executing)
            {
                _executingBlock = executing;
                foreach (var clickable in clickables)
                {
                    clickable.enabled = !_executingBlock;
                }
            }
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
                _currentFlowchart = levelGO.GetComponentInChildren<Flowchart>();
                if (_currentFlowchart.FindBlock("Start") != null)
                    _currentFlowchart.ExecuteBlock("Start");
            });

        clickables = GameObject.FindObjectsOfType<Clickable2D>();
    }
}
