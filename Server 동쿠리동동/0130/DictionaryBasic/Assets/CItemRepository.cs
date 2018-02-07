using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    // 아이템 이름
    public string itemName;

    // 아이템 특성들
    public int str; // 힘

    public int dex; // 민첩
    public int con; // 체력
    public List<int> dropStageNums;

    public Item(string itemName)
    {
        this.itemName = itemName;

        // 랜덤하게 아이템 특성을 부여함 (10 ~ 100)
        this.str = Random.Range(10, 100);
        this.dex = Random.Range(10, 100);
        this.con = Random.Range(10, 100);

        // 아이템 드랍 스테이지 번호 배열
        dropStageNums = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            dropStageNums.Add(Random.Range(1, 41));
        }
    }
}

public class CItemRepository : MonoBehaviour
{
    private Dictionary<string, Item> _itemDic = new Dictionary<string, Item>();   // 아이템 사전

    // 아이템 이름 입력 필드 참조
    public InputField _ipItemName;

    // 진행 안내 메시지 출력
    public Text _msgText;

    public Text _itemNameText;  // 아이템 정보 출력
    public Text _strText;   // 힘 정보 출력
    public Text _dexText;   // 민첩 정보 출력
    public Text _conText;   // 체력 정보 출력
    public Text _dropStageNumText;   // 드랍 스테이지 번호 배열 출력

    private Item _item;

    public void OnItemAddButtonClick()
    {
        // 아이템 이름을 입력 필드에서 받아옴
        string itemName = _ipItemName.text.Trim();

        // 아이템 생성
        Item item = new Item(itemName);

        // Dictionary.Add("키", 값)
        // -> 지정한 키로 찾을 수 있는 값을 추가함

        // 딕셔너리에 생성한 아이템을 추가
        _itemDic.Add(itemName, item);

        _ipItemName.text = "";

        StartCoroutine("ToastMessageCoroutine", "아이템이 1개 추가됨");
    }

    private IEnumerator ToastMessageCoroutine(string msg)
    {
        _msgText.text = msg;    // 메시지 출력하기

        yield return new WaitForSeconds(1f);    // 1초 출력

        _msgText.text = ""; // 메시지 자동 지우기
    }

    public void OnItemSearchButtonClick()
    {
        // 아이템 이름을 입력 필드에서 받아옴
        string itemName = _ipItemName.text.Trim();

        // bool 키를가진값이있는지여부 = Dictionary.ContainsKey("키")
        // -> 지정한 키를 가진 값이 있는지를 검색함
        if (!_itemDic.ContainsKey(itemName))
        {
            StartCoroutine("ToastMessageCoroutine", "아이템이 존재하지 않음");
            return;
        }

        // 값타입 값 = Dictionary["키"]
        _item = _itemDic[itemName];

        StartCoroutine("ToastMessageCoroutine", "아이템 정보를 출력함");

        // 검색된 정보를 Text로 출력함
        _itemNameText.text = _item.itemName;
        _strText.text = "힘 : " + _item.str.ToString();
        _dexText.text = "민첩 : " + _item.dex.ToString();
        _conText.text = "체력 : " + _item.con.ToString();

        List<int> dNums = _item.dropStageNums;
        _dropStageNumText.text = "드랍 위치 번호 : ";
        _dropStageNumText.text += dNums[0].ToString() + ", ";
        _dropStageNumText.text += dNums[1].ToString() + ", ";
        _dropStageNumText.text += dNums[2].ToString();
    }

    public void OnItemDeleteButtonClick()
    {
        if (_itemNameText.text.Length <= 0)
        {
            StartCoroutine("ToastMessageCoroutine", "삭제할 아이템이 없음");
            return;
        }

        // bool 키를가진값이있는지여부 = Dictionary.ContainsKey("키")
        // -> 지정한 키를 가진 값이 있는지를 검색함
        if (!_itemDic.ContainsKey(_ipItemName.text.Trim()))
        {
            StartCoroutine("ToastMessageCoroutine", "삭제할 아이템이 없음");
            return;
        }

        // bool 삭제완료여부 = Dictionary.Remove("키")
        // -> 지정한 키를 가진 요소를 삭제함
        if (!_itemDic.Remove(_ipItemName.text.Trim()))
        {
            StartCoroutine("ToastMessageCoroutine", "아이템 삭제를 실패함");
            return;
        }

        // 삭제되었으므로 출력 영역을 초기화함
        _itemNameText.text = "";
        _strText.text = "";
        _dexText.text = "";
        _conText.text = "";

        StartCoroutine("ToastMessageCoroutine", "아이템 삭제를 완료함");
    }

    public void OnItemDicToJsonString()
    {
        // 1. 객체 -> JSON 전환 (인코딩)
        // string JSON문자열 = JsonUtility.ToJson(객체참조변수);
        string jsonData = JsonUtility.ToJson(_item, true); //(콘솔창 모양옵션)
        Debug.Log(jsonData);

        // 2. JSON -> 객체 전환 (디코딩)
        // 변환타입 변수 = JsonUtility.FromJson<변환타입>(JSON문자열);
        Item item = JsonUtility.FromJson<Item>(jsonData);
        Debug.Log("아이템 이름 : " + item.itemName);
        Debug.Log("아이템 힘 : " + item.str);
        Debug.Log("아이템 민첩 : " + item.dex);
        Debug.Log("아이템 체력 : " + item.con);
        Debug.Log("아이템 드랍 위치 : " + item.dropStageNums[0] + ", " 
            + item.dropStageNums[1] + ", " + item.dropStageNums[2]);
    }
}