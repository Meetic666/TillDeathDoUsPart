using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour 
{
	public float m_InteractionRange;
	public LayerMask m_LayersToCollideWith;

	PickUp m_InRangePickUp;

	PlayerInput m_Input;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckForPickUp();

		if(m_Input.Interact && m_InRangePickUp)
		{
			m_InRangePickUp.Collect(this);
		}
	}

	void CheckForPickUp()
	{
		Collider[] pickUpColliders = Physics.OverlapSphere(transform.position, m_InteractionRange, m_LayersToCollideWith.value);

		float minDistance = m_InteractionRange;

		m_InRangePickUp = null;

		foreach(Collider pickUpCollider in pickUpColliders)
		{
			float distance = Vector3.Distance (pickUpCollider.transform.position, transform.position);

			if(distance < minDistance)
			{
				minDistance = distance;

				m_InRangePickUp = pickUpCollider.GetComponent<PickUp>();
			}
		}
	}
}
