using UnityEngine;
using System.Collections;

public class MovementState : GenericState
{
	public float m_MoveCycleDuration;

	float m_MoveCycleTimer;
	
	PlayerInput[] m_Players;
	protected PlayerInput m_TargettedPlayer;
	
	protected override void StartVirtual()
	{
		m_Players = FindObjectsOfType<PlayerInput>();
	}

	public override bool CanEnterState ()
	{
		return true;
	}

	public override bool CanExitState ()
	{
		return m_MoveCycleTimer <= 0.0f;
	}

	public override void EnterState ()
	{
		m_MoveCycleTimer = m_MoveCycleDuration;
		
		m_CharacterAnimator.SetBool ("Moving", true);
	}

	public override void UpdateState ()
	{
		m_MoveCycleTimer -= Time.deltaTime;

		UpdateTargettedPlayer();
	}

	public override void ExitState ()
	{		
		m_CharacterAnimator.SetBool ("Moving", false);
	}

	void UpdateTargettedPlayer()
	{
		float minDistance = float.PositiveInfinity;

		m_TargettedPlayer = null;
		
		foreach(PlayerInput player in m_Players)
		{
			if(!LayerMask.LayerToName(player.gameObject.layer).Contains (NameConstants.DEAD_LAYER))
			{
				float distance = Vector3.Distance(player.transform.position, transform.position);
				
				if(distance <= minDistance)
				{
					minDistance = distance;
					
					m_TargettedPlayer = player;
				}
			}
		}
	}
}
