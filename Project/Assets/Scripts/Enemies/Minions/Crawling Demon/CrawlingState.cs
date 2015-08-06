using UnityEngine;
using System.Collections;

public class CrawlingState : MovementState
{
	NavMeshAgent m_Agent;

	protected override void Start ()
	{
		base.Start ();

		m_Agent = GetComponent<NavMeshAgent>();
	}

	public override void EnterState ()
	{
		base.EnterState ();

		m_Agent.Resume();
	} 

	public override void ExitState ()
	{
		m_Agent.Stop ();
	}

	public override void UpdateState ()
	{
		base.UpdateState ();

		m_Agent.SetDestination(m_TargettedPlayer.transform.position);
	}
}
