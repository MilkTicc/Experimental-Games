 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {
	Vector3 velo = Vector3.zero;
	Vector3 rotation = Vector3.zero;
	Vector3 camRotation = Vector3.zero;
	private Rigidbody rb;
	[SerializeField]
	Camera pCam;
	bool isJumping = false;
	[SerializeField]
	float jumpForce = 120f;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velo){
		velo = _velo;
	}

	void PerformMovement(){
		if(velo != Vector3.zero){
			rb.MovePosition (rb.position + velo * Time.deltaTime);
		}
	}

	public void Rotate(Vector3 _rot){
		rotation = _rot;
	}

	void PerformRotation(){
		if(rotation != Vector3.zero){
			rb.MoveRotation (rb.rotation * Quaternion.Euler (rotation));
		}
		pCam.transform.Rotate (-camRotation);
	}

	public void RotateCam(Vector3 _rotcam){
		camRotation = _rotcam;
	}

	public void Jump(){
		isJumping = true;
	}

	public void PerformJump(){
		if(isJumping)
		{
			rb.AddForce (new Vector3 (0, jumpForce, 0));
			isJumping = false;
		}
	}

	void FixedUpdate(){
		PerformMovement ();
		//PerformRotation ();
		PerformJump ();
	}
}
