using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCrownAdder : MonoBehaviour {
	public string Add(string start, string add) {
		if (start == "") {
			return add;
		}
		return start + "," + add;
	}
}
