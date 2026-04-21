using UnityEngine;

public class CrownCollider : MonoBehaviour
{
    KingManager KM;
    public GameObject obj;
    void Start()
    {
        KM = FindFirstObjectByType<KingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Effects>())
        {
            if(collision.GetComponentInParent<Effects>().isShielded)
            {
                return;
            }
        }
        if (collision.CompareTag("Red") && KM.king == KM.Red)
        {
            KM.TransferKing(obj);
        }
        if (collision.CompareTag("Green") && KM.king == KM.Green)
        {
            KM.TransferKing(obj);
        }
        if (collision.CompareTag("Blue") && KM.king == KM.Blue)
        {
            KM.TransferKing(obj);
        }
        if (collision.CompareTag("Yellow") && KM.king == KM.Yellow)
        {
            KM.TransferKing(obj);
        }
    }
}
