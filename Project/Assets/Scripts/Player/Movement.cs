using UnityEngine;
using System.Collections;

public class Movement : GenericState 
{
	public float m_Speed;

	Animator m_CharacterAnimator;

	PlayerInput m_Input;
	NavMeshAgent m_Agent;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
		m_Agent = GetComponent<NavMeshAgent>();

		m_CharacterAnimator = GetComponent<AnimatorHandler>().m_CharacterAnimator;
	}

	public override bool CanEnterState ()
	{
		return true;
	}

	public override void UpdateState ()
	{
		Vector3 displacement = m_Input.Movement * m_Speed * Time.deltaTime;

		NavMeshHit hitInfo;

		if(m_Agent.Raycast(transform.position + displacement, out hitInfo))
		{
			Vector3 obstacleTangent = Vector3.Cross (hitInfo.normal, Vector3.up);
			float distanceAlongTangent = Vector3.Dot(obstacleTangent, displacement);

			displacement = distanceAlongTangent * obstacleTangent;
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
}
