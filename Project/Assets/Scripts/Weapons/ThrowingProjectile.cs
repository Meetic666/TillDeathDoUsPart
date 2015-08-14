using UnityEngine;
using System.Collections;

public class ThrowingProjectile : MonoBehaviour 
{
	public GameObject m_Explosion;

	ObjectPool m_ObjectPool;

	void Start()
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}

	void OnDisable()
	{
		m_ObjectPool.Instantiate (m_Explosion, transform.position, Quaternion.identity);
	}
}
