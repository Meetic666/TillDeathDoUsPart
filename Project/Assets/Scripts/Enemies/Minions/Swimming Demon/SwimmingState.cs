using UnityEngine;
using System.Collections;

public class SwimmingState : MovementState
{
	public Transform m_CenterOfRotation;

	NavMeshAgent m_Agent;

	protected override void Start ()
	{
		base.Start ();

		m_Agent = GetComponent<NavMeshAgent>();
	}

	public override void UpdateState ()
	{
		base.UpdateState ();
	}
}
