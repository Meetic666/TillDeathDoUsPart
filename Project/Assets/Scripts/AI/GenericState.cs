using UnityEngine;
using System.Collections;

public class GenericState : MonoBehaviour 
{
	public int m_Weight = 1;

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
