using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	public float Degrees = 1f;

	void FixedUpdate() {
		transform.Rotate(Vector3.up, Degrees); //Rotation is based on degrees
	}
}
