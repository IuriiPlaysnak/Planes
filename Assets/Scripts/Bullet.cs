using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField]
	private float _speed;

	[SerializeField]
	private float _lifetime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private bool _isBlast;

	void Update () {
		_lifetime -= Time.deltaTime;
		transform.position += transform.forward * _speed * Time.deltaTime;

		if (_isBlast) {

			_speed = Mathf.Lerp (_speed, 0f, 0.05f);

			if (_lifetime < 0f)
				Destroy (this.gameObject);
			return;
		}

		if (_lifetime < 0) {
			ParticleSystem particles = gameObject.GetComponent<ParticleSystem> ();
			particles.Play ();
			_lifetime = 1f;
			_isBlast = true;
		}
	}
}
