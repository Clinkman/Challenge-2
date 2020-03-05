using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public GameObject thePlayer;
    public float speed;
    public Text Win;
    public Text score;
    public Text Lives;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    private bool facingRight;

    private int scoreValue = 0;
    private int Lv;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        Lv = 3;
        SetLives();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        Flip(hozMovement);
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (rd2d.velocity.x == 0)
        {
            anim.SetInteger("Stuff", 0);
        }
        else
        {
            anim.SetInteger("Stuff", 1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("Stuff", 2);
        }
  
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetWinText();
        }
        if (collision.collider.tag == "Coin2")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetWinText2();
        }
        if (collision.collider.tag == "UFO")
        {
            Lv -= 1;
            Destroy(collision.collider.gameObject);
            SetLives();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetWinText()
    {
        if(scoreValue >= 4)
        {
            thePlayer.transform.position = new Vector4(45, 0, 0);
            Lv = 3;
            SetLives();
            scoreValue = 0;
            score.text = scoreValue.ToString();
        }
    }
    void SetWinText2()
    {
        if(scoreValue >= 4)
        {
            Win.text = "You win! Game created by Curtis Marcoux!";
            Lv = 3;
            SetLives();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
        }
    }

    void SetLives()
    {
        Lives.text = "Lives: " + Lv.ToString();
        if(Lv <= 0)
        {
            Win.text = "You Lose!";
            scoreValue = 0;
        }
    }
}
