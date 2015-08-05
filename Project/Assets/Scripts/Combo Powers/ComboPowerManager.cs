using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboPowerManager : MonoBehaviour 
{
	ComboPower[] m_ComboPowers;

	// Use this for initialization
	void Start () 
	{
		m_ComboPowers = GetComponents<ComboPower>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void UseCombo(Vector3 position, List<PlayerCombo> playersComboing)
	{
		foreach(ComboPower comboPower in m_ComboPowers)
		{
			if(comboPower.m_NumberOfPlayersNecessary == playersComboing.Count)
			{
				comboPower.UsePower(position);

				foreach(PlayerCombo playerCombo in playersComboing)
				{
					playerCombo.SetRecharging();
				}
			}
		}
	}
}
