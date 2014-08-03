using UnityEngine;
using System.Collections;

public class ParticleFieldScript : MonoBehaviour {

	// all bound between 0 and 100
	public int bass;
	public int mid;
	public int treble;

	private float progress;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		progress += Time.deltaTime * 0.25f;

		// COLOR
		float r = (float)(bass + mid * 2 + treble * 3) / 600.0f;
		float g = (float)(bass * 2 + mid * 2 + treble) / 500.0f;
		float b = (float)(bass * 4 + mid + treble ) / 600.0f;

		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(particles);

		for (int i = 0; i < particles.Length ; i ++) {
			ParticleSystem.Particle particle = particles[i];

			// COLOR
			particle.color = new Color32((byte)(r*255),(byte)(g*255),(byte)(b*255),255);
			particle.size = treble * 0.05f;

			// write over the retreived particle
			particles[i] = particle;
		}
		// write over the retreived particle array
		particleSystem.SetParticles(particles,particleSystem.particleCount);

		float localScale = bass * 5.5f;
		gameObject.transform.localScale = new Vector3(bass,bass,bass);
	}
}
