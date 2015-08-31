using UnityEngine;
using System.Collections;

public class GenericState : MonoBehaviour 
{
	public int m_Weight = 1;

	protected Animator m_CharacterAnimator;

	void Start()
	{
		m_CharacterAnimator = GetComponent<AnimatorHandler>().m_CharacterAnimator;

		StartVirtual();
	}

	virtual protected void StartVirtual()
	{

	}

	virtual public bool CanEnterState()
	{
		return false;
	}

	virtual public void EnterState()
	{

	}

	virtual public void UpdateState()
	{

	}

	virtual public bool CanExitState()
	{
		return false;
	}

	virtual public void ExitState()
	{

	}
}
