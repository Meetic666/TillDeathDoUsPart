using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	public float m_CameraSpeed;

	public float m_MinDistance;
	public float m_MaxDistance;
	public float m_MinDistanceBetweenPlayers;
	public float m_MaxDistanceBetweenPlayers;
	
	Vector3 m_TargetPosition;

	PlayerInput[] m_Players;

	CharacterController m_Controller;

	// Use this for initialization
	void Start () 
	{
		m_Players = FindObjectsOfType<PlayerInput>();

		m_Controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CalculateTargetPosition();

		Vector3 displacement = m_TargetPosition - transform.position;
		float distance = displacement.magnitude;

		displacement.Normalize();

		displacement *= Mathf.Min (distance, m_CameraSpeed * Time.deltaTime);
			
		m_Controller.Move (displacement);
	}

	void CalculateTargetPosition()
	{
		Vector3 playerCenter = Vector3.zero;

		for(int i = 0; i < m_Players.Length; i++)
		{
			playerCenter += m_Players[i].transform.position;
		}

		playerCenter /= m_Players.Length;


		float distanceBetweenPlayers = Vector3.Distance(playerCenter, m_Players[0].transform.position) * 2.0f;

		for(int i = 1; i < m_Players.Length; i++)
		{
			float distance = Vector3.Distance(playerCenter, m_Players[i].transform.position) * 2.0f;

			if(distance > distanceBetweenPlayers)
			{
				distanceBetweenPlayers = distance;
			}
		}

		distanceBetweenPlayers = Mathf.Clamp(distanceBetweenPlayers, m_MinDistanceBetweenPlayers, m_MaxDistanceBetweenPlayers);

		float distanceFromCenter = Mathf.Lerp(m_MinDistance, m_MaxDistance, (distanceBetweenPlayers - m_MinDistanceBetweenPlayers) / (m_MaxDistanceBetweenPlayers - m_MinDistanceBetweenPlayers));

		m_TargetPosition = playerCenter - transform.forward * distanceFromCenter;
	}
}
