using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;
    public Text winText;
    public Text PlayerLivesText;
    private int playerLive;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;
    public AudioSource musicSource;


    Animator anim;
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        playerLive = 3;
        SetCountText();
        anim = GetComponent<Animator>();


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetCountText();
        }
        else if (collision.collider.tag == "Enemy")
        {
            playerLive = playerLive - 1;
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector2(35.0f, 0.0f);
            if (playerLive == 1)
            {
                playerLive = playerLive + 2;
            }
            else if (playerLive == 2)
            {
                playerLive = playerLive + 1;
            }


        }



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }


    }
    void SetCountText()
    {
        PlayerLivesText.text = "Player Lives :" + playerLive.ToString();
        score.text = "Score:" + scoreValue.ToString();
        if (scoreValue >= 9)
        {
            winText.text = "You win! Game created by HangZheng!";

        }
        else if (playerLive <= 0)
        {
            Destroy(gameObject);
            winText.text = "You Lose";

        }

    }
    private void LateUpdate()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 3);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
            if (scoreValue >= 9 == true)
            {
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }

        }
        if (Input.GetKey("escape"))

            Application.Quit();

    
}
}

    






       