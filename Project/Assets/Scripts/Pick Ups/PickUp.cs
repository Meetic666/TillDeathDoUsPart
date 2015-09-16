using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour 
{
	public float m_CollectionTime;

	Vector3 m_InitialPosition;
	float m_InitialEulerAngleY;

	float m_Time;

	float m_CollectionTimer;

	bool m_IsCollected;

	Interaction m_PlayerInteraction;

	// Use this for initialization
	protected virtual void Start () 
	{
		m_InitialPosition = transform.position;

		m_InitialEulerAngleY = Random.Range (0.0f, 360.0f);
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
        UpdatePosition();

        UpdateCollection();
	}

    void UpdatePosition()
    {
        m_Time += Time.deltaTime;

        transform.position = m_InitialPosition + transform.up * GameConstants.PICK_UP_WAVE_AMPLITUDE * Mathf.Sin(m_Time * GameConstants.PICK_UP_WAVE_SPEED);

        Vector3 newEulerAngles = transform.eulerAngles;
        newEulerAngles.y = m_InitialEulerAngleY + m_Time * GameConstants.PICK_UP_ROTATION_SPEED;
        transform.eulerAngles = newEulerAngles;
    }

    protected void UpdateCollection()
    {
        if (m_IsCollected)
        {
            m_CollectionTimer -= Time.deltaTime;

            if (m_CollectionTimer <= 0.0f)
            {
                CollectVirtual(m_PlayerInteraction);

                if (m_PlayerInteraction)
                {
                    CollectionComplete();
                }
            }
        }
    }

	public void Collect(Interaction interaction)
	{
		Interaction previousInteraction = m_PlayerInteraction;

		m_PlayerInteraction = interaction;

		if(m_PlayerInteraction && !previousInteraction)
		{
			m_CollectionTimer = m_CollectionTime;

			m_IsCollected = true;
		}
		else if(!m_PlayerInteraction)
		{
			m_IsCollected = false;
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
