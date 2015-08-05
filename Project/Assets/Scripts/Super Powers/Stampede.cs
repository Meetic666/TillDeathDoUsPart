using UnityEngine;
using System.Collections;

public class Stampede : SuperPower 
{
	public GameObject m_StampedePrefab;
	public float m_DistanceFromPlayer;
	
	protected override void UsePower ()
	{
		m_ObjectPool.Instantiate (m_StampedePrefab, transform.position + transform.forward * m_DistanceFromPlayer, transform.rotation);
	}
}
