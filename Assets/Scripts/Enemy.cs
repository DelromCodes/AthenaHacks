using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Collider Collider;
	public float xIncrement;
	public float yIncrement;

	public void OnMove() {
		Vector3 newWorldPos = transform.position;
		newWorldPos.x += xIncrement;
		newWorldPos.y += yIncrement;

		Vector3 leftBoundPos = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0f, 0f));
		Vector3 rightBoundPos = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0f, 0f));
		Vector3 upperBoundPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.9f, 0f));
		Vector3 lowerBoundPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.1f, 0f));

		if (newWorldPos.x < leftBoundPos.x || newWorldPos.x > rightBoundPos.x) {
			xIncrement *= -1;
		}
		if (newWorldPos.y < lowerBoundPos.y || newWorldPos.y > upperBoundPos.y) {
			yIncrement *= -1;
		}

		transform.position = newWorldPos;
	}
}
