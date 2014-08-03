using UnityEngine;
using System.Collections;

public class OrbitScript : MonoBehaviour {

	public float radius;
	public float period;

	private float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;

		if (period != 0) {

			transform.position = new Vector3(radius * Mathf.Cos(time * 2 * Mathf.PI / period),
			                                 0,
			                                 radius * Mathf.Sin(time * 2 * Mathf.PI / period));

		}

		if (time > period) {time = 0;}

	}
}
