using Unity.Netcode;
using UnityEngine;

public class CrownShowMech : NetworkBehaviour
{
    private KingManager kingManager;
    void Start()
    {
        KingManager kingManager = FindFirstObjectByType<KingManager>();


    }

    void Update()
    {
        if (kingManager == null) return;

        if(OwnerClientId != kingManager.kingClientId.Value)
        {
            FindChildWithTag("Crown").GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            FindChildWithTag("Crown").GetComponent<SpriteRenderer>().enabled = true;
        }

    }
    Transform FindChildWithTag(string tag)
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag))
                return t;
        }
        return null;
    }
}
