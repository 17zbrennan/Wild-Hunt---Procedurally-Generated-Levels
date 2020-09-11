using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private bool followState = false;
    float speed;
    private string state;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        state = "Patrol";
        speed = 5;
        StateMachine(state);
    }

    // Update is called once per frame
    void Update()
    {
        //Following State for enemy
        if (followState == true)
        {
            //follows the player if its in the range
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, .02f);
            //Changes the state
            if (Vector3.Distance(this.transform.position, player.transform.position) >= 10.0f)
            {
                followState = false;
                state = "Patrol";
                StateMachine(state);
            }
        }
    }

    /* Patrol and follow states
     */
    void StateMachine(string state)
    {
        switch (state)
        {
            case "Patrol":
                StartCoroutine(Patrol());
                break;
            case "Follow":
                StopCoroutine(Patrol());
                followState = true;
                break;

        }
    }
    IEnumerator Patrol()
    {
        //For loop to make the enemy go back and forth until the player is in range.
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (Vector3.Distance(transform.position, player.transform.position) <= 10.0f)
            {
                state = "Follow";
                StateMachine(state);
            }
            if (i == 9)
            {
                speed *= -1;
                i = 0;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    //Decides if the enemy dies.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Sword")
        {
            Destroy(this.gameObject);
        }
    }
}
