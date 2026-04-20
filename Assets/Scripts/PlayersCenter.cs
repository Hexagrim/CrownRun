
using UnityEngine;

public class PlayersCenter : MonoBehaviour
{
    public Transform[] Players;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = GetCenter(Players);
    }
    Vector3 GetCenter(Transform[] targets)
    {
        if (targets == null || targets.Length == 0)
            return Vector3.zero;
        Vector3 sum = Vector3.zero;
        for (int i = 0; i < targets.Length; i++)
        {
            sum += targets[i].position;
        }
        return sum / targets.Length;
    }
}
