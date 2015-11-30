using UnityEngine;
using System.Collections;

public class SpritesTint : MonoBehaviour {

	public Color colorToTint;
	public SpriteRenderer[] spritesRenderers;
	Color[] startColor;

	void Awake () {
		startColor = new Color[spritesRenderers.Length];
		for(int i=0; i<spritesRenderers.Length; i++){
			startColor[i] = spritesRenderers[i].color;
		}
	}
	
	public void StartTint () {
		for(int i=0; i<spritesRenderers.Length; i++){
			spritesRenderers[i].color = colorToTint;
		}
	}
	public void EndTint () {
		for(int i=0; i<spritesRenderers.Length; i++){
			spritesRenderers[i].color = startColor[i];
		}
	}

}
