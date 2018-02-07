using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CUfo : MonoBehaviour {

    public Text _playerNameText;

    public Sprite[] _sprites;

	void Start () {
        _playerNameText.text = PlayerPrefs.GetString("USER_ID", "Player");

        string type = PlayerPrefs.GetString("TYPE", "0");

        GetComponent<SpriteRenderer>().sprite =
            _sprites[int.Parse(type)];
	}
	
	void Update () {
		
	}
}
