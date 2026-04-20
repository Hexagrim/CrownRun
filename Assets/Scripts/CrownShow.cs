using UnityEngine;

public class CrownShow : MonoBehaviour
{
    KingManager KM;
    public GameObject Crown;
    public GameObject parentObj;
    void Start()
    {
        KM=FindFirstObjectByType<KingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Crown.SetActive(KM.king == parentObj);
    }
}
