using UnityEngine;
using System.Collections;

public class FlyingState : MovementState
{	
	public float m_RotationSpeed;

	public override void UpdateState ()
	{
		base.UpdateState ();		
		
		Vector3 targetForward = (m_TargettedPlayer.transform.position - transform.position).normalized;
		
		transform.forward = Vector3.Slerp (transform.forward, targetForward, m_RotationSpeed * Time.deltaTime);
	}
}
