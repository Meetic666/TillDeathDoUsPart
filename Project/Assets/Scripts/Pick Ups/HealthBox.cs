using UnityEngine;
using System.Collections;

public class HealthBox : PickUp
{
	public int m_HealthAmount;

	protected override bool CollectVirtual (Interaction interaction)
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
				health.Heal (m_HealthAmount);
				
				return true;
			}
		}
		else
		{
			return false;
		}
	}
}
