using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoeSelector : MonoBehaviour
{

    private Queue<GameObject> m_CanoeModels;
    // Start is called before the first frame update
    void Start()
    {
        m_CanoeModels = new Queue<GameObject>();
        foreach (Transform child in transform)
        {
            m_CanoeModels.Enqueue(child.gameObject);
        }

        ToggleNext();
    }

    public void ToggleNext()
    {
        if (m_CanoeModels.Count > 0)
        {
            m_CanoeModels.Peek().SetActive(false);
            m_CanoeModels.Enqueue(m_CanoeModels.Dequeue());
            m_CanoeModels.Peek().SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
