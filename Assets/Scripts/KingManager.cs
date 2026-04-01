using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class KingManager : NetworkBehaviour
{
    public NetworkVariable<ulong> kingClientId = new NetworkVariable<ulong>();

    private bool transferInProgress = false;
    private ulong pendingKingId;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("Server LocalClientId: " + NetworkManager.Singleton.LocalClientId);
            kingClientId.Value = NetworkManager.Singleton.LocalClientId; 
        }
    }
    [ServerRpc]
    public void RequestKingTransferServerRpc(ulong newKingId)
    {
        if (!IsServer) return;

        if (transferInProgress) return;
        if (newKingId == kingClientId.Value) return;

        transferInProgress = true;
        pendingKingId = newKingId;

        StartCoroutine(TransferAfterDelay());
    }

    void Update()
    {

    }
    private IEnumerator TransferAfterDelay()
    {
        kingClientId.Value = pendingKingId;

        yield return new WaitForSeconds(1f);


        transferInProgress = false;
    }
}
