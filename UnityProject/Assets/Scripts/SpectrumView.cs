using UnityEngine;
using System.Collections;

public class SpectrumView : MonoBehaviour {

	public GameObject audioScriptGameObject;
	private AudioAnalyzer audioAnalyzer;

	public GameObject[] g;

	// Use this for initialization
	void Start () {
	
		audioAnalyzer = audioScriptGameObject.GetComponent ("AudioAnalyzer") as AudioAnalyzer;

		int bandLength = audioAnalyzer.band.Length;
		g = new GameObject[bandLength - 5];

		for (int i = 0; i < g.Length; i++)
		{
			g[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			g[i].renderer.material.SetColor("_Color", Color.white);
			g[i].transform.position = new Vector3(transform.localPosition.x  -4 + i, transform.localPosition.y, transform.localPosition.z);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		float rotate = Mathf.Sin (Time.time);

		float multFactor = 1.0f;

		for (int k = 0; k < g.Length; k++) 
		{
			Vector3 newScale = new Vector3 (g [k].transform.localScale.x, audioAnalyzer.band [k+4] * 20.0f * multFactor, g [k].transform.localScale.z);
			g [k].transform.localScale = newScale;

			g[k].transform.localEulerAngles = new Vector3 (rotate * audioAnalyzer.simpleBands[1] * 180.0f, g[k].transform.localEulerAngles.y, g[k].transform.localEulerAngles.z);
			multFactor = multFactor * 1.3f;
		}
	}
}
