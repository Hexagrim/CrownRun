using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class CamShake : NetworkBehaviour
{
    public GameObject Vcam;

    [Rpc(SendTo.ClientsAndHost)]
    public void ShakeClientRpc()
    {
        Shake();
    }

    void Shake()
    {
        StartCoroutine(ShakeCam(2f, 0.5f));
    }
    public IEnumerator ShakeCam(float intensity, float duration)
    {
        var noise = Vcam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        noise.AmplitudeGain = intensity;
        noise.FrequencyGain = intensity;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }
}
