using UnityEngine;
using System.Collections;

public class FlyingAttack : AttackState
{
	public bool m_CheckForObstacles;
	public float m_SlashSpeed;
	public float m_DipHeight;

	Vector3 m_SlashDirection;

	float m_InitialHeight;

	protected NavMeshAgent m_Agent;

	bool m_FirstRecovery;

	protected override void StartVirtual ()
	{
		base.StartVirtual ();

		m_Agent = GetComponent<NavMeshAgent>();
	}

	public override bool CanEnterState ()
	{
		bool result = base.CanEnterState();

		if(m_CheckForObstacles && result)
		{
			Vector3 playerDirection = m_TargettedPlayer.transform.position - transform.position;
			playerDirection.y = 0.0f;

			if(!Physics.Raycast (transform.position, playerDirection, playerDirection.magnitude))
			{
				result = true;
			}
			else
			{
				result = false;
			}
		}

		return result;
	}

	protected override void UpdateWindUp ()
	{
		m_SlashDirection = transform.forward;

		m_InitialHeight = transform.position.y;

		m_FirstRecovery = true;
	}

	protected override void UpdateRecovery ()
	{
		if(m_FirstRecovery)
		{
			m_Agent.nextPosition = transform.position;
			
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
