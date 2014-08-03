using UnityEngine;
using System.Collections;

public class AudioAnalyzer : MonoBehaviour {

	public int numberOfSamples = 8192;  // Min: 64, Max: 8192, must be power of 2

	public AudioSource aSource;

	public float[] freqData;
	public float[] band;
	public float[] simpleBands;


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

		band = new float[k+1];

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
				band[k] = 0;
			}
		}

		for (int z = 0; z < 3; z++)
		{
			simpleBands[z] = simpleBands[z] / 4.0f;
			simpleBands[z] = Mathf.Clamp(simpleBands[z], 0.0f, 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
