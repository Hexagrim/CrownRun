using Unity.Netcode;
using UnityEngine;

public class CountdownServer : NetworkBehaviour
{
    public NetworkVariable<float> duration = new NetworkVariable<float>();
    public NetworkVariable<double> startTime = new NetworkVariable<double>();
    public NetworkVariable<bool> isRunning = new NetworkVariable<bool>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            duration.Value = 0f;
            startTime.Value = 0;
            isRunning.Value = false;
        }
    }

    public float GetRemainingTime()
    {
        if (!isRunning.Value) return duration.Value;

        double currentTime = NetworkManager.Singleton.ServerTime.Time;
        double elapsed = currentTime - startTime.Value;
        float remaining = duration.Value - (float)elapsed;

        if (remaining < 0) return 0;
        return remaining;
    }

    public bool IsFinished()
    {
        return GetRemainingTime() <= 0f;
    }

    [Rpc(SendTo.Server)]
    public void StartCountdownRpc(float newDuration)
    {
        if (!IsServer) return;

        duration.Value = newDuration;
        startTime.Value = NetworkManager.Singleton.ServerTime.Time;
        isRunning.Value = true;
    }

    [Rpc(SendTo.Server)]
    public void StopCountdownRpc()
    {
        if (!IsServer) return;

        float remaining = GetRemainingTime();

        duration.Value = remaining;
        isRunning.Value = false;
    }

    [Rpc(SendTo.Server)]
    public void AddTimeRpc(float extraTime)
    {
        if (!IsServer) return;

        float remaining = GetRemainingTime();

        duration.Value = remaining + extraTime;
        startTime.Value = NetworkManager.Singleton.ServerTime.Time;
        isRunning.Value = true;
    }
}
