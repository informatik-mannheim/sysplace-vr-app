using UnityEngine;
using System.Collections;

public class RotateInput : MonoBehaviour {

	private float h;
	private float horozontalSpeed;

	// Use this for initialization
	void Start () {
		horozontalSpeed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//rotate by input
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch (0);
			
			if (touch.phase == TouchPhase.Moved) {
				h = horozontalSpeed * touch.deltaPosition.x;
				transform.Rotate (0, -h, 0, Space.World);
			}
		} else {
			//auto rotate
			transform.Rotate (0,-2*Time.deltaTime,0); //rotates 2 degrees per second around z axis
		}
	}
}

