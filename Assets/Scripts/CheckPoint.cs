using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	[SerializeField]
	private GameObject _player;

	[SerializeField]
	private Transform _transitionTo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		_player.transform.position = _transitionTo.position;

		Flight airplane = GameObject.FindObjectOfType<Flight> ();

		Quaternion newRotation = Quaternion.LookRotation (-_player.transform.position, Vector3.up);
		_player.transform.rotation = Quaternion.Euler (new Vector3(0f, newRotation.eulerAngles.y, 0f));
	}
}
