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
    [Rpc(SendTo.Server)]
    public void SpawnParticlesRpc(Vector2 Pos , Color col)
    {
        GameObject obj = Instantiate(Particle, Pos, Quaternion.identity);

        var netObj = obj.GetComponent<NetworkObject>();

        netObj.Spawn();

        ApplyParticleColorClientRpc(netObj.NetworkObjectId, col);

        StartCoroutine(DestroyObj(1, netObj));


    }
    public IEnumerator DestroyObj(float t,NetworkObject obj)
    {
        yield return new WaitForSeconds(t);
        obj.Despawn();
    }
    [Rpc(SendTo.ClientsAndHost)]
    void ApplyParticleColorClientRpc(ulong netObjectId, Color col)
    {
        StartCoroutine(ApplyWhenReady(netObjectId, col));
    }

    IEnumerator ApplyWhenReady(ulong netObjectId, Color col)
    {
        NetworkObject netObj;

        while (!NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(netObjectId, out netObj))
        {
            yield return null;
        }

        var ps = netObj.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = col;
    }

}
