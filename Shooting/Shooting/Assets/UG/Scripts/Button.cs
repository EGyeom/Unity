using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public void onButtonClicked()
    {
        SceneMgr.Instance.LoadScene("GameScene");
    }
}
