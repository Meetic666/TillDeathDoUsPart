using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour 
{
	PlayerInput m_Input;

	Health m_Health;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();

		m_Health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!m_Health.Dead && m_Input.Look != Vector3.zero)
		{
			transform.forward = m_Input.Look;
		}
	}
}
