using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public float slowdownFactor = 0.25f;
	public float slowdownLength = 2f;

	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			Time.timeScale = slowdownFactor;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;

		} else {
			Time.timeScale += (1f / slowdownLength) * Time.deltaTime;
			Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);
		}
	}
}
