using UnityEngine;
using System.Collections;

public class FlyingState : MovementState
{
	public float m_InRangeOscillationSpeed;
	public float m_InRangeOscillationAmplitude;

	float m_Timer;

	Vector3 m_Shortcut;

	NavMeshAgent m_Agent;

	AttackState m_Attack;

	protected override void Start ()
	{
		base.Start ();
		
		m_Agent = GetComponent<NavMeshAgent>();
		m_Agent.Stop ();

		m_Attack = GetComponent<AttackState>();
	}

	public override void UpdateState ()
	{
		base.UpdateState ();

		m_Timer += Time.deltaTime * m_InRangeOscillationSpeed;

		Vector3 destination = CalculateDestination();

		Vector3 targetForward = (m_TargettedPlayer.transform.position - transform.position).normalized;
		targetForward.y = 0.0f;
		
		transform.forward = Vector3.Slerp (transform.forward, targetForward, m_Agent.angularSpeed * Mathf.Deg2Rad * Time.deltaTime);

		m_Agent.nextPosition = transform.position;
		m_Agent.SetDestination(destination);

		CheckForShortcut();

		m_Agent.updatePosition = false;

		Vector3 displacement = m_Agent.speed * Time.deltaTime * (m_Shortcut - transform.position).normalized;

		transform.position += displacement;
		
		m_Agent.nextPosition = transform.position;
	}

	void CheckForShortcut()
	{
		foreach(Vector3 waypoint in m_Agent.path.corners)
		{
			Vector3 position = waypoint;
			position.y = transform.position.y;

			RaycastHit hitInfo;

			Vector3 shortcutOffset = position - transform.position;

			if(!Physics.SphereCast (transform.position, m_Agent.radius, shortcutOffset, out hitInfo, shortcutOffset.magnitude))
			{
				m_Shortcut = position;
			}
		}
	}

	Vector3 CalculateDestination()
	{
		Vector3 destination = Vector3.zero;

		Vector3 playerDirection = m_TargettedPlayer.transform.position - transform.position;
		playerDirection.y = 0.0f;
		playerDirection *= -1.0f;

		Vector3 playerOffset = playerDirection.normalized * m_Attack.m_AttackRange;

		RaycastHit hitInfo;

		if(!Physics.SphereCast(m_TargettedPlayer.transform.position, m_Agent.radius, playerOffset, out hitInfo, playerOffset.magnitude))
		{
			destination = m_TargettedPlayer.transform.position + playerOffset;
		}
		else
		{
			destination = m_TargettedPlayer.transform.position + playerOffset.normalized * hitInfo.distance;
		}

		destination.y = transform.position.y;
		
		Vector3 forward = -playerDirection.normalized;
		Vector3 right = Vector3.Cross(forward, Vector3.up);

		destination += right * m_InRangeOscillationAmplitude * Mathf.Sin(m_Timer) + forward * m_InRangeOscillationAmplitude * (1.0f + Mathf.Cos(m_Timer));

		return destination;
	}
}
