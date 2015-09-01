using UnityEngine;
using System.Collections;

public class AIHealth : Health 
{
	public GameObject m_CorpsePrefab;

	protected override void Die ()
	{
		GameObject corpse = m_ObjectPool.Instantiate(m_CorpsePrefab, m_CharacterAnimator.transform.position, m_CharacterAnimator.transform.rotation);

		CopyPose (m_CharacterAnimator.transform, corpse.transform, true);

		gameObject.SetActive (false);
	}

	void CopyPose(Transform original, Transform copy, bool isRoot)
	{
		if(!isRoot)
		{
			copy.localPosition = original.localPosition;
			copy.localRotation = original.localRotation;
			copy.localScale = original.localScale;
		}

		for(int i = 0; i < copy.childCount; i++)
		{
			CopyPose (original.GetChild(i), copy.GetChild (i), false);
		}
	}

	void OnEnable()
	{
		m_CurrentHealth = m_MaxHealth;
	}
}
