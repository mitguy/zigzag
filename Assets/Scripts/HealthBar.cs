using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public float maxHealth = 100;
	public float currentHealth = 100;
	private float originalScale;

	public void Start() {
		originalScale = gameObject.transform.localScale.x;
	}

	private void Update() {
		Vector3 tmpScale = gameObject.transform.localScale;
		tmpScale.x = currentHealth / maxHealth * originalScale;
		gameObject.transform.localScale = tmpScale;
	}
}
