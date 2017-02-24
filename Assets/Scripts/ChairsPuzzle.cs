using System;
using UnityEngine;

public class ChairsPuzzle : MonoBehaviour {

	Transform[] chairs;
	public Transform[] sitters;
	public int[] positions;
	public int[] targetPositions;

	void Start () {
		chairs = new Transform[transform.childCount];		
        for (int i = 0; i < transform.childCount; i++) {
			chairs[i] = transform.GetChild(i);
		}

		if (chairs.Length - 1 != sitters.Length || sitters.Length != positions.Length || sitters.Length != targetPositions.Length) {
			Debug.LogError("Wrong number of chairs" + chairs.Length + ", sitters " + sitters.Length + ", positions, or targetPositions!");
			enabled = false;
		}
	}

	public Vector3 GetSitterPosition(Transform sitter) {
		return chairs[positions[Array.IndexOf(sitters, sitter)]].position;
	}

	public Vector3 GetNewSitterPosition(Transform sitter) {
		int sitterI = Array.IndexOf(sitters, sitter);
		int newPos = GetEmptySeat();

		positions[sitterI] = newPos;

		return GetSitterPosition(sitter);
	}

	public int GetEmptySeat() {
		for (int i = 0; i < chairs.Length; i++) {
			if (Array.IndexOf(positions, i) == -1) {
				return i;
			}
		}
		return -1;
	}
	
	public bool InIdealPosition() {
		for(int i = 0; i < positions.Length; i++) {
			if (positions[i] != targetPositions[i])
				return false;
		}
		return true;
	}
}
