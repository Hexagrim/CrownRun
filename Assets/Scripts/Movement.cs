using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject leftLeg;
    public GameObject rightLeg;
    Rigidbody2D leftLegRB;
    Rigidbody2D rightLegRB;

    public Animator Anim;

    [SerializeField] float speed = 2f;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftLegRB = GetComponent<Rigidbody2D>();
        rightLegRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                Anim.Play("WalkRight");
            }
            else
            {
                Anim.Play("WalkLeft");
            }
        }
    }
    IEnumerator MoveRight()
    {

    }
}
