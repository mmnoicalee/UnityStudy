using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData; //minijason 사용

public class ChttpNetBasic : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetText();
    }

    void GetText() // Get 통신을 요청함
    {
        // 1. 유니티에서 http통신을 수행하는 처리는 반드시 코루틴을 이용해야함
        // (통신지연병목해결)
        StartCoroutine("HttpGetTestCoroutine");
    }

    IEnumerator HttpGetTestCoroutine()
    {
        // 2. 통신 url 작성
        string url = "http://127.0.0.1/get_sum.php";

        // 3. get 파라미터 설정
        string getParamUrl = url + "?val1 = 10 & val2 = 40";

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
        
        // JSON 문자열 -> C#의 딕셔너리(사전) 객체로 전환
        Dictionary<string, object> responseData =
            (Dictionary<string, object>)MiniJSON.jsonDecode(www.text.Trim());

        Debug.Log("val1 => " + responseData["val1"]);
        Debug.Log("val2 => " + responseData["val2"]);
        Debug.Log("data => " + responseData["data"]);
    }
}
