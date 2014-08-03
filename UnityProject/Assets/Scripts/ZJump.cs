using UnityEngine;
using System.Collections;

public class ZJump : MonoBehaviour {

	public GameObject audioScriptGameObject;
	private AudioAnalyzer audioAnalyzer;

	// Use this for initialization
	void Start () {
	
		audioAnalyzer = audioScriptGameObject.GetComponent ("AudioAnalyzer") as AudioAnalyzer;

	}
	
	// Update is called once per frame
	void Update () {
	
		float bassPower = audioAnalyzer.simpleBands [0];

		Vector3 newPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, bassPower * 2.0f);
		this.transform.localPosition = newPosition;

		float squash = 1.0f;
		if (bassPower > 0.3f)
		{
			squash =  1.0f - (bassPower -0.3f);
		}
		Vector3 newScale = new Vector3 (this.transform.localScale.x, squash, squash);
		this.transform.localScale = newScale;

	}
}
