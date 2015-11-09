using UnityEngine;
using System.Collections;

public class HashAnimatorCharacter : MonoBehaviour {

	public int xVelocity;
	public int yVelocity;
	public int grounded;

	void Awake () {
		xVelocity = Animator.StringToHash("xVelocity");
		yVelocity = Animator.StringToHash("yVelocity");
		grounded = Animator.StringToHash("Grounded");
	}

}
