using UnityEngine;
using System.Collections;

public class Horse : MonoBehaviour 
{
	public float m_StartDelay;
	public float m_Speed;
	public float m_VerticalSpeed;

	float m_InitialLocalX;
	float m_InitialLocalY;
	float m_InitialAngleX;
	float m_StartTimer;

	bool m_LastTurn;

	bool m_IsRunning;

	enum HorseState
	{
		e_Running,
		e_Emerging,
		e_Sinking
	}

	HorseState m_CurrentState;

	// Use this for initialization
	void Start () 
	{
		m_CurrentState = HorseState.e_Emerging;

		m_InitialAngleX = transform.localEulerAngles.x;

		if(m_InitialAngleX > 180.0f && m_InitialAngleX <= 360.0f)
		{
			m_InitialAngleX = m_InitialAngleX - 360.0f;
		}

		m_InitialLocalX = transform.localPosition.x;
		m_InitialLocalY = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_IsRunning)
		{
			switch(m_CurrentState)
			{
			case HorseState.e_Emerging:
			{
				transform.position += Vector3.up * m_VerticalSpeed * Time.deltaTime;
				
				float heightPercentage = (transform.localPosition.y - m_InitialLocalY) / (- m_InitialLocalY);
				
				Vector3 newLocalEulerAngles = transform.localEulerAngles;
				newLocalEulerAngles.x = Mathf.Lerp (m_InitialAngleX, 0.0f, heightPercentage);
				transform.localEulerAngles = newLocalEulerAngles;
				
				if(transform.localPosition.y >= 0.0f)
				{
					Vector3 newLocalPosition = transform.localPosition;
					newLocalPosition.y = 0.0f;
					transform.localPosition = newLocalPosition;
					
					m_CurrentState = HorseState.e_Running;
				}
			}
				break;
				
			case HorseState.e_Running:
			{
				transform.position += transform.forward * m_Speed * Time.deltaTime;
				
				if(Mathf.Abs (transform.localPosition.x) > Mathf.Abs (m_InitialLocalX))
				{
					m_CurrentState = HorseState.e_Sinking;
				}
			}
				break;
				
			case HorseState.e_Sinking:
			{
				transform.position -= Vector3.up * m_VerticalSpeed * Time.deltaTime;
				
				float heightPercentage = (transform.localPosition.y - m_InitialLocalY) / (- m_InitialLocalY);
				
				Vector3 newLocalEulerAngles = transform.localEulerAngles;
				newLocalEulerAngles.x = Mathf.Lerp (- m_InitialAngleX, 0.0f, heightPercentage);
				
				if(transform.localPosition.y <= m_InitialLocalY)
				{
					Vector3 newLocalPosition = transform.localPosition;
					newLocalPosition.x = m_InitialLocalX;
					newLocalPosition.y = m_InitialLocalY;
					transform.localPosition = newLocalPosition;
					
					newLocalEulerAngles.x = m_InitialAngleX;

					if(m_LastTurn)
					{
						m_IsRunning = false;
					}
					else
					{						
						m_CurrentState = HorseState.e_Emerging;
					}
				}
				
				transform.localEulerAngles = newLocalEulerAngles;
			}
				break;
			}
		}
		else
		{
			if(m_StartTimer > 0.0f)
			{
				m_StartTimer -= Time.deltaTime;

				if(m_StartTimer <= 0.0f)
				{
					m_IsRunning = true;
				}
			}
		}
	}

	public void StartRunning()
	{
		m_IsRunning = false;
		m_LastTurn = false;
		m_StartTimer = Random.Range (0.0f, m_StartDelay);
	}

	public void StopRunning()
	{
		m_LastTurn = true;
	}
}
