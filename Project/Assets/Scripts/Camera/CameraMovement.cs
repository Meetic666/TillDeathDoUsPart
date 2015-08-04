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

	// Use this for initialization
	void Start () 
	{
		m_Players = FindObjectsOfType<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CalculateTargetPosition();

		transform.position = Vector3.Lerp (transform.position, m_TargetPosition, m_CameraSpeed * Time.deltaTime);
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
		distanceBetweenPlayers = Mathf.Clamp(distanceBetweenPlayers, m_MinDistanceBetweenPlayers, m_MaxDistanceBetweenPlayers);


		float distanceFromCenter = Mathf.Lerp(m_MinDistance, m_MaxDistance, (distanceBetweenPlayers - m_MinDistanceBetweenPlayers) / (m_MaxDistanceBetweenPlayers - m_MinDistanceBetweenPlayers));


		m_TargetPosition = playerCenter - transform.forward * distanceFromCenter;
	}
}
