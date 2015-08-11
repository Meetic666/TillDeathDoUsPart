using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float m_Speed;
	public int m_Damage;
	public LayerMask m_LayersToCollideWith;

	Vector3 m_PreviousPosition;

	Collider m_Collider;

	void Start()
	{
		m_PreviousPosition = transform.position;

		m_Collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * m_Speed * Time.deltaTime;

		Vector3 displacement = transform.position - m_PreviousPosition;

		RaycastHit hitInfo;

		if(Physics.SphereCast (transform.position - displacement, m_Collider.bounds.size.x * 0.5f, displacement, out hitInfo, displacement.magnitude, m_LayersToCollideWith.value))
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
			otherHealth.Damage(transform.forward * m_Speed, m_Damage);
		}

		gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider otherCollider)
	{
		if((1 << otherCollider.gameObject.layer & m_LayersToCollideWith.value) != 0)
		{
			Explode (otherCollider.GetComponent<Health>());
		}
	}
}
