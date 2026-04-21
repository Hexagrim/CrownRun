using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KingManager : MonoBehaviour
{
    public GameObject Red, Green, Blue, Yellow;
    public GameObject king;

    bool canTransfer = true;
    void Start()
    {
        GameObject[] players = new GameObject[] { Red, Green, Blue, Yellow };
        List<GameObject> activePlayers = new List<GameObject>();
        foreach (var p in players)
        {
            if (p != null)
                activePlayers.Add(p);
        }
        int rand = Random.Range(0, activePlayers.Count);
        king = activePlayers[rand];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransferKing(GameObject k)
    {
        if (!canTransfer) return;
        king = k;
        StartCoroutine(Cooldown());


    }
    IEnumerator Cooldown()
    {
        canTransfer = false;
        yield return new WaitForSeconds(0.1f);
        canTransfer = true;

    }
}
