using UnityEngine;
using System.Collections;

public class WeaponPickUp : PickUp
{
	public WeaponType m_WeaponType;

	protected override bool CollectVirtual (Interaction interaction)
	{
		Inventory inventory = interaction.GetComponent<Inventory>();

		if(inventory != null)
		{
			if(inventory.WeaponIsUnlocked(m_WeaponType))
			{
				return false;
			}
			else
			{
				inventory.UnlockWeapon(m_WeaponType);

				return true;
			}
		}
		else
		{
			return false;
		}
	}
}
