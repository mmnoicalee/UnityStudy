using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using UnityEngine.SceneManagement;

public class CRankManager : MonoBehaviour {

    public GameObject _rankInfoPenelPrefab;

    public Transform _contentViewTr;

	// Use this for initialization
	void Start () {
        StartCoroutine("LoadRankCoroutine");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadRankCoroutine()
    {
        string url = "http://127.0.0.1/clickergame/selectorderby.php";

        WWW www = new WWW(url);

        yield return www;

        if(string.IsNullOrEmpty(www.error))
        {
            Dictionary<string, object> rankData = MiniJSON.jsonDecode(www.text) as Dictionary<string, object>;

            string result = rankData["RESULT"].ToString();

            // 순위 조회 데이타 성공했다면
            if (result.Equals("ORDERBY_SUCCESS"))
            {
                // 순위 리스트를 참조함
                List<object> rankList = rankData["DATA_LIST"] as List<object>;

                float posY = 0; //행 생성 위치 y

                //리스트
                for (int i = 0; i<rankList.Count; i++)
                {
                    Dictionary<string, object> rankRowData = rankList[i] as Dictionary<string, object>;

                    Debug.Log((i+1).ToString() + "등 유저 아이디 : " + rankRowData["nick_name"].ToString());

                    //랭크 정보를 표시할 행 오브젝트 생성
                   GameObject row = Instantiate(_rankInfoPenelPrefab, Vector2.zero, Quaternion.identity, _contentViewTr);

                    row.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, posY);

                    //랭크 정보를 행패널에 설정함
                    CRankInfoPanel rankInfo = row.GetComponent<CRankInfoPanel>();
                    rankInfo.SetRankInfo(rankRowData["nick_name"].ToString(), rankRowData["best_click_count"].ToString());

                    posY -= 100f;
                }

                // Content 뷰의 크기를 행 갯수에 맞게 키워줌

                RectTransform rt = _contentViewTr.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Abs(posY));
            }

            else
            {
                Debug.Log("통신오류 발생");
            }
        }
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("AccountScene");
    }
}

