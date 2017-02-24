using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.EventSystems;

public class ClickableBlockStarter : MonoBehaviour , IPointerClickHandler
{

    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string blockName;
    [SerializeField] private int commandIndex;

    [Tooltip("Is object clicking enabled")]
    [SerializeField] protected bool clickEnabled = true;

    [Tooltip("Use the UI Event System to check for clicks. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
    [SerializeField] protected bool useEventSystem;
    protected virtual void DoPointerClick()
    {
        if (!clickEnabled)
        {
            return;
        }
        var block = flowchart.FindBlock(blockName);
        flowchart.ExecuteBlock(block, commandIndex);
    }


    #region Legacy OnMouseX methods

    protected virtual void OnMouseDown()
    {
        if (!useEventSystem)
        {
            DoPointerClick();
        }
    }


    #endregion

    #region Public members

    /// <summary>
    /// Is object clicking enabled.
    /// </summary>
    public bool ClickEnabled { set { clickEnabled = value; } }

    #endregion

    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        if (useEventSystem)
        {
            DoPointerClick();
        }
    }

    #endregion

}
