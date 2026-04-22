using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public List<Transform> players;

    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float padding = 2f;

    private CinemachineCamera vcam;
    private Camera cam;

    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (players == null || players.Count == 0) return;
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 1; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        transform.position = new Vector3(bounds.center.x, bounds.center.y, transform.position.z);
        float aspect = 16 / 9;
        float width = bounds.size.x + padding;
        float height = bounds.size.y + padding;
        float sizeFromWidth = width / aspect / 2f;
        float sizeFromHeight = height / 2f;
        float requiredSize = Mathf.Max(sizeFromWidth, sizeFromHeight);
        vcam.Lens.OrthographicSize = Mathf.Clamp(requiredSize, minZoom, maxZoom);
    }
    public void Shake()
    {
        StartCoroutine(ShakeCam(2f, 0.5f));
    }
    public IEnumerator ShakeCam(float intensity, float duration)
    {
        var noise = vcam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        noise.AmplitudeGain = intensity;
        noise.FrequencyGain = intensity;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }
}
