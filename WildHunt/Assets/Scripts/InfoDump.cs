using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDump : MonoBehaviour {

    public Text score;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Changes the score and level for the death screen.
        score.text = "Score: " + GameManager.instance.GetCurrency() + " Level: " + GameManager.instance.GetLevel();
	}
}
