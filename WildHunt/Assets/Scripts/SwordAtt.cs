using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAtt : MonoBehaviour
{

    public GameObject hitBox;
    public GameObject p;

    private Quaternion currentRotate;
    private bool coolDown = false;
    private void Start()
    {
        //Chances the hitbox to disabled 
        hitBox.GetComponent<BoxCollider2D>().enabled = false;
        currentRotate = this.transform.rotation;
    }
    void Update()
    {
        //Checks if you swing
        if (Input.GetMouseButtonDown(0) && coolDown == false)
        {
            StartCoroutine(SwingSword());

        }



    }

    IEnumerator SwingSword()
    {
        /* This changes the sword's rotation based on the player's direction. If it is done with it's 
         * swing it will activate the reset.
          */
        currentRotate = this.transform.rotation;

        hitBox.GetComponent<BoxCollider2D>().enabled = true;
        for (int i = 0; i < 10; i++)
        {
            if (p.GetComponent<PlayerControl>().xMove > 0)
            {
                this.transform.Rotate(-Vector3.forward, 10);

            }
            else
            {
                this.transform.Rotate(Vector3.forward, 10);

            }

            if (i == 9)
            {
                coolDown = true;

                StartCoroutine(SwordReset());
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator SwordReset()
    {
        /* Resets the sword back to its original state and disables the sword hitbox.
         */
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotate, Time.time * 5);
        coolDown = false;
        hitBox.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
    }

}
