using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerColorer : MonoBehaviour {
	RageSpline[] splines;

	void Start() {
		splines = GetComponentsInChildren<RageSpline>();
	}

	public void SetColor(Color c) {
		if (splines == null) {
			Start();
		}
		foreach (var spline in splines) {
			spline.SetFillColor2(c);
		}
	}
}
