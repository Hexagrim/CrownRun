using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform parent = collision.transform .parent;
            Transform target = parent.Find("Body");
            target.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
        }
    }
}
