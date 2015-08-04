using UnityEngine;
using System.Collections;

public class AreaOfDamage : MonoBehaviour 
{
	public float m_DamagePerSecond;
	public LayerMask m_LayersToDamage;
	public float m_Range;
	
	// Update is called once per frame
	void Update () 
	{
		Collider[] inRangeColliders = Physics.OverlapSphere(transform.position, m_Range, m_LayersToDamage.value);

		for(int i = 0; i < inRangeColliders.Length; i++)
		{
			Health health = inRangeColliders[i].GetComponent<Health>();

			if(health)
			{
				health.Damage (m_DamagePerSecond * Time.deltaTime);
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, m_Range);
	}
}
