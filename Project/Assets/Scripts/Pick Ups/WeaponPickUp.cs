using UnityEngine;
using System.Collections;

public class WeaponPickUp : PickUp
{
	public WeaponType m_WeaponType;

	protected override void CollectVirtual (Interaction interaction)
	{
		if(interaction)
		{			
			Inventory inventory = interaction.GetComponent<Inventory>();
			
			if(inventory != null)
			{
                inventory.UnlockWeapon(m_WeaponType);
			}
		}
	}

	protected override bool CanBeCollectedVirtual (Interaction interaction)
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
                return true;
            }
        }
        else
        {
            return false;
		}
	}
}
