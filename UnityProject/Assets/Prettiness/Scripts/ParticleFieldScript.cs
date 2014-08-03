using UnityEngine;
using System.Collections;

public class ParticleFieldScript : MonoBehaviour {

	// all bound between 0 and 100
	public float bass;
	public float mid;
	public float treble;

	private System.Random random = new System.Random();

	public GameObject audioScriptGameObject;
	private AudioAnalyzer audioAnalyzer;

	/*private Light[] lights;*/ 

	// Use this for initialization
	void Start () {
		audioAnalyzer = audioScriptGameObject.GetComponent ("AudioAnalyzer") as AudioAnalyzer;
		/*lights = new Light[0];*/
	}
	
	// Update is called once per frame
	void Update () {

		bass = audioAnalyzer.simpleBands [0];
		mid = audioAnalyzer.simpleBands [1];
		treble = audioAnalyzer.simpleBands [2];

		/*
		// clear lights
		for (uint i = 0; i < lights.Length; i++) {
			Light light = lights[i];
			Destroy (light);
		}
		lights = new Light[particleSystem.particleCount];
		*/

		// COLOR
		float r = 1.0f - Mathf.Sin(bass + treble * 2.0f);
		float g = 1.0f - Mathf.Sin(mid + treble);
		float b = 1.0f - Mathf.Sin(treble );

		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(particles);

		for (int i = 0; i < particles.Length ; i ++) {
			ParticleSystem.Particle particle = particles[i];

			// COLOR
			particle.color = new Color32((byte)(r*255),(byte)(g*255),(byte)(b*255),255);
			particle.size = treble - 0.75f;

			/*
			// add a new light
			Light light = new Light();
			light.type = LightType.Point;
			light.transform.position = particle.position;
			lights[i] = light;
			*/

			// write over the retreived particle
			particles[i] = particle;
		}
		// write over the retreived particle array
		particleSystem.SetParticles(particles,particleSystem.particleCount);

		// set a random velocity for the next particle
		particleSystem.startSpeed = random.Next(0,10) + 2.2f;

		// scale to match the bass
		float localScale = 222.0f + 222.0f * Mathf.Cos (bass + treble);
		particleSystem.transform.localScale = new Vector3(localScale,localScale * 0.5f,localScale);
	}
}
