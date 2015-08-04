using UnityEngine;
using System.Collections;

public class SuperPower : MonoBehaviour 
{
	public float m_RechargeTime;
	float m_Timer;

	PlayerInput m_Input;

	protected ObjectPool m_ObjectPool;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();

		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Timer -= Time.deltaTime;

		if(m_Timer <= 0.0f)
		{
			if(m_Input.UsePower)
			{
				UsePower ();

				m_Timer = m_RechargeTime;
			}
		}
	}

	protected virtual void UsePower()
	{

	}
}
