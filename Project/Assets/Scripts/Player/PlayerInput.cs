using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
	public Vector3 Movement
	{
		get;
		protected set;
	}

	public Vector3 Look
	{
		get;
		protected set;
	}

	public bool Interact
	{
		get;
		protected set;
	}

	public bool Attack
	{
		get;
		protected set;
	}

	public bool Reload
	{
		get;
		protected set;
	}

	public bool SwitchWeapon
	{
		get;
		protected set;
	}

	public bool UsePower
	{
		get;
		protected set;
	}

	public int m_PlayerNumber;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 forward = Camera.main.transform.forward;
		forward.y = 0.0f;
		forward.Normalize ();

		Vector3 right = Vector3.Cross(forward, Vector3.up);

		Movement = - forward * Input.GetAxis(AxisConstants.LEFT_STICK_VERTICAL + m_PlayerNumber) - right * Input.GetAxis(AxisConstants.LEFT_STICK_HORIZONTAL + m_PlayerNumber);

		Look = - forward * Input.GetAxis(AxisConstants.RIGHT_STICK_VERTICAL + m_PlayerNumber) - right * Input.GetAxis(AxisConstants.RIGHT_STICK_HORIZONTAL + m_PlayerNumber);

		SwitchWeapon = Input.GetButtonDown(AxisConstants.Y_BUTTON + m_PlayerNumber);

		Attack = Input.GetButton(AxisConstants.RIGHT_BUMPER + m_PlayerNumber);

		Reload = Input.GetButtonDown(AxisConstants.B_BUTTON + m_PlayerNumber);

		Interact = Input.GetButton (AxisConstants.X_BUTTON + m_PlayerNumber);

		UsePower = Input.GetButton(AxisConstants.LEFT_BUMPER + m_PlayerNumber);
	}
}
