using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyBehavior : MonoBehaviour {
    private int currencyAmount;

    private float movementDirection;
    // Use this for initialization
    void Start()
    {
        currencyAmount = 0;
        movementDirection = 0.1f;
        StartCoroutine("MoveCurrency");
    }
    public int GetCurrencyAmount()
    {
        return currencyAmount;
    }
    public void SetCurrencyAmount(int a)
    {
         currencyAmount = a;
    }
    IEnumerator MoveCurrency()
    { //coroutine
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(new Vector2(0, movementDirection));
            yield return new WaitForSeconds(.1f);
            if (i == 4)
            { //resets
                i = 0;
                movementDirection *= -1;
            }
        }
    }
}
