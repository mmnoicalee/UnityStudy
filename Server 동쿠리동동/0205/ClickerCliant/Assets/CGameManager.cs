using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour {

    public Text _nickNameText;

    public Text _bestClickCountText;

    public Text _nowClickCountText;

    public Image _timerProgress;

    Dictionary<string, object> _userData;

	// Use this for initialization
	void Start () {
       string userInforString = PlayerPrefs.GetString("USER_INFO", "");
       Debug.Log(userInforString);

        // 유저 정보를 화면에 출력함
        _userData = MiniJSON.jsonDecode(userInforString) as Dictionary<string, object>;

        //닉네임출력
        _nickNameText.text = _userData["nick_name"].ToString();

        //최고 클릭 카운트 추가
        _bestClickCountText.text = _userData["best_click_count"].ToString();

        StartCoroutine("TimerCoroutine");

	}
	
    public void OnClickerButtonClick()
    {
        int clickCount = int.Parse(_nowClickCountText.text);
        int bestClickCount = int.Parse(_bestClickCountText.text);

        clickCount++;

        if(clickCount > bestClickCount)
        {
            _bestClickCountText.text = clickCount.ToString();
        }


        _nowClickCountText.text = clickCount.ToString();
    }

    //타이머 
    IEnumerator TimerCoroutine()
    {
        float progressValue = 1f / 100f;

        while (_timerProgress.fillAmount > 0f)
        {
            yield return new WaitForSeconds(0.1f);

            _timerProgress.fillAmount = _timerProgress.fillAmount - progressValue;
        }

        string url = "http://127.0.0.1/clickergame/update.php";

        WWWForm form = new WWWForm();
        form.AddField("nick_name", _userData["nick_name"].ToString());
        form.AddField("best_click_count", _bestClickCountText.text);

        //토탈 클릭 카운트값을 정수로 변환함
        int totalClickCount = int.Parse(_userData["total_click_count"].ToString());
        //현재 클릭 카운트 값을 종합 클릭 카운트에 누적함
        totalClickCount += int.Parse(_nowClickCountText.text);

        form.AddField("total_click_count", totalClickCount.ToString());
        
        // POST 방식으로 form 데이타를 넣어서 전송함
        WWW www = new WWW(url, form);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            SceneManager.LoadScene("Rank");
        }
        else
        {
            Debug.Log("Server connect fail.");
        }


    }
}
