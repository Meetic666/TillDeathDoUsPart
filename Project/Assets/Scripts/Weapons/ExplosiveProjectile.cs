using UnityEngine;
using System.Collections;

public class ExplosiveProjectile : MonoBehaviour 
{
	public GameObject m_Explosion;

	ObjectPool m_ObjectPool;

	void Start()
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}

	public void Explode()
	{
		m_ObjectPool.Instantiate (m_Explosion, transform.position, Quaternion.identity);
	}
}
