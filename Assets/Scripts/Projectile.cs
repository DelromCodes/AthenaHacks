using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile : MonoBehaviour {

	public Collider Collider;

	protected Vector3 startPosition;
	protected Vector3 direction;

	public void Init(Vector3 startPosition_, Vector3 direction_) {
		startPosition = startPosition_;
		direction = direction_;

		transform.position = startPosition;
	}

	public void OnMove() {
		transform.position += direction;
	}
		
}
