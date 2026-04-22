using System.Collections;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject leftLeg;
    public GameObject rightLeg;
    Rigidbody2D leftLegRB;
    Rigidbody2D rightLegRB;
    public Rigidbody2D rb;
    public Animator Anim;

    public float speed = 2f;
    [SerializeField] float stepWait = 0.5f;
    public float jumpForce;
    private bool isOnGround;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;
    private GameObject Camera;

    public KeyCode Right, Left, Jump;

    void Start()
    {
        Camera = FindFirstObjectByType<Camera>().gameObject;
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);

        if (isOnGround && Input.GetKeyDown(Jump))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (Input.GetKey(Right))
        {
            Anim.Play("WalkRight");
            StartCoroutine(MoveRight(stepWait));
        }
        else if (Input.GetKey(Left))
        {
            Anim.Play("WalkLeft");
            StartCoroutine(MoveLeft(stepWait));
        }
        else
        {
            Anim.Play("Idle");

            leftLegRB.linearVelocity *= 0.95f;
            rightLegRB.linearVelocity *= 0.95f;
            rb.linearVelocity *= 0.95f;
        }
    }

    IEnumerator MoveRight(float seconds)
    {
        float force = speed * 1000;
        if (leftLegRB.linearVelocity.x <= 0) force *= 3f;
        if (rightLegRB.linearVelocity.x <= 0) force *= 3f;

        leftLegRB.AddForce(Vector2.right * force * Time.deltaTime);
        yield return new WaitForSeconds(seconds);

        force = speed * 1000;
        if (rightLegRB.linearVelocity.x <= 0) force *= 2.5f;

        rightLegRB.AddForce(Vector2.right * force * Time.deltaTime);
    }

    IEnumerator MoveLeft(float seconds)
    {
        float force = speed * 1000;
        if (rightLegRB.linearVelocity.x >= 0) force *= 3f;
        if (leftLegRB.linearVelocity.x >= 0) force *= 3f;

        rightLegRB.AddForce(Vector2.left * force * Time.deltaTime);
        yield return new WaitForSeconds(seconds);

        force = speed * 1000;
        if (leftLegRB.linearVelocity.x >= 0) force *= 2.5f;

        leftLegRB.AddForce(Vector2.left * force * Time.deltaTime);
    }
}
