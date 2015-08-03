using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float m_Speed;
	public int m_Damage;
	public LayerMask m_LayersToCollideWith;
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 displacement = transform.forward * m_Speed * Time.deltaTime;

		transform.position += displacement;

		RaycastHit hitInfo;

		if(Physics.Raycast (transform.position - displacement, displacement, out hitInfo, displacement.magnitude, m_LayersToCollideWith.value))
		{
			if(hitInfo.collider.gameObject != gameObject)
			{				
				Explode(hitInfo.collider.GetComponent<Health>());
			}
		}
	}

	void Explode(Health otherHealth)
	{
		if(otherHealth)
		{
			otherHealth.Damage(m_Damage);
		}

		gameObject.SetActive(false);
	}
}
