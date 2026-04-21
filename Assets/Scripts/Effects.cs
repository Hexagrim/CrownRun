using System.Collections;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public bool isShielded;
    public float highJumpSpeed, boostedSpeed;
    public GameObject highJumpEffect, shieldEffect, speedEffect;
    public GameObject kaboom;

    Movement mov;

    bool HJ;
    bool speed;
    void Start()
    {
        mov = GetComponentInParent<Movement>();
    }
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
    public IEnumerator Speed()
    {
        speedEffect.SetActive(true);
        float normalForce = mov.speed;
        mov.speed = boostedSpeed;
        yield return new WaitForSeconds(10f);
        mov.speed = normalForce;
        speedEffect.SetActive(false);
        speed = false;
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
        if (collision.gameObject.CompareTag("shield"))
        {
            if (!isShielded)
            {
                StartCoroutine(Shield());
                Instantiate(kaboom, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                isShielded = true;
            }
        }
        if (collision.gameObject.CompareTag("speed"))
        {
            if (!speed)
            {
                StartCoroutine(Speed());
                Instantiate(kaboom, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                speed = true;
            }
        }
    }
}
