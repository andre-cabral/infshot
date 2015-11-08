using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class DestroyWhenOffScreen2d : MonoBehaviour {

	public int offset = 6;
	private Camera cam;
	private Transform objectTransform;
	private float spriteWidthHalf = 0f;
	private float spriteHeightHalf = 0f;

	float cameraHorizontalSizeWithOffset = 0f;
	float cameraVerticalSizeWithOffset = 0f;
	
	void Awake(){
		cam = Camera.main;
		objectTransform = transform;
	}
	
	void Start () {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteWidthHalf = (spriteRenderer.bounds.max.x - spriteRenderer.bounds.min.x)/2;
		spriteHeightHalf = (spriteRenderer.bounds.max.y - spriteRenderer.bounds.min.y)/2;

		//half the horizontal size of the screen
		//used to discover the camera border position
		//(because the camera.position return the camera center position)
		cameraHorizontalSizeWithOffset = (cam.orthographicSize * Screen.width/Screen.height) + offset;
		cameraVerticalSizeWithOffset = (cam.orthographicSize * 2f) + offset;
	}
	
	void Update () {
		CheckToDestroyThisObject();
	}

	void CheckToDestroyThisObject(){
		if(objectTransform.position.x + spriteWidthHalf < cam.transform.position.x  - cameraHorizontalSizeWithOffset ){
			DestroyThisObject();					
		}				

		if(objectTransform.position.x - spriteWidthHalf > cam.transform.position.x + cameraHorizontalSizeWithOffset ){
			DestroyThisObject();
		}

		if(objectTransform.position.y + spriteHeightHalf < cam.transform.position.y  - cameraVerticalSizeWithOffset ){
			DestroyThisObject();					
		}				

		if(objectTransform.position.y - spriteWidthHalf > cam.transform.position.y + cameraVerticalSizeWithOffset ){
			DestroyThisObject();
		}
	}

	void DestroyThisObject(){
		Destroy(gameObject);
	}
}
