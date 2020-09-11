using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCurrency : MonoBehaviour {

    public GameObject currency50;
    public GameObject currency100;
    public GameObject currency200;
    public GameObject currency500;
   

    private int randInt;
    // Use this for initialization
    void Start () {
        //puts them in a game object
        GameObject[] currencyG0 = { currency50, currency100, currency200, currency500 };
        randInt = Random.Range(0, 100);
        //60% chance to get worst currency
        if(randInt < 61)
        {
            GameObject temp = currencyG0[0];
            Instantiate(temp, this.transform.position, Quaternion.identity);
            temp.GetComponent<CurrencyBehavior>().SetCurrencyAmount(50);

            Destroy(this.gameObject);
        }
        //20% chance to get 100 currency
        else if (randInt < 81)
        {
            GameObject temp = currencyG0[1];
            Instantiate(temp, this.transform.position, Quaternion.identity);
            temp.GetComponent<CurrencyBehavior>().SetCurrencyAmount(100);

            Destroy(this.gameObject);
        }
        //150% chance to get 200 currency
        else if (randInt < 96)
        {
            GameObject temp = currencyG0[2];
            Instantiate(temp, this.transform.position, Quaternion.identity);
            temp.GetComponent<CurrencyBehavior>().SetCurrencyAmount(200);

            Destroy(this.gameObject);
        }
        //5% chance to get best currency
        else
        {
            GameObject temp = currencyG0[3];
            Instantiate(temp, this.transform.position, Quaternion.identity);
            temp.GetComponent<CurrencyBehavior>().SetCurrencyAmount(500);

            Destroy(this.gameObject);
        }
    }


}
