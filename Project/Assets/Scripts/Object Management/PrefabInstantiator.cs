using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrefabInstantiator : MonoBehaviour
{
    public GameObject m_PrefabToInstantiate;
    public bool m_RespawnAfterPlayerDeath;

    bool[] m_GameObjectsAlive;
    bool[] m_TempGameObjectsAlive;

    Transform[] m_ChildTransforms;

    ObjectPool m_ObjectPool;

    void Start()
    {
        m_ObjectPool = FindObjectOfType<ObjectPool>();

        m_ChildTransforms = new Transform[transform.childCount];

        int index = 0;

        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                m_ChildTransforms[index] = child;

                index++;
            }
        }

        m_GameObjectsAlive = new bool[transform.childCount];
        m_TempGameObjectsAlive = new bool[transform.childCount];

        for (int i = 0; i < m_GameObjectsAlive.Length; i++)
        {
            m_GameObjectsAlive[i] = true;
            m_TempGameObjectsAlive[i] = true;
        }
    }

    void SpawnPrefabs()
    {
        GameObject tempObject;
        Transform child;

        for (int i = 0; i < m_ChildTransforms.Length; i++)
        {
            child = m_ChildTransforms[i];

            if (m_TempGameObjectsAlive[i] && child.childCount == 0)
            {
                tempObject = m_ObjectPool.Instantiate(m_PrefabToInstantiate, child.position, child.rotation);

                tempObject.transform.parent = child;
            }
        }
    }

    void DespawnPrefabs()
    {
        Transform child;
        GameObject objectInstantiated;

        for (int i = 0; i < m_ChildTransforms.Length; i++)
        {
            child = m_ChildTransforms[i];

            if (child.childCount == 1)
            {
                objectInstantiated = child.GetChild(0).gameObject;

                if (objectInstantiated.activeSelf)
                {
                    objectInstantiated.SetActive(false);
                }
                else
                {
                    m_TempGameObjectsAlive[i] = false;
                }

                objectInstantiated.transform.parent = m_ObjectPool.transform;
            }
            else
            {
                m_TempGameObjectsAlive[i] = false;
            }
        }
    }

    [ContextMenu("Set up spawn positions")]
    void SetUpSpawnPositions()
    {
#if UNITY_EDITOR
        Transform child;

        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);

            Component[] components = child.GetComponents<Component>();

            for (int j = 0; j < components.Length; j++)
            {
                if (!(components[j] is Transform))
                {
                    DestroyImmediate(components[j]);
                }
            }

            for (int j = 0; j < child.childCount; j++)
            {
                DestroyImmediate(child.GetChild(j).gameObject);
            }

            child.name = "SpawnPosition";

            PrefabUtility.DisconnectPrefabInstance(child.gameObject);
        }
#endif
    }
}
