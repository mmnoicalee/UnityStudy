using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CRankInfoPanel : MonoBehaviour {

    public Text _nickNameText;
    public Text _bestclickCountText;

    public void SetRankInfo(string nickName, string bestclickCount)
    {
        _nickNameText.text = nickName;
        _bestclickCountText.text = bestclickCount;
    }
}
