using Unity.Netcode;
using UnityEngine;

public class CountdownServer : NetworkBehaviour
{
    public NetworkVariable<float> duration = new NetworkVariable<float>();
    public NetworkVariable<double> startTime = new NetworkVariable<double>();
    public NetworkVariable<bool> isRunning = new NetworkVariable<bool>();
    public bool started;
    public bool finished;
    public override void OnNetworkSpawn()
    {
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

    public void StartCountdownRpc(float newDuration)
    {

        started = true;
        duration.Value = newDuration;
        startTime.Value = NetworkManager.Singleton.ServerTime.Time;
        isRunning.Value = true;
    }

    [Rpc(SendTo.Server)]
    public void StopCountdownRpc()
    {

        started = false;
        float remaining = GetRemainingTime();
        started = false;
        duration.Value = remaining;
        isRunning.Value = false;
    }

    [Rpc(SendTo.Server)]
    public void AddTimeRpc(float extraTime)
    {

        float remaining = GetRemainingTime();

        duration.Value = remaining + extraTime;
        startTime.Value = NetworkManager.Singleton.ServerTime.Time;
        isRunning.Value = true;
    }
}
