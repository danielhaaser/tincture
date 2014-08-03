using UnityEngine;
using System.Collections;

public class AudioAnalyzer : MonoBehaviour {

	public int numberOfSamples = 8192;  // Min: 64, Max: 8192, must be power of 2

	public AudioSource aSource;

	public float[] freqData;
	public float[] band;
	public float[] simpleBands;

	public GameObject[] g;
	public GameObject[] simpleBandObjects;

	// Use this for initialization
	void Start () 
	{
		freqData = new float[numberOfSamples];

		simpleBands = new float[3];

		int n = freqData.Length;

		//  check that n is a power of 2 in 2's complement
		//if ((n (n - 1)) != 0) 
		//{
		//	Debug.LogError("freqData length " + n + " is not a power of 2! Min: 64, Max: 8192");
		//	return;
		//}

		int k = 0;

		for (int j = 0; j < freqData.Length; j++) 
		{
			n = n / 2;
			if (n <= 0) break;
			k++;
		}

		Debug.Log ("Value of k is " + k);

		band = new float[k+1];
		g = new GameObject[k+1];

		simpleBandObjects = new GameObject[3];

		for (int i = 0; i < band.Length; i++)
		{
			band[i] = 0;
			g[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			g[i].renderer.material.SetColor("_Color", Color.white);
			g[i].transform.position = new Vector3(-6 + i, 0, 0);
		}

		for (int i = 0; i < 3; i++)
		{
			simpleBandObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			simpleBandObjects[i].renderer.material.SetColor("_Color", Color.green);
			simpleBandObjects[i].transform.position = new Vector3 (-2 + (2 * i), -4, 0);
		}

		InvokeRepeating ("check", 0.0f, 1.0f / 30.0f);  // 30 fps update
	}

	private void check()
	{
		aSource.GetSpectrumData(freqData, 0, FFTWindow.Hanning);
		
		int k = 0;
		int crossover = 2;

		simpleBands [0] = 0.0f;
		simpleBands [1] = 0.0f;
		simpleBands [2] = 0.0f;

		Debug.Log("Frequency Data Length: " + freqData.Length);

		for (int i = 4; i < freqData.Length; i++)
		{
			float d = freqData[i];
			float b = band[k];

			float[] totalBins = new float[3];

			if (crossover > 32 && crossover <= 256)
			{
				totalBins[0] += 1.0f;
				simpleBands[0] += d; 
			}
			else if (crossover > 256 && crossover <= 2048)
			{
				totalBins[1] += 1.0f;
				simpleBands[1] += d;
			}
			else 
			{
				totalBins[2] += 1.0f;
				simpleBands[2] += d;
			}
			
			// find the max as the peak value in that frequency band.
			band[k] = ( d > b )? d : b;
			
			if (i > (crossover-3) )
			{
				k++;
				crossover *= 2;   // frequency crossover point for each band.
				Debug.Log("New Crossover: " + crossover);
				Vector3 newScale = new Vector3 (g[k].transform.localScale.x, band[k]*32, g[k].transform.localScale.z);
				g[k].transform.localScale = newScale;
				band[k] = 0;
			}
		}

		for (int z = 0; z < 3; z++)
		{
			simpleBands[z] = simpleBands[z] / 4.0f;
			simpleBands[z] = Mathf.Clamp(simpleBands[z], 0.0f, 1.0f);

			Vector3 newScale = new Vector3 (simpleBandObjects[z].transform.localScale.x, simpleBands[z] * 4.0f, simpleBandObjects[z].transform.localScale.z);
			simpleBandObjects[z].transform.localScale = newScale;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
