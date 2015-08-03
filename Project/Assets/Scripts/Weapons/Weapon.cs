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
	public int m_ReloadTime;

	int m_RemainingShots;
	
	float m_Timer;

	float m_SprayOffset;

	ObjectPool m_ObjectPool;

	// Use this for initialization
	void Start () 
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();

		m_RemainingShots = m_ClipSize;

		m_SprayOffset = Mathf.Sin(m_SprayAngle * Mathf.Deg2Rad);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Timer -= Time.deltaTime;
	}

	public void Shoot()
	{
		if(m_RemainingShots > 0 && m_Timer <= 0.0f)
		{
			GameObject newProjectile = null;

			for(int i = 0; i < m_NumberOfProjectilesPerShot; i++)
			{				
				newProjectile = m_ObjectPool.Instantiate(m_ProjectilePrefab, m_ProjectileStartPoint.position, Quaternion.identity);

				Vector3 forward = transform.forward + transform.right * Random.Range(- m_SprayOffset, m_SprayOffset) + transform.up * Random.Range(- m_SprayOffset, m_SprayOffset);
				forward.Normalize();

				newProjectile.transform.forward = forward;
			}
			
			m_RemainingShots--;

			if(m_RemainingShots > 0)
			{
				m_Timer = 1.0f / (float)m_FireRate;
			}
		}
	}

	public void Reload()
	{
		m_RemainingShots = m_ClipSize;

		m_Timer = m_ReloadTime;
	}
}
