using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Velocity = 10;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float videnemy = 3;
    private float videnemy2 = 6;

    private YollController Yoll;
    public void SetPlayerController(YollController playerController)
    {
        Yoll = playerController;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sr.flipX == true)
        {
            rb.velocity = new Vector2(Velocity * -1, rb.velocity.y);
        }
        if (sr.flipX == false)
        {
            rb.velocity = new Vector2(Velocity * 1, rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;

        if (tag == "Limite2")
        {
            sr.flipX = true;
        }
        if (tag == "Limite")
        {
            sr.flipX = false;
        }
        if (tag == "bola1")
        {
            videnemy -= 1;
            Debug.Log(videnemy);
            if (videnemy <= 0)
            {
                Destroy(this.gameObject);

            }
            videnemy2 -= 1;
            Debug.Log(videnemy2);
            if (videnemy2 <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (tag == "bola2")
        {
            videnemy -= 2;
            Debug.Log(videnemy);
            if (videnemy <= 0)
            {
                Destroy(this.gameObject);

            }
            videnemy2 -= 2;
            Debug.Log(videnemy2);
            if (videnemy2 <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
