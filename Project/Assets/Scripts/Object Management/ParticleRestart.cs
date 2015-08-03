using UnityEngine;
using System.Collections;

public class ParticleRestart : MonoBehaviour 
{
	ParticleSystem m_Particles;

	// Use this for initialization
	void Start () 
	{
		m_Particles = GetComponent<ParticleSystem>();
	}
	
	void OnEnable()
	{
		if(m_Particles)
		{
			m_Particles.Clear ();
		}
	}
}
