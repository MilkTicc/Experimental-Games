using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (PlayerMotor))]

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 3f;

	public float mouseSensitivity =1f;
	private PlayerMotor motor;
	bool isGrounded = false;
	//Collider collider;

	void Start(){
		motor = GetComponent<PlayerMotor> ();
		//collider = GetComponent<BoxCollider> ();
	}

	//void OnCollisionEnter (Collision col)
	//{
	//	if (col.gameObject.tag == "Ground")

	//		isGrounded = true;
	//}

	//void OnCollisionExit(Collision col){
	//	if(col.gameObject.tag=="Ground")
	//	isGrounded = false;
	//}

	void OnTriggerEnter(Collider col){
			if(col.gameObject.tag=="Ground")
			isGrounded = true;

	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Ground")
		{
			isGrounded = false; 
			Debug.Log (col.gameObject.name);
		}

	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Tab))
		{Debug.Log (isGrounded); }
		if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
			motor.Jump ();
			Debug.Log ("jumped");
		}
		
		float _xMov = Input.GetAxis ("Vertical");
		float _zMov = Input.GetAxis ("Horizontal");

		Vector3 _moveHori = transform.right * _xMov;
		Vector3 _moveVerti =- transform.forward * _zMov;

		Vector3 _velo = (_moveHori + _moveVerti) * speed;
		motor.Move (_velo);

		float _yRot = Input.GetAxisRaw ("Mouse X");

		Vector3 _rotation = new Vector3 (0, _yRot, 0) * mouseSensitivity;
		motor.Rotate (_rotation);

		float _xRot = Input.GetAxisRaw ("Mouse Y");

		Vector3 _camRotation = new Vector3 (_xRot, 0, 0) * mouseSensitivity;

		motor.RotateCam (_camRotation);
	}

}
