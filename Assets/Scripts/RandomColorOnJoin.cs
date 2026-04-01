using Unity.Netcode;
using UnityEngine;

public class RandomColorOnJoin : NetworkBehaviour
{
    private SpriteRenderer[] sprites;

    // Synced color (owner writes, everyone reads)
    public NetworkVariable<Color32> playerColor = new NetworkVariable<Color32>(
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        // Owner picks color ONCE
        if (IsOwner)
        {
            Color32 color = GetRandomVibrantColor();

            ApplyColor(color);          // instant local feedback
            playerColor.Value = color;  // sync to others
        }
        else
        {
            // Non-owners apply whatever value is already set
            ApplyColor(playerColor.Value);
        }

        // Listen for updates (for everyone)
        playerColor.OnValueChanged += OnColorChanged;
    }

    private void OnColorChanged(Color32 oldColor, Color32 newColor)
    {
        ApplyColor(newColor);
    }

    private void ApplyColor(Color32 color)
    {
        foreach (SpriteRenderer sr in sprites)
        {
            if (sr.gameObject.CompareTag("Player"))
            {
                sr.color = color;
            }
        }
    }

    private Color32 GetRandomVibrantColor()
    {
        float hue = Random.value;

        float saturation = Mathf.Clamp01(0.9f + Random.value * 0.1f);
        float value = Mathf.Clamp01(0.9f + Random.value * 0.1f);

        Color color = Color.HSVToRGB(hue, saturation, value);

        return new Color32(
            (byte)(color.r * 255),
            (byte)(color.g * 255),
            (byte)(color.b * 255),
            255
        );
    }
}
