using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class YollController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Text Vidas;
    public Text Monedas;

    public int vida = 8;
    public int monedas = 0;

    private string moneyData = "";
    private string vidaData = "";

    public GameObject flechaPrefabs;
    public GameObject espadaPrefabs;

    public float velocity = 20;
    public float JumpForce = 10;

    public AudioClip[] audioClips;

    private static readonly int right = 1;
    private static readonly int left = -1;

    private static readonly int idle = 0;
    private static readonly int run = 1;
    private static readonly int jump = 2;
    private static readonly int attack = 3;
    private static readonly int dead = 4;

    private float time = 0f;
    private Puntajes Scors;

    //private AudioSource audioSor;

    private void Awake()
    {
        loadData();
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Scors = FindObjectOfType<Puntajes>();
        //audioSor = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vidas.text = "" + vida;
        Monedas.text = "" + monedas;

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
            //audioSor.PlayOneShot(audioClips[0]);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            ChangeAnimation(attack);
            Disparar2();
            //audioSor.PlayOneShot(audioClips[2]);
        }

        /*if (Scors.miVida == 0)
        {
            audioSor.PlayOneShot(audioClips[1]);
            SceneManager.LoadScene("Nivel 1");
        }*/

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
               // audioSor.PlayOneShot(audioClips[2]);
                Disparar();
            }
        }
    }
    
    private void Desplazarse(int position)
    {
        rb2D.velocity = new Vector2(velocity * position, rb2D.velocity.y);
        spriteRenderer.flipX = position == left;
        ChangeAnimation(run);
    }

    private void Disparar()
    {
        //cear elementos en tiempo de ejecuccion
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var bullgo = Instantiate(flechaPrefabs, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<EspadaController>();

        controller.SetController(this);

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

        var bullgo = Instantiate(espadaPrefabs, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<EspadaController>();

        controller.SetController(this);

        if (spriteRenderer.flipX)
        {

            controller.velocity = controller.velocity * -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Pinchos")
        {
            SceneManager.LoadScene("PrimerNivel");
        }
        if (tag == "Pinchos2")
        {
            SceneManager.LoadScene("SegundoNivel");
        }
        if (tag == "Pinchos3")
        {
            SceneManager.LoadScene("TercerNivel");
        }
        if (tag == "Pinchos4")
        {
            SceneManager.LoadScene("NivelFinal");
        }
        if (tag == "Nivel1")
        {
            SceneManager.LoadScene("PrimerNivel");
        }
        if (tag == "Nivel2")
        {
            SceneManager.LoadScene("SegundoNivel");
        }
        if (tag == "Nivel3")
        {
            SceneManager.LoadScene("TercerNivel");
        }
        if (tag == "Nivel4")
        {
            SceneManager.LoadScene("NivelFinal");
        }
        if (tag == "moneda1")
        {
            monedas += 1;
            Destroy(other.gameObject);
            //audioSor.PlayOneShot(audioClips[3]);
        }
        if (tag == "DisparoJefe")
        {
            Scors.MenosVida(3);
            vida -= 3;
            if (vida <= 0)
            {
                //audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
        if (tag == "Final")
        {
            SceneManager.LoadScene("Menus");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemigo")
        {
            Scors.MenosVida(1);
            vida -= 1;
            if (vida <= 0)
            {
               // audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
        if (tag == "Enemigo2")
        {
            Scors.MenosVida(2);
            vida -= 2;
            if (vida <= 0)
            {
                //audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
        if (tag == "Jefe")
        {
            Scors.MenosVida(3);
            vida -= 3;
            if (vida <= 0)
            {
               // audioSor.PlayOneShot(audioClips[1]);
                ChangeAnimation(dead);
                SceneManager.LoadScene("PrimerNivel");
            }
        }
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }

    private void OnDestroy()
    {
        //saveData();
        loadData();
    }

    private void saveData()
    {
        PlayerPrefs.SetInt(moneyData, monedas);
        PlayerPrefs.SetInt(vidaData, vida);
    }

    private void loadData()
    {
        monedas = PlayerPrefs.GetInt(moneyData, 0);
        vida = PlayerPrefs.GetInt(vidaData, 8);
    }
}
