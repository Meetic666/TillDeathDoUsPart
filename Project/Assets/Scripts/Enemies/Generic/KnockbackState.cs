using UnityEngine;
using System.Collections;

public class KnockbackState : InterruptionState
{
	public float m_DamageForceRatio;

	NavMeshAgent m_Agent;
	Rigidbody m_Rigidbody;

	void Start()
	{
		m_Agent = GetComponent<NavMeshAgent>();
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	public override void EnterState ()
	{
		m_Agent.enabled = false;
		m_Rigidbody.isKinematic = false;
	}

	public override bool CanExitState ()
	{
		return m_Rigidbody.velocity == Vector3.zero;
	}

	public override void ExitState ()
	{
		m_Agent.enabled = true;
		m_Rigidbody.isKinematic = true;
	}

	public void SetUpKnockback(Vector3 velocity, float damage)
	{
		velocity.y = 0.0f;

		m_Rigidbody.AddForce(velocity + Vector3.up * damage * m_DamageForceRatio * 0.5f);
	}
}
