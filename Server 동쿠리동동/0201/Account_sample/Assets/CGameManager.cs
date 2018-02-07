using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGameManager : MonoBehaviour {

    public Text _scoreText;

	// Use this for initialization
	void Start () {

        _scoreText.text = "SCORE : " + PlayerPrefs.GetString("SCORE", "0");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
