using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData; //minijason
using System;

[Serializable]
class Weapon
{
    public string sword = "빛나는 검";
    public string gun = "따발 총";
    public string bow = "";
}

[Serializable]
class Monster
{
    public string name = "오우거";
    public int level = 5;
    public int hp = 100;
    public Weapon weapons = new Weapon();
}


public class CJasonText : MonoBehaviour {

    Monster monster = new Monster();

    private void Start()
    {
        string jsonData = JsonUtility.ToJson(monster,true);
        Debug.Log(jsonData);

        Dictionary<string, object> monsterDic
            = new Dictionary<string, object>();


        monsterDic["name"] = "오우거";
        monsterDic["level"] = "5";
        monsterDic["hp"] = "100";
        monsterDic["weapons"] = new Dictionary<string, object>();
    
        Dictionary<string, object> weapons =
        monsterDic["weapons"] as Dictionary<string, object>;
        weapons["sword"] = "빛나는 검";
        weapons["gun"] = "따발총";
        weapons["box"] = "";

        jsonData = MiniJSON.jsonEncode(monsterDic);
        Debug.Log(jsonData);

    }
}
