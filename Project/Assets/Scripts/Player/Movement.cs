using UnityEngine;
using System.Collections;

public class Movement : GenericState 
{
	public float m_Speed;

	public float m_MaxDistanceFromPlayers;

	PlayerInput m_Input;
	NavMeshAgent m_Agent;

	Interaction m_Interaction;

	PlayerInput[] m_Players;

	// Use this for initialization
	protected override void StartVirtual ()
	{
		m_Input = GetComponent<PlayerInput>();
		m_Agent = GetComponent<NavMeshAgent>();

		m_Interaction = GetComponent<Interaction>();

		m_Players = FindObjectsOfType<PlayerInput>();
	}

	public override bool CanEnterState ()
	{
		return true;
	}

	public override void UpdateState ()
	{
		Vector3 displacement = m_Input.Movement * m_Speed * Time.deltaTime;

		if(m_Interaction.Interacting)
		{
			displacement = Vector3.zero;
		}

		if(displacement != Vector3.zero)
		{
			Vector3 playersCenter = CalculatePlayersCenter();
			
			if(Vector3.Distance(transform.position + displacement, playersCenter) > m_MaxDistanceFromPlayers)
			{
				Vector3 targetPosition = transform.position + displacement;
				Vector3 vectorFromPlayers = targetPosition - playersCenter;

				vectorFromPlayers.Normalize ();

				vectorFromPlayers *= m_MaxDistanceFromPlayers;

				targetPosition = playersCenter + vectorFromPlayers;

				displacement = targetPosition - transform.position;
			}

			if(displacement != Vector3.zero)
			{
				NavMeshHit hitInfo;
				
				if(m_Agent.Raycast(transform.position + displacement, out hitInfo))
				{
					Vector3 obstacleTangent = Vector3.Cross (hitInfo.normal, Vector3.up);
					float distanceAlongTangent = Vector3.Dot(obstacleTangent, displacement);
					
					displacement = distanceAlongTangent * obstacleTangent;
				}
			}
		}

		m_Agent.Move (displacement);

		displacement.Normalize ();

		m_CharacterAnimator.SetFloat ("ForwardDotProduct", Vector3.Dot (transform.forward, displacement));
		m_CharacterAnimator.SetFloat ("RightDotProduct", Vector3.Dot (transform.right, displacement));
	}

	public override void ExitState ()
	{
		m_CharacterAnimator.SetFloat ("ForwardDotProduct", 0.0f);
		m_CharacterAnimator.SetFloat ("RightDotProduct", 0.0f);
	}

	Vector3 CalculatePlayersCenter()
    {
        Vector3 playerCenter = Vector3.zero;

        if (m_Players.Length > 1)
        {
		
		    for(int i = 0; i < m_Players.Length; i++)
		    {
			    if(m_Players[i] != m_Input)
			    {
				    playerCenter += m_Players[i].transform.position;
			    }	
		    }
		
		    playerCenter /= (m_Players.Length - 1);
        }
        else
        {
            playerCenter = transform.position;
        }

		return playerCenter;
	}
}
