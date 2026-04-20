using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    private List<GameObject> spawnPoints = new List<GameObject>();

    public void SpawnPlayers(string sceneName)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
    }

    private void OnSceneLoaded(string sceneName, LoadSceneMode mode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new List<GameObject>(points);

        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            var playerObj = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObj == null || spawnPoints.Count == 0) continue;

            if (spawnPoints.Count == 0)
            {
                spawnPoints = new List<GameObject>(points);
            }

            int index = Random.Range(0, spawnPoints.Count);
            GameObject chosen = spawnPoints[index];
            spawnPoints.RemoveAt(index);

            Vector2 pos = chosen.transform.position;
            Quaternion rot = chosen.transform.rotation;

            playerObj.transform.SetPositionAndRotation(pos, rot);

            Transform body = playerObj.transform.Find("Body");
            if (body != null)
            {
                body.SetPositionAndRotation(pos, rot);
            }
        }

        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnSceneLoaded;
    }
}
