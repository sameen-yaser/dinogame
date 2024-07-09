using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    Animator anim;
    public CharacterController2D controller;

    public float runSpeed = 40f;
    private float startTouchPosition, endTouchPosition;

    float lefthorizontalMove = -40f;
    float righthorizontalMove = 40f;

    float speed = 0.05f;

    bool jump = false;
    bool crouch = false;

    public float health = 100;
    public Slider healthBar;

    float score;
    public Text scoreText;
    public static float highestscore = 0;
    public Text HighestScoreText;
    public GameObject cloud;

    // Start is called before the first frame update
    void Start()
    {
        highestscore = PlayerPrefs.GetFloat("MyHighestScore");
        anim = GetComponent<Animator>();
        scoreText.text = "Score: " + score.ToString();
        HighestScoreText.text = "Highest Score " + highestscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //for swipe Jump

        /*if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector2.up * 10 * Time.fixedDeltaTime);
        }*/

        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position.y;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position.y;
            }

            if (endTouchPosition > startTouchPosition)
            {
                transform.Translate(Vector2.up * 10 * Time.fixedDeltaTime);
            }
        }
    }



    private void FixedUpdate()
    {

        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     controller.Move(lefthorizontalMove * Time.fixedDeltaTime, crouch, jump);
        //   anim.SetTrigger("walk");
        // }

        //  else if (Input.GetKey(KeyCode.RightArrow))
        //{

        //  controller.Move(righthorizontalMove * Time.fixedDeltaTime, crouch, jump);
        //anim.SetTrigger("walk");
        // }
        // else
        //{
        // anim.SetTrigger("idle");

        // }

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                controller.Move(lefthorizontalMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetTrigger("walk");
            }

            else if (touch.position.x > Screen.width / 2)
            {
                controller.Move(righthorizontalMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetTrigger("walk");
            }

            else
            {
                anim.SetTrigger("idle");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name.StartsWith("Saw"))
        {
            if (health > 0)
            {
                health -= 10;
                healthBar.value = health;
            }
            else
            {
                anim.SetTrigger("die");
                SceneManager.LoadScene("Scenes/youloose");
            }
        }
        if(col.gameObject.name.StartsWith("apple"))
        {
            health += 10;
            healthBar.value = health;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name.StartsWith("coin"))
        {
            if (score < 95)
            {
                score += 10;
                if(score> highestscore)
                {
                    highestscore = score;
                    HighestScoreText.text = "Highest Score: " + highestscore.ToString();
                }
                scoreText.text = "Score: " + score.ToString();
                Destroy(col.gameObject);
                PlayerPrefs.SetFloat("MyHighestScore", highestscore);
                StartCoroutine(delayOnDestroyingEnemy());
            }
            if (score == 100)
            {
                SceneManager.LoadScene("youwin");
            }
        }
        if(col.gameObject.name.StartsWith("cloud"))
        {
            transform.gameObject.transform.parent = cloud.transform;
        }
        if (!col.gameObject.name.StartsWith("cloud"))
        {
            transform.gameObject.transform.parent = null;
        }
        if (col.gameObject.name.StartsWith("Spike"))
        {
            anim.SetTrigger("die");
            SceneManager.LoadScene("Scenes/youloose");
        }
    }
    private IEnumerator delayOnDestroyingEnemy()
    {
        yield return new WaitForSeconds(5);
    }
        }
