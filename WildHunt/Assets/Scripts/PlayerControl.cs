using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    public float speed;
    public float fallSpeed;
    public float jumpSpeed;
    public GameObject p;
    public Text currencyText;
    public Slider healthBar;
    public Text level;


    private int hp;
    private bool directionFacing;
    private Vector3 offset;
    public float xMove;
    //private bool isHit = false;
    private bool isGrounded;
    private bool isWalled;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        hp = 100;
        offset = transform.position - p.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Updates the UI and if the player dies.
        if (hp == 0)
            Die();
        level.text = "Level: " + GameManager.instance.GetLevel();
        healthBar.value = hp;
        currencyText.text = "Score: " + GameManager.instance.GetCurrency();
    }
    private void LateUpdate()
    {
        //Camera follows player
        Camera.main.transform.position = new Vector3(p.transform.position.x + offset.x, p.transform.position.y + offset.y, Camera.main.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xMove = Input.GetAxis("Horizontal");
        Vector2 move;
        //This code is the jump for when they are grounded
        if (isGrounded && Input.GetKeyDown("space"))
        {
            move = new Vector2(xMove, 20);
            rb.AddForce(move * jumpSpeed);
        }
        else if (isWalled && Input.GetKeyDown("space")) //This code is for the wall jump
        {
            if (xMove > 0)
            {
                move = new Vector2(-20, 20);
            }
            else
            {
                move = new Vector2(20, 20);
            }

            rb.AddRelativeForce(move * jumpSpeed);
        }
        else if (isGrounded) // This is for grounded movement
        {
            move = new Vector2(xMove, 0);
            rb.AddForce(move * speed);
        }
        else // This is for falling movement
        {
            move = new Vector2(xMove, 0);
            rb.AddForce(move * speed);
            rb.AddForce(Vector2.down * fallSpeed);
        }

        //Flips the player based on the direction
        if (xMove < 0 && directionFacing)
        {
            FlipDirection(false);
        }
        else if (xMove > 0 && directionFacing == false)
        {
            FlipDirection(true);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Bumps player and deals damage
        if (collision.gameObject.tag == "Enemy")
        {
            hp -= 10;
            //Bumps it in the opposite direction of movement
            if (xMove > 0)
            {
                rb.AddRelativeForce(new Vector2(-15, 15) * jumpSpeed);
            }
            else
            {
                rb.AddRelativeForce(new Vector2(15, 15) * jumpSpeed);
            }
        }
        //Checks if grounded or "Walled"
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Walls" && isGrounded == false)
        {
            isWalled = true;
        }
    }
    //Resolves for the jumps
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "Walls")
        {
            isWalled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If hitting the finish it resets the level
        if(other.gameObject.tag == "Finish")
        {
            GameManager.instance.SetLevel(1);
            SceneManager.LoadScene("Play");
        }
        //If hit the boundary it implements die
        if (other.gameObject.tag == "Boundary")
        {
            Die();
        }
        //Adds the currency to the game manager based on the coin collected.
        if (other.gameObject.tag == "Currency")
        {
            if (other.gameObject.name == "currency50(Clone)")
            {
                GameManager.instance.SetCurrency(50);

            }
            else if (other.gameObject.name == "currency100(Clone)")
            {
                GameManager.instance.SetCurrency(100);

            }
            else if (other.gameObject.name == "currency200(Clone)")
            {
                GameManager.instance.SetCurrency(200);

            }
            else if (other.gameObject.name == "currency500(Clone)" || other.gameObject.name == "currency500 (1)" || other.gameObject.name == "currency500")
            {
                GameManager.instance.SetCurrency(500);

            }
            //Destroys the currency
            Destroy(other.gameObject);
        }
    }
    //Flips the characters direction
    void FlipDirection(bool newWay)
    {
        //Gets the local x 
        float flip = transform.localScale.x;
        //Changes it 
        flip *= -1;
        //Fixes the scale of the character
        transform.localScale = new Vector3(flip, transform.localScale.y, 0);
        directionFacing = newWay;
    }
    //Takes them to the death screens
    void Die()
    {
        SceneManager.LoadScene("Death");
    }
}
