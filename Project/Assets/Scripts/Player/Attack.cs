using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour 
{
	Inventory m_Inventory;
	PlayerInput m_Input;

	// Use this for initialization
	void Start () 
	{
		m_Inventory = GetComponent<Inventory>();
		m_Input = GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Weapon equippedWeapon = m_Inventory.EquippedWeapon;

		if(equippedWeapon)
		{
			if(m_Input.Attack)
			{
				equippedWeapon.Shoot();
			}
			
			if(m_Input.Reload)
			{
				equippedWeapon.Reload ();
			}
		}
	}
}
