using UnityEngine;
using System.Collections;

// LifeTime handles the countdown to deactivation of objects with a life span
public class LifeTime : MonoBehaviour 
{
	#region Members Open For Designer
	public float m_LifeTime;
	#endregion
	
	#region Private Members
	float m_Timer;
	#endregion
	
	#region Unity Hooks
	void OnEnable()
	{
		m_Timer = m_LifeTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Counts down the time until the object is set inactive
		m_Timer -= Time.deltaTime;
		
		if(m_Timer < 0.0f)
		{
			gameObject.SetActive (false);
		}
	}
	#endregion
}
