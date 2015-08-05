using UnityEngine;
using System.Collections;

public class ComboPower : MonoBehaviour 
{	
	public int m_NumberOfPlayersNecessary;

	protected ObjectPool m_ObjectPool;

	// Use this for initialization
	void Start () 
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}

	public virtual void UsePower(Vector3 center)
	{

	}
}
