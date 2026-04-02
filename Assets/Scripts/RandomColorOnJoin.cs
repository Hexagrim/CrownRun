using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RandomColorOnJoin : NetworkBehaviour
{
    [System.Serializable]
    public struct NamedColor
    {
        public string name;
        public Color color;
    }
    [Header("give five colours here")]
    public NamedColor[] availableColors;
    private SpriteRenderer[] sprites;
    public NetworkVariable<int> colorIndex = new NetworkVariable<int>(
        -1,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    private static HashSet<int> usedIndices = new HashSet<int>();

    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        colorIndex.OnValueChanged += OnColorChanged;

        if (IsServer)
        {
            AssignColorServerSide();
        }
        if (colorIndex.Value != -1)
        {
            ApplyColor(colorIndex.Value);
        }
    }

    new private void OnDestroy()
    {
        colorIndex.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged(int oldIndex, int newIndex)
    {
        ApplyColor(newIndex);
    }

    private void AssignColorServerSide()
    {
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < availableColors.Length; i++)
        {
            if (!usedIndices.Contains(i))
                availableIndices.Add(i);
        }

        int chosenIndex;

        if (availableIndices.Count > 0)
        {
            chosenIndex = availableIndices[Random.Range(0, availableIndices.Count)];
        }
        else
        {
            chosenIndex = Random.Range(0, availableColors.Length);
        }

        usedIndices.Add(chosenIndex);
        colorIndex.Value = chosenIndex;
    }

    private void ApplyColor(int index)
    {
        if (index < 0 || index >= availableColors.Length) return;

        Color color = availableColors[index].color;

        foreach (SpriteRenderer sr in sprites)
        {
            if (sr.gameObject.CompareTag("Player"))
            {
                sr.color = color;
            }
        }
    }


    public string GetColorName()
    {
        int index = colorIndex.Value;

        if (index < 0 || index >= availableColors.Length)
            return "Unknown";

        return availableColors[index].name;
    }

    public Color GetColor()
    {
        int index = colorIndex.Value;

        if (index < 0 || index >= availableColors.Length)
            return Color.white;

        return availableColors[index].color;
    }
}
