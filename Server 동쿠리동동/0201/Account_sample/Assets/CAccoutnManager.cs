using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using UnityEngine.SceneManagement;

public class CAccoutnManager : MonoBehaviour {

    public InputField _idInputField;
    public InputField _pwInputField;
    public InputField _msgText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //입력필드에 포커스활성화가 되었다면 텍스트 지움
		if(_idInputField.isFocused || _pwInputField.isFocused)
        {
            _msgText.text = "";

        }
	}
    public void OnLoginButtonclick()
    {
        StartCoroutine("LoginNetCoroutine");
    }

    IEnumerator LoginNetCoroutine()
    {
        string url = "http://127.0.0.1/account.text.php";

        // form-data 데이터 객체 생성
        WWWForm form = new WWWForm();
        // POST 파라미터 설정
        form.AddField("user_id", _idInputField.text.Trim());
        form.AddField("user_pw", _pwInputField.text.Trim()); // Post key

        //POST메소드 방식의 Request를 수행
        WWW www = new WWW(url, form);

        yield return www;

        if (!string.IsNullOrEmpty(www.error)) //오류가 났으면
        {
            Debug.Log("서버 통신 오류 발생");
            _msgText.text = "서버 통신 오류 발생";
            yield break;
        }

        //응답받은 Json > Dictionary 전환
        Dictionary<string, object> responseData
            = MiniJSON.jsonDecode(www.text.Trim())
            as Dictionary<string, object>;

        //처리 결과 판단
        string result = responseData["result"].ToString();

        if(result.Equals("LOGIN_FAIL"))
        {
            _msgText.text = responseData["msg"].ToString();
            yield break;
        }

        Dictionary<string, object> userInfo
            = responseData["user_Info"] as Dictionary<string, object>;

        //유저의 정보를 PlayerPrefs에 저장함
        PlayerPrefs.SetString("User_ID", userInfo["id"].ToString());
        PlayerPrefs.SetString("TYPE", userInfo["type"].ToString());
        PlayerPrefs.SetString("SCORE", userInfo["score"].ToString());
        PlayerPrefs.Save();

        SceneManager.LoadScene("Game");
    }
}






