using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float m_Speed;
	public int m_Damage;
	public LayerMask m_LayersToCollideWith;

	Vector3 m_PreviousPosition;

	void Start()
	{
		m_PreviousPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * m_Speed * Time.deltaTime;

		Vector3 displacement = transform.position - m_PreviousPosition;

		RaycastHit hitInfo;

		if(Physics.Raycast (transform.position - displacement, displacement, out hitInfo, displacement.magnitude, m_LayersToCollideWith.value))
		{
			if(hitInfo.collider.gameObject != gameObject)
			{				
				Explode(hitInfo.collider.GetComponent<Health>());
			}
		}

		m_PreviousPosition = transform.position;
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
