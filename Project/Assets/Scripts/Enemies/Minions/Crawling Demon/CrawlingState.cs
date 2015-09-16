using UnityEngine;
using System.Collections;

public class CrawlingState : MovementState
{
	NavMeshAgent m_Agent;

	protected override void StartVirtual ()
	{
		base.StartVirtual ();

		m_Agent = GetComponent<NavMeshAgent>();
	}

	public override void EnterState ()
	{
		base.EnterState ();

		m_Agent.Resume();
	} 

	public override void ExitState ()
	{
		base.ExitState ();

		m_Agent.Stop ();
	}

	public override void UpdateState ()
	{
		base.UpdateState ();

		if(m_TargettedPlayer != null)
		{
			m_Agent.SetDestination(m_TargettedPlayer.transform.position);
		}

		if(m_Agent.remainingDistance <= m_Agent.stoppingDistance)
		{			
			m_Agent.velocity = Vector3.zero;
		}
	}
}
