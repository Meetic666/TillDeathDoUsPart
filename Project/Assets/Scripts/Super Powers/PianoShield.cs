using UnityEngine;
using System.Collections;

public class PianoShield : SuperPower 
{
	public GameObject m_PianoPrefab;
	public float m_DistanceFromPlayer;
	
	protected override void UsePower ()
	{
		m_ObjectPool.Instantiate (m_PianoPrefab, transform.position + transform.forward * m_DistanceFromPlayer, transform.rotation);
	}
}
