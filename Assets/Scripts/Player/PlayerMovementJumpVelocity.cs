using UnityEngine;
using System.Collections;

public class PlayerMovementJumpVelocity : MonoBehaviour {

	public float jumpStartForce = 200f;
	public float jumpStartTime = 0.07f;
	float jumpStartTimePassed = 0f;
	public float jumpMidForce = 300f;
	public float jumpTime = 1f;
	public float walkSpeed = 2f;
	public GameObject[] allWeaponsObjects;
	public int startingWeaponNumber = 0;
	WeaponShootAndKickback[] allWeapons;
	WeaponShootAndKickback activeWeapon;
	bool grounded = true;
	bool touchingGround = true;
	bool touchingCeiling = false;
	float jumpTimePassed = 0f;
	bool jumpPressed = false;
	bool jumpOnStartVelocity = false;
	bool jumpVelocityZero = true;
	Rigidbody2D rbody2d;
	Animator animator;
	HashAnimatorCharacter hashAnimatorCharacter;

	bool AButton = false;
	bool LeftButton = false;
	bool RightButton = false;

	void Awake () {
		rbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		hashAnimatorCharacter = GetComponent<HashAnimatorCharacter>();

		allWeapons = new WeaponShootAndKickback[allWeaponsObjects.Length];

		for(int i=0; i<allWeaponsObjects.Length; i++){
			allWeapons[i] = allWeaponsObjects[i].GetComponent<WeaponShootAndKickback>();
		}

		ChangeWeapon(startingWeaponNumber);
	}

	void Update () {		
		if(LeftButton){
			rbody2d.velocity = new Vector2( (Vector2.right.x * -1 * walkSpeed), rbody2d.velocity.y );
		}

		if(RightButton){
			rbody2d.velocity = new Vector2( (Vector2.right.x * 1 * walkSpeed), rbody2d.velocity.y );
		}

		if(!LeftButton && !RightButton){
			rbody2d.velocity = new Vector2( 0f, rbody2d.velocity.y );
		}

		if( (jumpPressed || jumpOnStartVelocity) && !grounded){
			if(jumpStartTimePassed >= jumpStartTime){
				jumpOnStartVelocity = false;
				jumpTimePassed += Time.deltaTime;
			}else{
				jumpStartTimePassed += Time.deltaTime;
				jumpTimePassed += Time.deltaTime;
			}
		}

		if( !jumpVelocityZero && ( (!jumpOnStartVelocity && !jumpPressed) || touchingCeiling) ){
			Vector2 velocity = rbody2d.velocity;
			velocity.y = 0f;
			rbody2d.velocity = velocity;
			
			jumpTimePassed = 0f;
			jumpStartTimePassed = 0f;

			jumpVelocityZero = true;
		}

#if UNITY_EDITOR
		/*on touch pressed. subtitute axisraw for -1 or 1*/
		if(Input.GetAxis("Horizontal") != 0){
			rbody2d.velocity = new Vector2( (Vector2.right.x * Input.GetAxisRaw("Horizontal") * walkSpeed), rbody2d.velocity.y );
		}
		
		if(Input.GetButtonDown("Jump")){
			JumpButtonStart();
		}
		
		/*on touch status endtouch*/
		if(Input.GetButtonUp("Jump")){
			JumpButtonEnd();
		}
#endif
	}

	void FixedUpdate(){

		//start time
		if(jumpOnStartVelocity && !jumpVelocityZero && jumpStartTimePassed < jumpStartTime){
			Vector2 velocity = rbody2d.velocity;
			velocity.y = jumpStartForce;
			rbody2d.velocity = velocity;
		}

		/*ontouch status = pressed*/
		if(jumpPressed && jumpTimePassed <= jumpTime && !jumpVelocityZero && jumpStartTimePassed >= jumpStartTime){
			Vector2 velocity = rbody2d.velocity;
			velocity.y = jumpMidForce;
			rbody2d.velocity = velocity;
		}

		animator.SetFloat(hashAnimatorCharacter.xVelocity, rbody2d.velocity.x);
		animator.SetFloat(hashAnimatorCharacter.yVelocity, rbody2d.velocity.y);

		if(rbody2d.velocity.y != 0f && grounded){
			setGrounded(false);
		}

		if(!grounded && rbody2d.velocity.y > -0.1f && rbody2d.velocity.y < 0.1f && touchingGround){
			setGrounded(true);
			
			jumpTimePassed = 0f;
			jumpStartTimePassed = 0f;
		}
	}

	public void JumpButtonStart(){
		if(grounded){
			jumpTimePassed = 0f;
			jumpStartTimePassed = 0f;

			jumpPressed = true;
			jumpOnStartVelocity = true;
			jumpVelocityZero = false;
			
			Vector2 velocity = rbody2d.velocity;
			velocity.y = jumpStartForce;
			rbody2d.velocity = velocity;
		}
	}

	public void JumpButtonEnd(){
		jumpPressed = false;
	}

	public void ChangeWeapon(int weaponNumber){
		if(allWeapons.Length > weaponNumber){
			activeWeapon = allWeapons[weaponNumber];
			for(int i=0; i<allWeaponsObjects.Length; i++){
				allWeaponsObjects[i].SetActive(false);
			}
			allWeaponsObjects[weaponNumber].SetActive(true);
		}
	}

	public void StartBButton(){
		activeWeapon.StartBButton();
	}

	public void EndBButton(){
		activeWeapon.EndBButton();
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
	
	public void setTouchingCeiling(bool touchingCeiling){
		this.touchingCeiling = touchingCeiling;
	}
	
	public bool getTouchingCeiling(){
		return touchingCeiling;
	}

	public void setLeftButton(bool LeftButton){
		this.LeftButton = LeftButton;
	}
	public void setRightButton(bool RightButton){
		this.RightButton = RightButton;
	}
}
