using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public float m_Speed;

	PlayerInput m_Input;
	CharacterController m_Controller;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
		m_Controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Controller.Move (m_Input.Movement * m_Speed * Time.deltaTime);
	}
}
