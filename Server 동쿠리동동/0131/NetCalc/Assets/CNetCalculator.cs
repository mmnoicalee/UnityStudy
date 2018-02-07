using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class CNetCalculator : MonoBehaviour {

    public InputField _value1InputField;
    public InputField _value2InputField;

    public Text _val1Text;
    public Text _signText;
    public Text _val2Text;
    public Text _equalText;
    public Text _resultText;

    
    public void OnCalcSetButtonClick(string clacType)
    {
        string val1 = _value1InputField.text;
        string val2 = _value2InputField.text;

        StartCoroutine(CalcNetCoroutine(val1, val2, clacType));
    }

    IEnumerator CalcNetCoroutine(string val1, string val2, string clacType)
    {
        // 2. 통신 url 작성
        string url = "http://127.0.0.1/calcu.php";

        // 3. get 파라미터 설정
        string getParamUrl = url + "?val1 ="+val1+"&val2 ="+val2+
            "&calc_name="+clacType;

        // 4. HTTP 통신객체 생성(www) 및 통신 요청
        WWW www = new WWW(getParamUrl);

        yield return www; // 5. 통신 응답 대기

        // 6. 통신 에러 체크
        // 만약 통신 에러가 발생했다면 (error 객체가 null 이 아님)
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error); //에러 메세지 출력
            yield break;
        }

        // 7. 응답 받은 데이타 출력
        Debug.Log(www.text);

        Dictionary<string, object> responseData = MiniJSON.jsonDecode(www.text.Trim())
            as Dictionary<string, object>; //딕셔너리로 다운케스팅

        Debug.Log("val1 => " + responseData["val1"]);
        Debug.Log("calc_type => " + responseData["calc_type"]);
        Debug.Log("val2 => " + responseData["val2"]);
        Debug.Log("result => " + responseData["result"]);

        _val1Text.text = responseData["val1"].ToString();
        _signText.text = responseData["calc_type"].ToString();
        _val2Text.text = responseData["val2"].ToString();
        _equalText.text = "=";
        _resultText.text = responseData["result"].ToString();

    }
}


