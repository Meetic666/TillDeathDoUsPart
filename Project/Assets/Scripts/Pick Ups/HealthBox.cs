using UnityEngine;
using System.Collections;

public class HealthBox : PickUp
{
	public int m_HealthAmount;

	protected override void CollectVirtual (Interaction interaction)
	{
		if(interaction)
		{
			Health health = interaction.GetComponent<Health>();
			
			if(health != null)
			{
                health.Heal (m_HealthAmount);
			}
		}
	}

	protected override bool CanBeCollectedVirtual (Interaction interaction)
	{
		Health health = interaction.GetComponent<Health>();
		
		if(health != null)
		{
			if(!health.NeedsHealing())
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
