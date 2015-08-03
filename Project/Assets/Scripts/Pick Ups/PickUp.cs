using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour 
{
	Vector3 m_InitialPosition;

	float m_Time;

	// Use this for initialization
	void Start () 
	{
		m_InitialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Time += Time.deltaTime;

		transform.position = m_InitialPosition + transform.up * GameConstants.PICK_UP_WAVE_AMPLITUDE * Mathf.Sin(m_Time * GameConstants.PICK_UP_WAVE_SPEED);

		Vector3 newEulerAngles = transform.eulerAngles;
		newEulerAngles.y = m_Time * GameConstants.PICK_UP_ROTATION_SPEED;
		transform.eulerAngles = newEulerAngles;
	}

	public bool Collect(Interaction interaction)
	{
		bool result = CollectVirtual (interaction);

		if(result)
		{
			CollectionComplete ();
		}

		return result;
	}

	protected virtual bool CollectVirtual(Interaction interaction)
	{
		return false;
	}

	void CollectionComplete()
	{
		gameObject.SetActive (false);
	}
}
