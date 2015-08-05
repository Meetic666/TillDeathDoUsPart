using UnityEngine;
using System.Collections;

public class HorseManager : MonoBehaviour 
{
	public float m_StampedeDuration;
	float m_Timer;

	Horse[] m_Horses;

	// Use this for initialization
	void Start () 
	{
		m_Horses = GetComponentsInChildren<Horse>();

		StartStampede();
	}

	void OnEnable()
	{
		StartStampede();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Timer > 0.0f)
		{
			m_Timer -= Time.deltaTime;

			if(m_Timer <= 0.0f)
			{
				StopStampede();
			}
		}
	}

	void StartStampede()
	{
		m_Timer = m_StampedeDuration;
		
		if(m_Horses != null)
		{
			foreach(Horse horse in m_Horses)
			{
				horse.StartRunning();
			}
		}
	}

	void StopStampede()
	{
		if(m_Horses != null)
		{
			foreach(Horse horse in m_Horses)
			{
				horse.StopRunning();
			}
		}
	}
}
