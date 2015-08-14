using UnityEngine;
using System.Collections;

public class AttackState : GenericState 
{
	public GameObject m_AttackProjectilePrefab;
	public Transform m_ProjectileSpawnPoint;

	public float m_RotationSpeed;
	
	public float m_AttackRange;

	public float m_AttackDelay;
	public float m_AttackRecovery;

	float m_AttackTimer;
	protected float m_RecoveryTimer;

	PlayerInput[] m_Players;
	protected PlayerInput m_TargettedPlayer;
	ObjectPool m_ObjectPool;
	
	protected virtual void Start()
	{
		m_Players = FindObjectsOfType<PlayerInput>();

		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}
	
	public override bool CanEnterState ()
	{
		float minDistance = m_AttackRange;

		m_TargettedPlayer = null;

		foreach(PlayerInput player in m_Players)
		{
			Vector3 playerPosition = player.transform.position;
			playerPosition.y = transform.position.y;

			float distance = Vector3.Distance(playerPosition, transform.position);
			
			if(distance <= minDistance)
			{
				minDistance = distance;
				
				m_TargettedPlayer = player;
			}
		}

		return m_TargettedPlayer != null;
	}
	
	public override bool CanExitState ()
	{
		return (m_AttackTimer <= 0.0f && m_RecoveryTimer <= 0.0f);
	}
	
	public override void EnterState ()
	{
		m_AttackTimer = m_AttackDelay;
	}

	public override void UpdateState ()
	{
		if(m_AttackTimer > 0.0f)
		{
			m_AttackTimer -= Time.deltaTime;

			Vector3 targetForward = (m_TargettedPlayer.transform.position - transform.position).normalized;
			targetForward.y = 0.0f;

			transform.forward = Vector3.Slerp (transform.forward, targetForward, m_RotationSpeed * Time.deltaTime);

			if(m_AttackTimer <= 0.0f)
			{
				Attack ();

				m_RecoveryTimer = m_AttackRecovery;
			}

			UpdateWindUp();
		}
		else if(m_RecoveryTimer > 0.0f)
		{
			m_RecoveryTimer -= Time.deltaTime;

			UpdateRecovery ();
		}
	}
	
	public override void ExitState ()
	{
		base.ExitState ();
	}

	protected virtual GameObject Attack()
	{
		return m_ObjectPool.Instantiate (m_AttackProjectilePrefab, m_ProjectileSpawnPoint.position, m_ProjectileSpawnPoint.rotation);
	}

	protected virtual void UpdateWindUp()
	{

	}

	protected virtual void UpdateRecovery()
	{

	}
}
