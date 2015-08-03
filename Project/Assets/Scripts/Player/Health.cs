using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public int m_MaxHealth;
	int m_CurrentHealth;

	// Use this for initialization
	void Start () 
	{
		m_CurrentHealth = m_MaxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Revive()
	{
		m_CurrentHealth = (int) (m_MaxHealth * GameConstants.REVIVAL_HEALTH_MULTIPLIER);

		Resurrect ();
	}

	public void Damage(int damageAmount)
	{
		m_CurrentHealth -= damageAmount;

		if(m_CurrentHealth <= 0)
		{
			m_CurrentHealth = 0;

			Die ();
		}
	}

	public void Heal(int amount)
	{
		m_CurrentHealth += amount;

		if(m_CurrentHealth > m_MaxHealth)
		{
			m_CurrentHealth = m_MaxHealth;
		}
	}

	public bool NeedsHealing()
	{
		return m_CurrentHealth < m_MaxHealth;
	}

	void Die()
	{

	}

	void Resurrect()
	{

	}
}
