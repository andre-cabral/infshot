using UnityEngine;
using System.Collections;

public class GroundedCheck : MonoBehaviour {

	public PlayerMovement playerMovement;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == Tags.platform){
			if(playerMovement.getRigidbody2D().velocity.y <=0){
				playerMovement.setGrounded(true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == Tags.platform){
			playerMovement.setGrounded(false);
		}
	}
}
