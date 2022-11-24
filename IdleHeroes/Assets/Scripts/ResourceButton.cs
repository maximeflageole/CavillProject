using UnityEngine;

public class ResourceButton : MonoBehaviour
{
    [SerializeField]
    private int m_resourceAmount;
    [SerializeField]
    private EResourceType m_resourceType;

    public void AddResource()
    {
        GameManager._Instance.AddResource(m_resourceAmount, m_resourceType);
    }
}