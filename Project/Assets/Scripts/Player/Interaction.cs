using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour 
{
	public float m_InteractionRange;
	public LayerMask m_LayersToCollideWith;

	Animator m_CharacterAnimator;

	PickUp m_InRangePickUp;
	Health m_InRangeHealth;

	PlayerInput m_Input;

	public bool Interacting
	{
		get;
		protected set;
	}

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
		
		m_CharacterAnimator = GetComponent<AnimatorHandler>().m_CharacterAnimator;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckForPickUp();

		if(m_InRangePickUp && m_InRangePickUp.CanBeCollected(this))
		{
			if(m_Input.Interact)
			{
				m_InRangePickUp.Collect(this);

				Interacting = true;
			}
			else
			{
				m_InRangePickUp.Collect (null);

				Interacting = false;
			}
		}
		else
		{
			Interacting = false;
		}
		
		m_CharacterAnimator.SetBool("Crouching", Interacting);
	}

	void CheckForPickUp()
	{
		Collider[] pickUpColliders = Physics.OverlapSphere(transform.position, m_InteractionRange, m_LayersToCollideWith.value);

		float minDistance = m_InteractionRange;

		PickUp inRangePickUp = null;

		foreach(Collider pickUpCollider in pickUpColliders)
		{
			float distance = Vector3.Distance (pickUpCollider.transform.position, transform.position);

			if(pickUpCollider.gameObject != gameObject && distance < minDistance)
			{
				minDistance = distance;

				inRangePickUp = pickUpCollider.GetComponent<PickUp>();
			}
		}

		if(m_InRangePickUp && inRangePickUp != m_InRangePickUp)
		{
			m_InRangePickUp.Collect (null);
		}

		m_InRangePickUp = inRangePickUp;
	}
}
