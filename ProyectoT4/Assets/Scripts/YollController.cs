using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class YollController : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float velocity = 20;
    public float JumpForce = 10;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;

    private static readonly int right = 1;
    private static readonly int left = -1;

    private static readonly int idle = 0;
    private static readonly int run = 1;
    private static readonly int jump = 2;
    private static readonly int attack = 3;
    private static readonly int dead = 4;
    private static readonly int roll = 5;

    private float time = 0f;
    private Puntajes Scors;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        ChangeAnimation(idle);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Desplazarse(right);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Desplazarse(left);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeAnimation(jump);
            rb2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            ChangeAnimation(attack);
            //Disparar2();
        }

        if (Scors.miVida == 0)
        {
            SceneManager.LoadScene("Nivel 2");
        }

        timeKey();
    }

    void timeKey()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            time = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            time = Time.time - time;
            Debug.Log("Pressed for : " + time + " Seconds");
            if (time >= 2f)
            {
                ChangeAnimation(attack);
               // Disparar();
            }
        }
    }
    
    private void Desplazarse(int position)
    {
        rb2D.velocity = new Vector2(velocity * position, rb2D.velocity.y);
        spriteRenderer.flipX = position == left;
        ChangeAnimation(run);
    }

    /*private void Disparar()
    {
        //cear elementos en tiempo de ejecuccion
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var bullgo = Instantiate(kunaiPrefabs, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<CucchilloController>();

        controller.setController(this);

        if (spriteRenderer.flipX)
        {

            controller.velocity = controller.velocity * -1;
        }
    }

    private void Disparar2()
    {
        //crear elementos en tiempo de ejecuccion
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var bullgo = Instantiate(bulletPrefabs2, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<CucchilloController>();

        controller.SetController(this);

        if (spriteRenderer.flipX)
        {

            controller.velocity = controller.velocity * -1;
        }
    }*/

    private void ChangeAnimation(int animation)
    {
        //animation.SetInteger("Estado", animation);
    }
}
