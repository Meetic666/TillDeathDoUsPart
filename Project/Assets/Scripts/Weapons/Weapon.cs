using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	public Transform m_ProjectileStartPoint;
	public GameObject m_ProjectilePrefab;
	public int m_FireRate;
	public int m_ClipSize;
	public int m_SprayAngle;
	public int m_NumberOfProjectilesPerShot;
	public float m_ReloadTime;

	protected int m_RemainingShots;
	
	protected float m_Timer;

	float m_SprayOffset;

	ObjectPool m_ObjectPool;

	protected Animator m_CharacterAnimator;

	protected bool m_IsReloading;

	// Use this for initialization
	void Start () 
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();

		m_CharacterAnimator = transform.root.GetComponent<AnimatorHandler>().m_CharacterAnimator;

		m_RemainingShots = m_ClipSize;

		m_SprayOffset = Mathf.Sin(m_SprayAngle * Mathf.Deg2Rad);

		StartVirtual ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Timer > 0.0f)
		{
			m_Timer -= Time.deltaTime;

			if(m_Timer <= 0.0f)
			{
				FinishTimer();
			}
		}
				
		m_CharacterAnimator.SetBool("Reloading", m_IsReloading);

		UpdateVirtual ();
	}

	protected virtual void StartVirtual()
	{

	}

	protected virtual void UpdateVirtual()
	{

	}

	public virtual bool Shoot()
	{
		bool result = false;

		if(m_RemainingShots > 0 && m_Timer <= 0.0f)
		{
			GameObject newProjectile = null;

			for(int i = 0; i < m_NumberOfProjectilesPerShot; i++)
			{				
				newProjectile = m_ObjectPool.Instantiate(m_ProjectilePrefab, m_ProjectileStartPoint.position, Quaternion.identity);

				Vector3 forward = m_ProjectileStartPoint.transform.forward + m_ProjectileStartPoint.transform.right * Random.Range(- m_SprayOffset, m_SprayOffset) + m_ProjectileStartPoint.transform.up * Random.Range(- m_SprayOffset, m_SprayOffset);
				forward.Normalize();

				newProjectile.transform.forward = forward;
			}
			
			m_RemainingShots--;

			if(m_RemainingShots > 0)
			{
				m_Timer = 1.0f / (float)m_FireRate;
			}

			result = true;
		}

		return result;
	}

	public void Reload()
	{
		if(m_RemainingShots != m_ClipSize)
		{
			ReloadVirtual();
		}
	}

	protected virtual void ReloadVirtual()
	{
		m_IsReloading = true;

		m_Timer = m_ReloadTime;
	}

	protected virtual void FinishTimer()
	{
		if(m_IsReloading)
		{
			EndReload();
		}
		else
		{
			ReadyForNextShot ();
		}
	}

	protected virtual void EndReload()
	{		
		m_IsReloading = false;

		m_RemainingShots = m_ClipSize;
	}

	protected virtual void ReadyForNextShot()
	{

	}
}
