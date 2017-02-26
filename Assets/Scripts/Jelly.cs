using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Jelly : MonoBehaviour 
{

    public Rect targetRect;
    public Flowchart flowchart;

    public void SetRandomDestination()
    {
        var target = new Vector3(Random.Range(targetRect.xMin, targetRect.xMax), Random.Range(targetRect.yMin, targetRect.yMax), transform.position.z);
        var v = flowchart.GetVariable<VariableBase<Vector3>>("jellyTarget");
        v.Value = target;
    }


}
