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

		float theta = 0.5f * Mathf.Asin (Physics.gravity.y * distance / (m_ProjectileSpeed * m_ProjectileSpeed));

		Debug.Log(theta);
			
		Vector3 velocity = direction;
		velocity.y = Mathf.Sin (theta);
		velocity.Normalize ();

		velocity = velocity * m_ProjectileSpeed;

		Debug.Log(velocity);

		return velocity;
	}
}
