using UnityEngine;
using System.Collections;

public class FlyingAttack : AttackState
{
	public float m_SlashSpeed;
	public float m_DipHeight;

	Vector3 m_SlashDirection;

	float m_InitialHeight;

	NavMeshAgent m_Agent;

	bool m_FirstRecovery;

	protected override void Start ()
	{
		base.Start ();

		m_Agent = GetComponent<NavMeshAgent>();
	}

	public override bool CanEnterState ()
	{
		bool result = base.CanEnterState();

		if(result)
		{
			Vector3 playerDirection = m_TargettedPlayer.transform.position - transform.position;
			playerDirection.y = 0.0f;

			result &= !Physics.Raycast (transform.position, playerDirection);
		}

		return result;
	}

	protected override void UpdateWindUp ()
	{
		m_Agent.enabled = false;

		m_SlashDirection = transform.forward;

		m_InitialHeight = transform.position.y;

		m_FirstRecovery = true;
	}

	protected override void UpdateRecovery ()
	{
		if(m_FirstRecovery)
		{
			m_Agent.enabled = true;
			
			m_Agent.updatePosition = true;
		}

		transform.position += m_SlashSpeed * m_SlashDirection * Time.deltaTime;

		float percentage = m_RecoveryTimer / m_AttackRecovery;

		percentage = Mathf.Clamp01(percentage);

		float dip = m_DipHeight * Mathf.Sin(percentage * Mathf.PI);

		Vector3 newPosition = transform.position;
		newPosition.y = m_InitialHeight - dip;
		transform.position = newPosition;
	}
}
