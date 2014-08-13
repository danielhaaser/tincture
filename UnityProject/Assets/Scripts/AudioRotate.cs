using UnityEngine;
using System.Collections;

public class AudioRotate : MonoBehaviour {

	public GameObject audioScriptGameObject;
	private AudioAnalyzer audioAnalyzer;
	private float yAngleStart;


	// Use this for initialization
	void Start () {
		yAngleStart = transform.localEulerAngles.y;
		audioAnalyzer = audioScriptGameObject.GetComponent ("AudioAnalyzer") as AudioAnalyzer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float mid = audioAnalyzer.simpleBands [1];
		float treble = audioAnalyzer.simpleBands [2];

		//if (this.transform.localEulerAngles.y > 0.0f) 
		//{
			float amountToRotate = this.transform.localEulerAngles.y * 0.6f;
			this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, yAngleStart - amountToRotate, this.transform.localEulerAngles.z);
		//}
		//else 
		//{
			if (mid > 0.7f && treble > 0.7f) 
			{
				System.Random random = new System.Random ();
				float randomFloat = (float) random.NextDouble ();
				float randomMultiplier = (float) random.NextDouble () * 4.0f;
				float rotationVector = randomFloat * randomMultiplier;

				this.transform.localEulerAngles = new Vector3 (this.transform.localEulerAngles.x, rotationVector, this.transform.localEulerAngles.z);
			}
		//}

	}
}
