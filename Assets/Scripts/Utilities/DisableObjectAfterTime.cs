using UnityEngine;
using System.Collections;

public class DisableObjectAfterTime : MonoBehaviour {

	public float timeToDisable = 0.1f;

	float timePassed = 0f;


	void Update () {
		timePassed += Time.deltaTime;
		if(timePassed >= timeToDisable){
			timePassed = 0f;
			gameObject.SetActive(false);
		}
	}
}
