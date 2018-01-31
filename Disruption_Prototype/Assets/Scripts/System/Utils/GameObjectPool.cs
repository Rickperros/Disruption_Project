using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectPool
{
    private GameObject m_primitive;
    private int m_poolSize;
    private List<GameObject> m_pool;

    private int m_index;
    private GameObject m_poolParent;

    public GameObjectPool(GameObject primitive, int poolSize, string poolName = "Game Object Pool")
    {
        m_primitive = primitive;
        m_poolSize = poolSize;

        m_poolParent = new GameObject();
        m_poolParent.name = poolName;
        m_poolParent.transform.position = Vector3.zero;

        m_pool = new List<GameObject>();

        for (int i = 0; i < m_poolSize; i++)
        {
            m_pool.Add(Object.Instantiate(m_primitive, Vector3.zero, Quaternion.identity, m_poolParent.transform));
            m_pool[i].SetActive(false);
        }

        m_index = 0;
    }

    /// <summary>
    /// Gets next Game Object of the pool
    /// </summary>
    public GameObject RequireObject()
    {
        int l_index = m_index;

        m_index = (m_index + 1) % m_poolSize;
        return m_pool[l_index];
    }

    public void ClearPool()
    {
        Object.DestroyImmediate(m_poolParent);
        m_pool.Clear();
    }
}
