using UnityEngine;
using System.Collections;

public class ShotGun : Weapon 
{
	public float m_PumpDuration;

	bool m_IsPumping;

	Animator m_ShotgunAnimator;

	protected override void StartVirtual ()
	{
		m_ShotgunAnimator = GetComponent<Animator>();
	}

	public override bool Shoot ()
	{		
		bool shotFired = base.Shoot ();
		
		if(m_IsReloading)
		{
			EndReload ();
		}

		return shotFired;
	}

	protected override void EndReload ()
	{
		m_IsReloading = false;

		PumpShotgun();
	}

	protected override void FinishTimer ()
	{
		if(m_IsReloading)
		{
			if(m_RemainingShots < m_ClipSize)
			{
				m_RemainingShots++;

				if(m_RemainingShots == m_ClipSize)
				{
					EndReload ();
				}
				else
				{
					m_Timer = m_ReloadTime;
				}
			}
		}
		else if(m_IsPumping)
		{
			m_IsPumping = false;			
			
			m_CharacterAnimator.SetBool("PumpShotgun", false);
			m_ShotgunAnimator.SetBool ("Pumping", false);
		}
		else
		{
			PumpShotgun ();
		}
	}

	void PumpShotgun()
	{
		m_Timer = m_PumpDuration;
		
		m_CharacterAnimator.SetBool("PumpShotgun", true);
		m_ShotgunAnimator.SetBool ("Pumping", true);

		m_IsPumping = true;
	}
}
