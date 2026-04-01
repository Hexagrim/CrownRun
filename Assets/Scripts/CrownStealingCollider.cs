using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class CrownStealingCollider : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerCol")) return;
        if(collision.gameObject.GetComponentInParent<NetworkObject>().OwnerClientId != OwnerClientId)
        {
            var kingManager = FindFirstObjectByType<KingManager>();
            var netObj = collision.gameObject.GetComponentInParent<NetworkObject>();

            if (kingManager != null && netObj != null)
            {
                kingManager.RequestKingTransferServerRpc(netObj.OwnerClientId);
            }

        }
    }
}
