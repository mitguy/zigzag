using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour {
	public int WaveCount { get; set; }
	void Start() {
		WaveCount = 0;
	}
}