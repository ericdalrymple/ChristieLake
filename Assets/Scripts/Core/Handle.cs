using UnityEngine;

public class Handle : ScriptableObject
{
    [SerializeField]
    private string m_Value = string.Empty;

    public string Value
    {
        get { return m_Value; }
    }

    public override string ToString()
    {
        return Value;
    }
}
