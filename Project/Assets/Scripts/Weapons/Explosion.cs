using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
	public float m_MaxRange;
	public float m_ExpansionTime;

	float m_Timer;

	AreaOfDamage m_AOD;

	// Use this for initialization
	void Start () 
	{
		m_AOD = GetComponent<AreaOfDamage>();
	}

	void OnEnable()
	{
		m_Timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Timer += Time.deltaTime;

		if(m_Timer > m_ExpansionTime)
		{
			m_Timer = m_ExpansionTime;
		}

		m_AOD.m_Range = Mathf.Lerp (0.0f, m_MaxRange, m_Timer / m_ExpansionTime);
	}
}
