using UnityEngine;
using System.Collections;

public class Movement : GenericState 
{
	public float m_Speed;

	PlayerInput m_Input;
	NavMeshAgent m_Agent;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
		m_Agent = GetComponent<NavMeshAgent>();
	}

	public override bool CanEnterState ()
	{
		return true;
	}

	public override void UpdateState ()
	{
		Vector3 displacement = m_Input.Movement * m_Speed * Time.deltaTime;

		NavMeshHit hitInfo;

		if(!m_Agent.Raycast(transform.position + displacement, out hitInfo))
		{
			m_Agent.Move (displacement);
		}
		else
		{
			Vector3 obstacleTangent = Vector3.Cross (hitInfo.normal, Vector3.up);
			float distanceAlongTangent = Vector3.Dot(obstacleTangent, displacement);

			m_Agent.Move(distanceAlongTangent * obstacleTangent);
		}
	}
}
