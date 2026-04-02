using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingManager : NetworkBehaviour
{
    public NetworkVariable<ulong> kingClientId = new NetworkVariable<ulong>();

    private bool transferInProgress = false;
    private ulong pendingKingId;

    bool initKing;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("Server LocalClientId: " + NetworkManager.Singleton.LocalClientId);
            kingClientId.Value = NetworkManager.Singleton.LocalClientId; 
        }
    }

    private void Awake()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnSceneLoaded;
        }
    }
    [Rpc(SendTo.Server)]
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
    private void OnSceneLoaded(string sceneName, UnityEngine.SceneManagement.LoadSceneMode mode,
        List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        Debug.Log("Scene fully loaded on server: " + sceneName);

        if (!initKing)
        {
            Debug.Log("Server LocalClientId: " + NetworkManager.Singleton.LocalClientId);
            kingClientId.Value = NetworkManager.Singleton.LocalClientId;
            initKing = true;
        }

    }
}
