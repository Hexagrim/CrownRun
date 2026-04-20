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

    [SerializeField] float speed = 2f;
    [SerializeField] float stepWait = 0.5f;
    public float jumpForce;
    private bool isOnGround;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;
    private GameObject Camera;


    public KeyCode Right, Left, Jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera = FindFirstObjectByType<Camera>().gameObject;
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();


    }

    Transform FindChildWithTag(string tag)
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag))
                return t;
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);

        if (isOnGround && Input.GetKeyDown(Jump))
        {
            Debug.Log("HEYAYAYYAAYAYA");
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
        }
    }
    IEnumerator MoveRight(float seconds)
    {
        leftLegRB.AddForce(Vector2.right * (speed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        rightLegRB.AddForce(Vector2.right * (speed * 1000) * Time.deltaTime);
    }
    IEnumerator MoveLeft(float seconds)
    {
        rightLegRB.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        leftLegRB.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
    }
}
