using UnityEngine;
using System.Collections;

public class Health : PickUp 
{
	public int m_MaxHealth;
	float m_CurrentHealth;

	public float m_RevivingTime;
	float m_Timer;

	public GameObject m_BloodParticlesPrefab;

	StateMachine m_StateMachine;
	KnockbackState m_Knockback;
	ObjectPool m_ObjectPool;

	// Use this for initialization
	protected override void Start () 
	{
		m_CurrentHealth = m_MaxHealth;

		m_StateMachine = GetComponent<StateMachine>();
		m_Knockback = GetComponent<KnockbackState>();
		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		if(m_Timer > 0.0f)
		{
			m_Timer -= Time.deltaTime;

			if(m_Timer <= 0.0f)
			{
				Revive();
			}
		}
	}

	protected override bool CanBeCollectedVirtual (Interaction interaction)
	{
		return (m_CurrentHealth <= 0.0f) ? true : false;
	}

	protected override void CollectionComplete ()
	{

	}

	protected override void CollectVirtual (Interaction interaction)
	{
		if(m_Timer <= 0.0f)
		{
			m_Timer = (interaction != null) ? m_RevivingTime : 0.0f;
		}
		else
		{
			m_Timer = (interaction == null) ? 0.0f : m_Timer;
		}
	}

	void Revive()
	{
		m_CurrentHealth = m_MaxHealth * GameConstants.REVIVAL_HEALTH_MULTIPLIER;

		Resurrect ();
	}

	public void Damage(float damageAmount)
	{
		m_CurrentHealth -= damageAmount;

		if(m_BloodParticlesPrefab)
		{
			m_ObjectPool.Instantiate(m_BloodParticlesPrefab, transform.position, Quaternion.identity);
		}

		if(m_CurrentHealth <= 0.0f)
		{
			m_CurrentHealth = 0.0f;

			Die ();
		}
	}

	public void Damage(Vector3 projectileVelocity, float damageAmount)
	{
		m_StateMachine.Interrupt();
		m_Knockback.SetUpKnockback(projectileVelocity , damageAmount);

		Damage (damageAmount);
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
