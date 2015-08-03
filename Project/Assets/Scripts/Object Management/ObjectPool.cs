using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ObjectPool handles pooling of object (better memory management)
// It uses a dictionary to get the pooled objects using the desired prefab as key
public class ObjectPool : MonoBehaviour 
{
	#region Private Members
	// Used to store the object pool by comparing prefabs
	Dictionary<GameObject, List<GameObject>> m_ObjectPool;
	#endregion
	
	#region Unity Hooks
	// Use this for initialization
	void Start () 
	{
		m_ObjectPool = new Dictionary<GameObject, List<GameObject>>();
	}
	#endregion
	
	#region Public Methods
	// Used to get an existing instance in the object pool, or get a new one (if none is available in the pool)
	public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		GameObject result = null;
		
		// Checks if the prefab exists in the object pool
		if(!m_ObjectPool.ContainsKey(prefab))
		{
			m_ObjectPool.Add (prefab, new List<GameObject>());
		}
		else
		{
			foreach(GameObject pooledObject in m_ObjectPool[prefab])
			{
				// Searches for an inactive object in the pool corresponding to the prefab
				if(result == null && !pooledObject.activeSelf)
				{
					pooledObject.SetActive(true);
					
					result = pooledObject;
				}
			}
		}
		
		// If no object has been found in the pool
		if(result == null)
		{
			// Instantiate a new object
			result = (GameObject) Instantiate(prefab);
			
			// Sets object to be a child of the object pool
			result.transform.parent = transform;
			
			// Adds the new object to the pool corresponding to the prefab
			m_ObjectPool[prefab].Add (result);
		}
		
		// Sets position and rotation of object according to arguments
		result.transform.position = position;
		result.transform.rotation = rotation;
		
		return result;
	}
	#endregion
}