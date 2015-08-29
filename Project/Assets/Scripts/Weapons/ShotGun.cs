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

	public override void Shoot ()
	{		
		int numberOfCartridgesLeft = m_RemainingShots;

		base.Shoot ();
		
		if(m_IsReloading)
		{
			EndReload ();
		}
		else if(numberOfCartridgesLeft > 0)
		{			
			PumpShotgun();
		}
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
	}

	void PumpShotgun()
	{
		m_Timer = m_PumpDuration;
		
		m_CharacterAnimator.SetBool("PumpShotgun", true);
		m_ShotgunAnimator.SetBool ("Pumping", true);

		m_IsPumping = true;
	}
}
