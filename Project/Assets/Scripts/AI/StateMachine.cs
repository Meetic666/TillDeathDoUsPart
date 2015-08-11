using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour 
{
	GenericState[] m_States;

	int m_CurrentStateIndex;
	int m_InterruptionStateIndex;

	List<int> m_PotentialNextStatesIndex;

	// Use this for initialization
	void Start () 
	{
		m_States = GetComponents<GenericState>();

		for(int i = 0; i < m_States.Length; i++)
		{
			if(m_States[i] is InterruptionState)
			{
				m_InterruptionStateIndex = i;
			}
		}

		m_PotentialNextStatesIndex = new List<int>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_States[m_CurrentStateIndex].UpdateState();
		
		CalculateNextState();
	}

	public void Interrupt()
	{
		SetNewState(m_InterruptionStateIndex);
	}

	void CalculateNextState()
	{
		m_PotentialNextStatesIndex.Clear();

		if(m_States[m_CurrentStateIndex].CanExitState())
		{
			for(int i = 0; i < m_States.Length; i++)
			{
				if(i != m_InterruptionStateIndex && m_States[i].CanEnterState())
				{
					for(int j = 0; j < m_States[i].m_Weight; j++)
					{
						m_PotentialNextStatesIndex.Add (i);
					}
				}
			}
		}

		if(m_PotentialNextStatesIndex.Count > 0)
		{
			SetNewState(m_PotentialNextStatesIndex[Random.Range (0, m_PotentialNextStatesIndex.Count)]);
		}
	}

	void SetNewState(int newStateIndex)
	{
		m_States[m_CurrentStateIndex].ExitState();
		
		m_CurrentStateIndex = newStateIndex;
		
		m_States[m_CurrentStateIndex].EnterState();
	}
}
