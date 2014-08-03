using UnityEngine;
using System.Collections;

public class ParticleFieldScript : MonoBehaviour {

	// all bound between 0 and 100
	public float bass;
	public float mid;
	public float treble;

	private float progress;

	public GameObject audioScriptGameObject;
	private AudioAnalyzer audioAnalyzer;

	// Use this for initialization
	void Start () {
		audioAnalyzer = audioScriptGameObject.GetComponent ("AudioAnalyzer") as AudioAnalyzer;
	}
	
	// Update is called once per frame
	void Update () {

		bass = audioAnalyzer.simpleBands [0];
		mid = audioAnalyzer.simpleBands [1];
		treble = audioAnalyzer.simpleBands [2];

		progress += Time.deltaTime * 0.25f;

		// COLOR
		float r = 1.0f - Mathf.Sin(bass + mid * 2.0f + treble * 3.0f);
		float g = 1.0f - Mathf.Sin(bass * 2.0f + mid * 2.0f + treble);
		float b = 1.0f - Mathf.Sin(bass * 4.0f + mid + treble );

		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(particles);

		for (int i = 0; i < particles.Length ; i ++) {
			ParticleSystem.Particle particle = particles[i];

			// COLOR
			particle.color = new Color32((byte)(r*255),(byte)(g*255),(byte)(b*255),255);
			particle.size = treble * 50.0f;

			// write over the retreived particle
			particles[i] = particle;
		}
		// write over the retreived particle array
		particleSystem.SetParticles(particles,particleSystem.particleCount);

		float localScale = 100.0f + 100.0f * Mathf.Cos (bass);
		gameObject.transform.localScale = new Vector3(localScale,localScale,localScale);
	}
}
