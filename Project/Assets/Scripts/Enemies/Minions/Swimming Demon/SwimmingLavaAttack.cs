using UnityEngine;
using System.Collections;

public class SwimmingLavaAttack : AttackState
{
	public float m_ProjectileSpeed;

	protected override GameObject Attack ()
	{
		GameObject projectile = base.Attack ();

		Rigidbody projectileBody = projectile.GetComponent<Rigidbody>();

		if(projectileBody)
		{
			projectileBody.velocity = CalculateInitialVelocity();
		}

		return projectile;
	}

	Vector3 CalculateInitialVelocity()
	{
		Vector3 targetPosition = m_TargettedPlayer.transform.position;

		Vector3 direction = targetPosition - transform.position;
		direction.y = 0.0f;

		float distance = direction.magnitude;

		direction.Normalize ();

		float time = distance / m_ProjectileSpeed;
			
		Vector3 velocity = direction * m_ProjectileSpeed;
		velocity.y = - 2.0f * Physics.gravity.y / time;

		return velocity;
	}
}
