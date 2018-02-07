using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using UnityEngine.SceneManagement;

public class CAccountManager : MonoBehaviour {

    public InputField _nicNameInputField;
    public Text _msgText;

	// Use this for initialization
	void Start () {
		
	}

    public void OnNickNameEditEnterEvent()
    {
        if (string.IsNullOrEmpty(_nicNameInputField.text.Trim()))
        {
            _msgText.text = "You have not entered a nickname";
            return;
        }
        StartCoroutine("LoadAccountInfoCoroutine");
    }

    IEnumerator LoadAccountInfoCoroutine()
    {
        string baseUrl = "http://127.0.0.1/clickergame/login.php";

        //요청 url 작성
        string url = baseUrl + "?nick_name=" + _nicNameInputField.text.Trim();

        //통신요청
        WWW www = new WWW(url);

        //응답 대기
        yield return www;
        
        // 통신 오류가 발생하지 않았다면
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Response Data : " + www.text);

            //JSON -> 딕셔너리로
            Dictionary<string, object> responseData = MiniJSON.jsonDecode(www.text) as Dictionary<string, object>;

            string result = responseData["RESULT"].ToString();

            if (result.Equals("LOAD_SUCCESS"))
            {
                Dictionary<string, object> userData = responseData["USER_INFO"] as Dictionary<string, object>;
 
                // 유저 정보 딕셔너리 -> JSON으로 전환
                string saveData = MiniJSON.jsonEncode(userData);

                // PlayerPrefs에 유저정보를 저장함
                PlayerPrefs.SetString("USER_INFO", saveData);
                PlayerPrefs.Save();

                //게임신으로 이동함
                SceneManager.LoadScene("Game");
            }
            else
            {
                _msgText.text = "User infomation load fail.";
            }
        }
        else
        {
            _msgText.text = "Server connect fail.";
        }
    }
}
