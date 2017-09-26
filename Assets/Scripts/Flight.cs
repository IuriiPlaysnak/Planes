using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour {

	[SerializeField]
	private float _minSpeed = 1f;

	[SerializeField]
	private float _maxSpeed = 5f;

	[SerializeField]
	private GameObject _player;

	private float _currentSpeed = 1f;
	private float _newSpeed;
	private Vector3 _rotationVelocity;
	private Vector3 _newRotationVelocity;

	private GameObject _bulletPrefab;

	// Use this for initialization
	void Start () {
	
		_rotationVelocity = new Vector3 ();

		_bulletPrefab = Resources.Load ("Prefabs/Bullet") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 rotations = OVRInput.Get (OVRInput.RawAxis2D.RThumbstick);

		float xA = Mathf.Abs (rotations.y);
		float zA = Mathf.Abs (rotations.x);
		float d = xA - zA;
		float threshold = 0.1f;

		float xRot = rotations.y; 
		float zRot = -rotations.x;

		if (d > threshold) {
			zRot = 0f;
		} else if (d < -threshold)
			xRot = 0f;

		_newRotationVelocity = new Vector3 (xRot, 0f, zRot * 1.5f);
		_rotationVelocity = Vector3.Lerp (_rotationVelocity, _newRotationVelocity, 0.05f);

		transform.Rotate (_rotationVelocity);

		float acceleration = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger);
		_newSpeed = _minSpeed + (_maxSpeed - _minSpeed) * acceleration;
		_currentSpeed = Mathf.Lerp (_currentSpeed, _newSpeed, 0.01f);

		transform.position += (_currentSpeed * Time.deltaTime * transform.forward);

		if (OVRInput.GetDown ( OVRInput.Button.PrimaryIndexTrigger )) {

			GameObject bullet = GameObject.Instantiate (_bulletPrefab);
			bullet.transform.rotation = transform.rotation;
			bullet.transform.position = transform.position + transform.forward * 0.1f;
		}

		if (OVRInput.GetDown (OVRInput.RawButton.B)) {

			transform.rotation = Quaternion.LookRotation (_player.transform.forward);
			transform.position = _player.transform.position + _player.transform.forward * 2f;
			transform.Translate (new Vector3 (0f, -_player.transform.position.y + 0.1f, 0f));
			_currentSpeed = 0f;
			_newSpeed = 0f;
			_newRotationVelocity = Vector3.zero;
			_rotationVelocity = Vector3.zero;
		}
	}
}