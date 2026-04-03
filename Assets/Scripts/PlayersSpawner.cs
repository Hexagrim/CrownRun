using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class PlayersSpawner : MonoBehaviour
{
    public void SpawnPlayers(string sceneName)
    {
        if (!NetworkManager.Singleton.IsServer) return;
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
    }

    private void OnSceneLoaded(string sceneName, LoadSceneMode mode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (!NetworkManager.Singleton.IsServer) return;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        var players = NetworkManager.Singleton.ConnectedClientsIds.ToList();
        for (int i = 0; i < players.Count; i++)
        {
            ulong clientid = players[i];
            var playerObj = NetworkManager.Singleton.ConnectedClients[clientid].PlayerObject;

            if (playerObj == null || spawnPoints.Length == 0) continue;
            GameObject randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 pos = randomSpawn.transform.position;
            Quaternion rot = randomSpawn.transform.rotation;
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
