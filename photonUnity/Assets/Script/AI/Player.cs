using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 标识 用于区分不同的 player，当前客户端的默认为true，
    /// 其他玩家的为flase
    /// </summary>
    public bool isLocalPlayer = true;

    private SyncPositionRequest syncPositionRequest;

    private Vector3 lastPosition = Vector3.zero;

    void Start()
    {
        if (isLocalPlayer)
        {
            //当前客户端玩家默认设置为绿色
            GetComponent<Renderer>().material.color = Color.green;
            syncPositionRequest = GetComponent<SyncPositionRequest>();
            //一秒同步5次
            InvokeRepeating("SyncPosition", 3, 0.2f);
        }

    }

    void SyncPosition()
    {
        if (Vector3.Distance(transform.position, lastPosition)>0.1f)
        {
            lastPosition = transform.position;
            syncPositionRequest.pos = transform.position;
            syncPositionRequest.DefaultRequest();
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            //得到上下左右的按键
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            //设置 时间间隔、 移动速度
            transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 4);
        }
    }
}
