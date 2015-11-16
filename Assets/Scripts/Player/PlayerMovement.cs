using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float jumpStartForce = 200f;
	public float jumpStartTime = 0.07f;
	float jumpStartTimePassed = 0f;
	public float jumpMidForce = 300f;
	public float maxJump = 600f;
	public float walkSpeed = 2f;
	bool grounded = true;
	bool touchingGround = true;
	float jumpPassed = 0f;
	bool jumpPressed = false;
	Rigidbody2D rbody2d;
	Animator animator;
	HashAnimatorCharacter hashAnimatorCharacter;

	void Awake () {
		touchingGround = false;
		rbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		hashAnimatorCharacter = GetComponent<HashAnimatorCharacter>();
	}

	void Update () {

		/*on touch pressed. subtitute axisraw for -1 or 1*/
		if(Input.GetAxis("Horizontal") != 0){
			rbody2d.velocity = new Vector2( (Vector2.right.x * Input.GetAxisRaw("Horizontal") * walkSpeed), rbody2d.velocity.y );
		}

		if(Input.GetButton("Jump")){
			if(grounded && !jumpPressed){
				jumpPressed = true;
				rbody2d.AddForce(Vector2.up * jumpStartForce);
			}
		}

		if(jumpPressed){
			jumpStartTimePassed += Time.deltaTime;
		}


		/*on touch status endtouch*/
		if(Input.GetButtonUp("Jump")){
			jumpPressed = false;
			jumpPassed = 0f;
			jumpStartTimePassed = 0f;
		}
	}

	void FixedUpdate(){
		/*ontouch status = pressed*/
		if(Input.GetButton("Jump")){
			if(jumpPressed && jumpPassed <= maxJump && jumpStartTimePassed >= jumpStartTime){
				jumpPassed += jumpMidForce;
				rbody2d.AddForce(Vector2.up * jumpMidForce);
			}
		}

		animator.SetFloat(hashAnimatorCharacter.xVelocity, rbody2d.velocity.x);
		animator.SetFloat(hashAnimatorCharacter.yVelocity, rbody2d.velocity.y);

		if(rbody2d.velocity.y != 0 && grounded){
			setGrounded(false);
		}
		if(!grounded && rbody2d.velocity.y == 0 && touchingGround){
			setGrounded(true);
		}
	}

	public void setGrounded(bool grounded){
		this.grounded = grounded;
		animator.SetBool(hashAnimatorCharacter.grounded, this.grounded);
	}

	public Rigidbody2D getRigidbody2D(){
		return rbody2d;
	}
	
	public void setTouchingGround(bool touchingGround){
		this.touchingGround = touchingGround;
	}
	
	public bool getTouchingGround(){
		return touchingGround;
	}
}
