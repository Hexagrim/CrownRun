using System.Collections;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public bool isSheilded;
    public float highJumpSpeed;
    public GameObject highJumpEffect;
    public GameObject kaboom;

    Movement mov;

    bool HJ;
    
    void Start()
    {
        mov = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator HighJump()
    {
        highJumpEffect.SetActive(true);
        float normalForce = mov.jumpForce;
        mov.jumpForce = highJumpSpeed;
        yield return new WaitForSeconds(5f);
        mov.jumpForce = normalForce;
        highJumpEffect.SetActive(false);
        HJ = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("highJump"))
        {
            if (!HJ)
            {
                StartCoroutine(HighJump());
                Instantiate(kaboom, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                HJ = true;
            }
        }
    }
}
