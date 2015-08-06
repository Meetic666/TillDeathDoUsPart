using UnityEngine;
using System.Collections;

public class KnockbackState : InterruptionState
{
	public float m_DamageForceRatio;

	float m_InitialHeight;

	NavMeshAgent m_Agent;
	Rigidbody m_Rigidbody;

	void Start()
	{
		m_Agent = GetComponent<NavMeshAgent>();
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	public override void EnterState ()
	{
		if(m_Agent)
		{
			m_Agent.enabled = false;
		}

		if(m_Rigidbody)
		{
			m_Rigidbody.isKinematic = false;
		}

		m_InitialHeight = transform.position.y;
	}

	public override bool CanExitState ()
	{
		return (m_Rigidbody && (m_Rigidbody.velocity == Vector3.zero || (m_Rigidbody.velocity.y < 0.0f && transform.position.y <= m_InitialHeight)));
	}

	public override void ExitState ()
	{
		Vector3 newPosition = transform.position;
		newPosition.y = m_InitialHeight;
		transform.position = newPosition;

		if(m_Agent)
		{
			m_Agent.enabled = true;
		}
		
		if(m_Rigidbody)
		{
			m_Rigidbody.isKinematic = true;
		}
	}

	public void SetUpKnockback(Vector3 velocity, float damage)
	{
		velocity.y = 0.0f;

		if(m_Rigidbody)
		{
			m_Rigidbody.AddForce(velocity + Vector3.up * damage * m_DamageForceRatio * 0.5f);
		}
	}
}
