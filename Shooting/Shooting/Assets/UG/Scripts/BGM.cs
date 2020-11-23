using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private void Start()
    {
        BgmMgr.Instance.PlayBGM("bgm1");
    }

}
