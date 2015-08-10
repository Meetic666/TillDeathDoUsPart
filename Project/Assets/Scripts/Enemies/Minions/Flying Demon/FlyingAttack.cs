using UnityEngine;
using System.Collections;

public class FlyingAttack : AttackState
{
	public float m_SlashSpeed;
	public float m_DipHeight;

	Vector3 m_SlashDirection;

	float m_InitialHeight;

	protected override void UpdateWindUp ()
	{
		m_SlashDirection = transform.forward;

		m_InitialHeight = transform.position.y;
	}

	protected override void UpdateRecovery ()
	{
		transform.position += m_SlashSpeed * m_SlashDirection * Time.deltaTime;

		float percentage = m_RecoveryTimer / m_AttackRecovery;

		percentage = Mathf.Clamp01(percentage);

		float dip = m_DipHeight * Mathf.Sin(percentage * Mathf.PI);

		Vector3 newPosition = transform.position;
		newPosition.y = m_InitialHeight - dip;
		transform.position = newPosition;
	}
}
