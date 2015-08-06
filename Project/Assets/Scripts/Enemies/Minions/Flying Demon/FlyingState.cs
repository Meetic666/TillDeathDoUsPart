using UnityEngine;
using System.Collections;

public class FlyingState : MovementState
{	
	Vector3 m_Shortcut;

	NavMeshAgent m_Agent;

	protected override void Start ()
	{
		base.Start ();
		
		m_Agent = GetComponent<NavMeshAgent>();
		m_Agent.Stop ();
	}

	public override void UpdateState ()
	{
		base.UpdateState ();		
		
		Vector3 targetForward = (m_TargettedPlayer.transform.position - transform.position).normalized;
		targetForward.y = 0.0f;
		
		transform.forward = Vector3.Slerp (transform.forward, targetForward, m_Agent.angularSpeed * Mathf.Deg2Rad * Time.deltaTime);

		m_Agent.SetDestination(m_TargettedPlayer.transform.position);

		CheckForShortcut();

		Vector3 displacement = m_Agent.speed * Time.deltaTime * (m_Shortcut - transform.position).normalized;

		transform.position += displacement;
		
		m_Agent.updatePosition = false;
	}

	void CheckForShortcut()
	{
		foreach(Vector3 waypoint in m_Agent.path.corners)
		{
			Vector3 position = waypoint;
			position.y = transform.position.y;

			RaycastHit hitInfo;

			if(!Physics.SphereCast (transform.position, m_Agent.radius, position - transform.position, out hitInfo))
			{
				m_Shortcut = position;
			}
		}
	}
}
