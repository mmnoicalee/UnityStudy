using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using UnityEngine.UI;



public class CItemRepository : MonoBehaviour {



  
    private Dictionary<int>, 
    
    public int _value1;
    public int _value2;
    public int _value3;
    public int _value4;
    public int _value5;

    public void OnPlusButtonClick()
    {

    }
    public void OnMinusButtonClick()
    {

    }
    public void OngopButtonClick()
    {

    }
    public void OnhalfButtonClick()
    {

    }

    void GetText()

    { 
        StartCoroutine("HttpGetTestCoroutine");
    }

    IEnumerator HttpGetTestCoroutine()
    {
        string url = "http://127.0.0.1/calcu.php";
        string getParamUrl = url + "?val1 = 10 & val2 = 40";
        WWW www = new WWW(getParamUrl);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield break;
        }

        Debug.Log(www.text);

        Dictionary<string, object> responseData =
            (Dictionary<string, object>)MiniJSON.jsonDecode(www.text.Trim());

        Debug.Log("val1 =>" + responseData["val1"]);
        Debug.Log("val2 =>" + responseData["val2"]);
        Debug.Log("data =>" + responseData["data"]);

    }
}
