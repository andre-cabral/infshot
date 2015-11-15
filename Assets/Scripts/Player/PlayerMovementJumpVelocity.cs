using UnityEngine;
using System.Collections;

public class PlayerMovementJumpVelocity : MonoBehaviour {

	public float jumpStartForce = 200f;
	public float jumpStartTime = 0.07f;
	float jumpStartTimePassed = 0f;
	public float jumpMidForce = 300f;
	public float jumpTime = 1f;
	public float walkSpeed = 2f;
	bool grounded = true;
	float jumpTimePassed = 0f;
	bool jumpPressed = false;
	bool jumpOnStartVelocity = false;
	bool jumpVelocityZero = true;
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

		if(Input.GetButton("Jump")){
			if(grounded && !jumpPressed){
				jumpPressed = true;
				jumpOnStartVelocity = true;
				jumpVelocityZero = false;
			}
		}



		if(jumpStartTimePassed >= jumpStartTime){
			jumpOnStartVelocity = false;
		}else{
			jumpStartTimePassed += Time.deltaTime;
		}


		/*on touch status endtouch*/
		if(Input.GetButtonUp("Jump")){
			jumpPressed = false;
		}


		if(!jumpVelocityZero && ((!jumpOnStartVelocity && !jumpPressed) || grounded)){
			Vector2 velocity = rbody2d.velocity;
			velocity.y = 0f;
			rbody2d.velocity = velocity;
			
			jumpTimePassed = 0f;
			jumpStartTimePassed = 0f;

			jumpVelocityZero = true;
		}
	}

	void FixedUpdate(){

		if(jumpOnStartVelocity && jumpStartTimePassed < jumpStartTime){
			Vector2 velocity = rbody2d.velocity;
			velocity.y = jumpStartForce;
			rbody2d.velocity = velocity;
		}

		/*ontouch status = pressed*/
		if(Input.GetButton("Jump")){
			if(jumpPressed && jumpTimePassed <= jumpTime && jumpStartTimePassed >= jumpStartTime){
				Vector2 velocity = rbody2d.velocity;
				velocity.y = jumpMidForce;
				rbody2d.velocity = velocity;
			}
		}

		animator.SetFloat(hashAnimatorCharacter.xVelocity, rbody2d.velocity.x);
		animator.SetFloat(hashAnimatorCharacter.yVelocity, rbody2d.velocity.y);

		if(rbody2d.velocity.y != 0 && grounded){
			setGrounded(false);
		}
		if(!grounded && rbody2d.velocity.y == 0){
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
}
