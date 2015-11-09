using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float jumpMidForce = 2000f;
	public float maxJump = 100f;
	public float walkSpeed = 2f;
	bool grounded = true;
	float jumpPassed = 0f;
	bool jumpPressed = false;
	Rigidbody2D rbody2d;
	Animator animator;
	HashAnimatorCharacter hashAnimatorCharacter;

	void Awake () {
		rbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		hashAnimatorCharacter = GetComponent<HashAnimatorCharacter>();
	}

	void Update () {

		/*on touch pressed. subtitute axisraw for -1 or 1*/
		if(Input.GetAxis("Horizontal") != 0){
			rbody2d.velocity = new Vector2( (Vector2.right.x * Input.GetAxisRaw("Horizontal") * walkSpeed), rbody2d.velocity.y );
		}

		/*ontouch status = pressed*/
		if(Input.GetButton("Jump")){
			if(grounded && !jumpPressed){
				jumpPressed = true;
			}
			if(jumpPressed && jumpPassed <= maxJump){
				jumpPassed += Time.deltaTime * jumpMidForce;
				rbody2d.AddForce(Vector2.up * Time.deltaTime * jumpMidForce);
			}
		}

		/*on touch status endtouch*/
		if(Input.GetButtonUp("Jump")){
			jumpPressed = false;
			jumpPassed = 0f;
		}
	}

	void FixedUpdate(){
		animator.SetFloat(hashAnimatorCharacter.xVelocity, rbody2d.velocity.x);
		animator.SetFloat(hashAnimatorCharacter.yVelocity, rbody2d.velocity.y);
	}

	public void setGrounded(bool grounded){
		this.grounded = grounded;
		animator.SetBool(hashAnimatorCharacter.grounded, this.grounded);
	}

	public Rigidbody2D getRigidbody2D(){
		return rbody2d;
	}
}
