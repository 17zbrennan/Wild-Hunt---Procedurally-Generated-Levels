using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private int levelNumber;
    private int currency;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);// kills the current instance so we can't have 2 instances

        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        levelNumber = 1;
        currency = 0;
	}
  /* Getters and Setters for current levels and score.
   */
    public void SetCurrency(int a)
    {
        currency += a;
    }
    public int GetCurrency()
    {
        return currency;
    }
    public void SetLevel(int a)
    {
        levelNumber += a;
    }
    public int GetLevel()
    {
        return levelNumber;
    }
    public void Reset()
    {
        currency = 0;
        levelNumber = 1;
    }
}
