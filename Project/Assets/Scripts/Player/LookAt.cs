using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour 
{
	PlayerInput m_Input;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Input.Look != Vector3.zero)
		{
			transform.forward = m_Input.Look;
		}
	}
}
