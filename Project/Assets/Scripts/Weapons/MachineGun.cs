using UnityEngine;
using System.Collections;

public class MachineGun : Weapon
{
	public GameObject m_Clip;
	public Transform m_LeftHand;
	public float m_ReleaseTime;
	public float m_AttachTime;

	float m_ClipTimer;

	bool m_IsClipAttached;

	Transform m_ClipParent;
	Vector3 m_ClipPosition;
	Quaternion m_ClipRotation;
	Vector3 m_ClipScale;

	protected override void StartVirtual ()
	{
		m_ClipParent = m_Clip.transform.parent;

		m_ClipPosition = m_Clip.transform.localPosition;
		m_ClipScale = m_Clip.transform.localScale;
		m_ClipRotation = m_Clip.transform.localRotation;

		m_IsClipAttached = true;
	}

	protected override void ReloadVirtual ()
	{
		base.ReloadVirtual ();

		m_ClipTimer = m_ReleaseTime;
	}

	protected override void UpdateVirtual ()
	{
		if(m_ClipTimer > 0.0f)
		{
			m_ClipTimer -= Time.deltaTime;

			if(m_ClipTimer <= 0.0f)
			{
				if(m_IsClipAttached)
				{
					DetachClip ();
				}
				else
				{
					AttachClip();
				}
			}
		}
	}

	void AttachClip()
	{
		m_Clip.transform.parent = m_ClipParent;
		
		m_Clip.transform.localPosition = m_ClipPosition;
		m_Clip.transform.localScale = m_ClipScale;
		m_Clip.transform.localRotation = m_ClipRotation;

		m_IsClipAttached = true;
	}

	void DetachClip()
	{			
		m_Clip.transform.parent = m_LeftHand;

		m_ClipTimer = m_AttachTime;

		m_IsClipAttached = false;
	}
}
