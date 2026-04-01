using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Movement : NetworkBehaviour
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        Camera = FindFirstObjectByType<Camera>().gameObject;
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();

        if (!IsOwner)
        {
            FindChildWithTag("Indicator").gameObject.SetActive(false);
        }
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
        if (!IsOwner)
        {
            return;
        }
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);

        if(isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("HEYAYAYYAAYAYA");
            rb.AddForce(Vector2.up * jumpForce);
        }

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                Anim.Play("WalkRight");
                StartCoroutine(MoveRight(stepWait));
            }
            else
            {
                Anim.Play("WalkLeft");
                StartCoroutine(MoveLeft(stepWait));
            }
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
