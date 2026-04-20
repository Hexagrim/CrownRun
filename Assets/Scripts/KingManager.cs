using System.Collections;
using System.Threading;
using UnityEngine;

public class KingManager : MonoBehaviour
{
    public GameObject Red, Green, Blue, Yellow;
    public GameObject king;

    bool canTransfer = true;
    void Start()
    {
        
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
