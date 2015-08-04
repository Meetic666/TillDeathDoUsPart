using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour 
{
	Vector3 m_InitialPosition;

	float m_Time;

	// Use this for initialization
	protected virtual void Start () 
	{
		m_InitialPosition = transform.position;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		m_Time += Time.deltaTime;

		transform.position = m_InitialPosition + transform.up * GameConstants.PICK_UP_WAVE_AMPLITUDE * Mathf.Sin(m_Time * GameConstants.PICK_UP_WAVE_SPEED);

		Vector3 newEulerAngles = transform.eulerAngles;
		newEulerAngles.y = m_Time * GameConstants.PICK_UP_ROTATION_SPEED;
		transform.eulerAngles = newEulerAngles;
	}

	public void Collect(Interaction interaction)
	{
		CollectVirtual (interaction);

		if(interaction)
		{
			CollectionComplete ();
		}
	}

	public bool CanBeCollected(Interaction interaction)
	{
		return CanBeCollectedVirtual(interaction);
	}

	protected virtual void CollectVirtual(Interaction interaction)
	{

	}

	protected virtual bool CanBeCollectedVirtual(Interaction interaction)
	{
		return false;
	}

	protected virtual void CollectionComplete()
	{
		gameObject.SetActive (false);
	}
}
