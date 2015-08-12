using UnityEngine;
using System.Collections;

public class SwimmingState : MovementState
{
	public Transform m_CenterOfRotation;
	public float m_OrbitSpeed;
	public float m_OrbitAlterationSpeed;
	public float m_OrbitAlterationAmplitude;

	float m_OrbitAlterationTimer;

	AttackState m_Attack;

	NavMeshAgent m_Agent;

	float m_OrbitAngle;

	float m_OrbitDirection;

	Vector3 m_Destination;

	protected override void Start ()
	{
		base.Start ();

		m_Agent = GetComponent<NavMeshAgent>();

		m_Attack = GetComponent<AttackState>();

		m_OrbitDirection = (Random.value <= 0.5f) ? -1.0f : 1.0f;

		m_Destination = transform.position;
	}

	public override void EnterState ()
	{
		base.EnterState ();

		CalculateOrbitAngle ();
	}

	public override void UpdateState ()
	{
		base.UpdateState ();

		m_OrbitAngle += m_OrbitDirection * m_OrbitSpeed * Time.deltaTime;
		m_OrbitAlterationTimer += m_OrbitAlterationSpeed * Time.deltaTime;

		CalculateOrbitDestination();

		m_Agent.SetDestination (m_Destination);
	}

	void CalculateOrbitAngle()
	{
		Vector3 direction = m_Destination - m_CenterOfRotation.position;
		direction.y = 0.0f;
		direction.Normalize ();

		m_OrbitAngle = Vector3.Angle(Vector3.right, direction);

		Vector3 crossProduct = Vector3.Cross(Vector3.right, -direction);

		if(crossProduct.y < 0.0f)
		{
			m_OrbitAngle *= -1.0f;
		}
	}

	void CalculateOrbitDestination()
	{
		Vector3 offset = Vector3.zero;
		offset.x = Mathf.Cos (m_OrbitAngle * Mathf.Deg2Rad);
		offset.z = Mathf.Sin (m_OrbitAngle * Mathf.Deg2Rad);
		offset.Normalize();

		float offsetLength = m_Attack.m_AttackRange + m_OrbitAlterationAmplitude * Mathf.Sin(m_OrbitAlterationTimer);

		m_Destination = m_CenterOfRotation.position + offsetLength * offset;
	}
}
