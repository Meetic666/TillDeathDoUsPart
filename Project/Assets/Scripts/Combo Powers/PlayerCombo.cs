using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombo : MonoBehaviour 
{
	public float m_OtherPlayerDetectionRange;
	public float m_InputDelay;

	public float m_RechargeTime;

	float m_Timer;

	float m_InputTimer;

	public bool IsReady
	{
		get
		{
			return (m_Timer <= 0.0f && m_Input.ComboPower);
		}
	}

	PlayerCombo[] m_PlayerCombos;
	PlayerInput m_Input;
	ComboPowerManager m_ComboPowerManager;

	// Use this for initialization
	void Start () 
	{
		m_PlayerCombos = FindObjectsOfType<PlayerCombo>();
		m_ComboPowerManager = FindObjectOfType<ComboPowerManager>();

		m_Input = GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Timer -= Time.deltaTime;

		if(IsReady)
		{
			if(m_InputTimer > 0.0f)
			{
				m_InputTimer -= Time.deltaTime;

				if(m_InputTimer <= 0.0f)
				{
					EngagePower();
				}
			}
		}
		else
		{
			m_InputTimer = m_InputDelay;
		}
	}

	public void SetRecharging()
	{
		m_Timer = m_RechargeTime;
	}

	void EngagePower()
	{
		List<PlayerCombo> playersInRange = new List<PlayerCombo>();
		
		Vector3 centerOfCombo = Vector3.zero;
		
		foreach(PlayerCombo playerCombo in m_PlayerCombos)
		{
			if(Vector3.Distance(playerCombo.transform.position, transform.position) <= m_OtherPlayerDetectionRange
			   && playerCombo.IsReady)
			{
				playersInRange.Add (playerCombo);
				
				centerOfCombo += playerCombo.transform.position;
			}
		}
		
		if(playersInRange.Count > 0)
		{
			centerOfCombo /= playersInRange.Count;
			
			m_ComboPowerManager.UseCombo(centerOfCombo, playersInRange);
		}
	}
}
