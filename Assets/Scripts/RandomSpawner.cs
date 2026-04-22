using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform[] spawnPoints;

    private List<Transform> availablePoints = new List<Transform>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (availablePoints.Count == 0)
            {
                availablePoints = new List<Transform>(spawnPoints);
            }
            yield return new WaitForSeconds(5f);
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            int index = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[index];
            availablePoints.RemoveAt(index);
            GameObject spawned = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            float timer = 0f;
            while (timer < 15f)
            {
                if (spawned == null)
                {
                    yield return new WaitForSeconds(5f);
                    break;
                }
                timer += Time.deltaTime;
                yield return null;
            }

            if (spawned != null)
            {
                Destroy(spawned);
            }
        }
    }
}
