using UnityEngine;
using System.Collections;

public class SwimmingAttack : FlyingAttack 
{
	protected override void UpdateRecovery ()
	{
		base.UpdateRecovery();

		m_Agent.enabled = false;
	}

	public override void ExitState ()
	{
		base.ExitState ();

		m_Agent.enabled = true;
	}
}
