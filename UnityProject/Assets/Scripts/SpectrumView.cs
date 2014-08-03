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
		g = new GameObject[bandLength];

		for (int i = 0; i < g.Length; i++)
		{
			g[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			g[i].renderer.material.SetColor("_Color", Color.white);
			g[i].transform.position = new Vector3(transform.localPosition.x  -6 + i, transform.localPosition.y, transform.localPosition.z);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log ("GameObjects: " + g.Length);
		Debug.Log ("Bands: " + audioAnalyzer.band.Length);

		for (int k = 0; k < g.Length; k++) 
		{
			Vector3 newScale = new Vector3 (g [k].transform.localScale.x, audioAnalyzer.band [k] * 32, g [k].transform.localScale.z);
			g [k].transform.localScale = newScale;
		}
	}
}
