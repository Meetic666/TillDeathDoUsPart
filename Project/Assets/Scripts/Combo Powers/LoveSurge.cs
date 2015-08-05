using UnityEngine;
using System.Collections;

public class LoveSurge : ComboPower
{
	public GameObject m_LoveSurgePrefab;

	public override void UsePower (Vector3 center)
	{
		m_ObjectPool.Instantiate (m_LoveSurgePrefab, center, Quaternion.identity);
	}
}
