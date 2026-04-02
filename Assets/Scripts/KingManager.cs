using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class KingManager : NetworkBehaviour
{
    public GameObject Particle;
    public NetworkVariable<ulong> kingClientId = new NetworkVariable<ulong>();
    bool initialized;
    private bool transferInProgress = false;
    private ulong pendingKingId;
    private void Awake()
    {
        if (!NetworkManager.Singleton.IsServer) return;
        if (initialized) return;

        kingClientId.Value = NetworkManager.Singleton.LocalClientId;

        initialized = true;
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("Server LocalClientId: " + NetworkManager.Singleton.LocalClientId);
            kingClientId.Value = NetworkManager.Singleton.LocalClientId; 
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
        ShakeAll();
        StartCoroutine(TransferAfterDelay());
    }

    void Update()
    {
    }
    private IEnumerator TransferAfterDelay()
    {
        //GetNetworkObject(kingClientId.Value).transform.Find("Head").position
        GameObject obj = Instantiate(Particle, new Vector2(0,0), Quaternion.identity);

        var ps = obj.GetComponent<ParticleSystem>();
        var main = ps.main;

        main.startColor = GetNetworkObject(kingClientId.Value)
            .transform.Find("Head")
            .GetComponent<SpriteRenderer>().color;

        obj.GetComponent<NetworkObject>().Spawn();

        kingClientId.Value = pendingKingId;


        yield return new WaitForSeconds(1f);
        transferInProgress = false;
    }
    public void ShakeAll()
    {
        if (!IsServer) return;

        foreach (var obj in FindObjectsByType<CamShake>(FindObjectsSortMode.None))
        {
            if (obj.IsSpawned)
            {
                obj.ShakeClientRpc();
            }
        }
    }

}
