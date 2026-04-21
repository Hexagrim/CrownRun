using System.Collections;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public bool isShielded;
    public float highJumpSpeed;
    public GameObject highJumpEffect, shieldEffect;
    public GameObject kaboom;

    Movement mov;

    bool HJ;
    
    void Start()
    {
        mov = GetComponentInParent<Movement>();
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
        yield return new WaitForSeconds(10f);
        mov.jumpForce = normalForce;
        highJumpEffect.SetActive(false);
        HJ = false;
    }
    public IEnumerator Shield()
    {
        shieldEffect.SetActive(true);
        yield return new WaitForSeconds(10f);
        shieldEffect.SetActive(false);
        isShielded = false;
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
        if (collision.gameObject.CompareTag("highJump"))
        {
            if (!isShielded)
            {
                StartCoroutine(Shield());
                Instantiate(kaboom, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                isShielded = true;
            }
        }
    }
}
