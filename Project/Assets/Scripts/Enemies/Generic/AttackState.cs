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
	float m_RecoveryTimer;
	
	PlayerInput[] m_Players;
	PlayerInput m_TargettedPlayer;
	ObjectPool m_ObjectPool;
	
	void Start()
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
			float distance = Vector3.Distance(player.transform.position, transform.position);
			
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
			}
		}
		else if(m_RecoveryTimer > 0.0f)
		{
			m_RecoveryTimer -= Time.deltaTime;
		}
	}
	
	public override void ExitState ()
	{
		base.ExitState ();
	}

	void Attack()
	{
		m_ObjectPool.Instantiate (m_AttackProjectilePrefab, m_ProjectileSpawnPoint.position, m_ProjectileSpawnPoint.rotation);
	}

	protected virtual void UpdateWindUp()
	{

	}

	protected virtual void UpdateRecovery()
	{

	}
}
