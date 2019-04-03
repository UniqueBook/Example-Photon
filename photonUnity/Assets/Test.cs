using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //点击鼠标左键时触发
        if (Input.GetMouseButtonDown(0))
        {
            SendRequest();
        }
    }

    //发送请求到服务器   OpCustom
    void SendRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, 45454);
        data.Add(2, "45645454sdsadsfd你哈斯");
        PhotonEngine.Peer.OpCustom(1, data, true);
    }
}
