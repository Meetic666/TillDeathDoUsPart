using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType
{
	e_HandGun,
	e_ShotGun,
	e_MachineGun,
	e_RocketLauncher
}

public class Inventory : MonoBehaviour 
{
	public Weapon[] m_Weapons;

	public float m_WeaponChangeTime;

	float m_Timer;

	List<WeaponType> m_UnlockedWeapons;

	int m_EquippedWeaponIndex;
	
	PlayerInput m_Input;
	
	Animator m_CharacterAnimator;

	public Weapon EquippedWeapon
	{
		get
		{
			if(m_UnlockedWeapons.Count > 0)
			{
				return m_Weapons[(int)m_UnlockedWeapons[m_EquippedWeaponIndex]];
			}
			else
			{
				return null;
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
		m_UnlockedWeapons = new List<WeaponType>();
		m_UnlockedWeapons.AddRange (GameData.Instance.UnlockedWeapons);

		m_Input = GetComponent<PlayerInput>();
		
		m_CharacterAnimator = GetComponent<AnimatorHandler>().m_CharacterAnimator;

		ChangeWeapon();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Input.SwitchWeapon)
		{
			m_EquippedWeaponIndex++;

			m_EquippedWeaponIndex %= m_UnlockedWeapons.Count;

			ChangeWeapon();
		}

		if(m_Timer > 0.0f)
		{
			m_Timer -= Time.deltaTime;

			if(m_Timer <= 0.0f)
			{
				EquipWeapon ();
			}
		}
	}

	public void UnlockWeapon(WeaponType type)
	{
		if(!m_UnlockedWeapons.Contains(type))
		{
			m_UnlockedWeapons.Add (type);

			m_EquippedWeaponIndex = m_UnlockedWeapons.Count - 1;

			ChangeWeapon();

			if(m_UnlockedWeapons.Count == 1)
			{
				m_Timer = 0.0f;

				EquipWeapon ();
			}

			GameData.Instance.UnlockWeapon(type);
		}
	}

	public bool WeaponIsUnlocked(WeaponType type)
	{
		return m_UnlockedWeapons.Contains(type);
	}

	void EquipWeapon()
	{
		for(int i = 0; i < m_Weapons.Length; i++)
		{
			m_Weapons[i].gameObject.SetActive (m_UnlockedWeapons.Count > 0 && i == (int) m_UnlockedWeapons[m_EquippedWeaponIndex]);
		}
	}

	void ChangeWeapon()
	{
		m_Timer = m_WeaponChangeTime;		
		
		if(m_UnlockedWeapons.Count > 0)
		{			
			m_CharacterAnimator.SetInteger("WeaponEquipped", (int) m_UnlockedWeapons[m_EquippedWeaponIndex] + 1);
		}
	}
}
