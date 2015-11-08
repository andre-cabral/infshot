using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float jumpMidForce = 2000f;
	public float maxJump = 100f;
	bool grounded = true;
	float jumpPassed = 0f;
	bool jumpPressed = false;
	Rigidbody2D rbody2d;

	// Use this for initialization
	void Awake () {
		rbody2d = GetComponent<Rigidbody2D>();
	}

	void Update () {
		if(Input.GetButton("Jump")){
			if(grounded && !jumpPressed){
				jumpPressed = true;
			}
			if(jumpPressed && jumpPassed <= maxJump){
				jumpPassed += Time.deltaTime * jumpMidForce;
				rbody2d.AddForce(Vector2.up * Time.deltaTime * jumpMidForce);
			}
		}
		if(Input.GetButtonUp("Jump")){
			jumpPressed = false;
			jumpPassed = 0f;
		}
	}

	public void setGrounded(bool grounded){
		this.grounded = grounded;
	}

	public Rigidbody2D getRigidbody2D(){
		return rbody2d;
	}
}
