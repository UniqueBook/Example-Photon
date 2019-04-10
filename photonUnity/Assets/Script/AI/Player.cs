using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Tools;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 标识 用于区分不同的 player，当前客户端的默认为true，
    /// 其他玩家的为flase
    /// </summary>
    public bool isLocalPlayer = true;

    public string username;

    public GameObject playerPrefab;

    public GameObject player;

    private SyncPlayerRequest syncPlayerRequest;

    private SyncPositionRequest syncPositionRequest;

    private Vector3 lastPosition = Vector3.zero;

    /// <summary>
    /// 移动距离差
    /// </summary>
    private float moveOffset = 0.1f;

    /// <summary>
    /// 保存用户及对应的游戏物体
    /// </summary>
    private Dictionary<string, GameObject> playerDict = new Dictionary<string, GameObject>();

    void Start()
    {
        //当前客户端玩家默认设置为绿色
        player.GetComponent<Renderer>().material.color = Color.green;
        syncPositionRequest = GetComponent<SyncPositionRequest>();
        syncPlayerRequest = GetComponent<SyncPlayerRequest>();
        syncPlayerRequest.DefaultRequest();
        //一秒同步5次  调用 SyncPosition方法   time = 3 (3秒后执行) ，repeatRate = 0.2f  重复执行间隔
        InvokeRepeating("SyncPosition", 3, 0.2f);

    }



    void SyncPosition()
    {
        if (Vector3.Distance(player.transform.position, lastPosition) > moveOffset)
        {
            Debug.Log("SyncPosition---距离判断发送请求");
            lastPosition = player.transform.position;
            syncPositionRequest.pos = player.transform.position;
            syncPositionRequest.DefaultRequest();
        }
    }

    void Update()
    {
        //得到上下左右的按键
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //设置 时间间隔、 移动速度
        player.transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 4);
    }

    /// <summary>
    /// 获取其他客户端用户，并实例化其游戏物体
    /// </summary>
    /// <param name="usernameList"></param>
    public void OnSyncPlayerResponse(List<string> usernameList)
    {
        //创建其他客户端的player
        foreach (string username in usernameList)
        {
            OnNewPlayerEvent(username);
        }
    }

    /// <summary>
    /// 通过用户名创建游戏物体
    /// </summary>
    /// <param name="username"></param>
    public void OnNewPlayerEvent(string username)
    {
        //实例化游戏物体
        GameObject go = GameObject.Instantiate(playerPrefab);
        //移除多余SyncPlayerRequest、SyncPositionRequest脚本，并改变其他用户状态
        //DestroyImmediate(go.GetComponent<SyncPlayerRequest>());
        //DestroyImmediate(go.GetComponent<SyncPositionRequest>());
        //DestroyImmediate(go.GetComponent<NewPlayerEvent>());
        //DestroyImmediate(go.GetComponent<SyncPositionEvent>());
        //go.GetComponent<Player>().isLocalPlayer = false;
        //go.GetComponent<Player>().username = username;
        Debug.Log("新增游戏物体OnNewPlayerEvent：" + username);
        playerDict.Add(username, go);
    }

    /// <summary>
    /// 通过playerDatas(包含用户名与所在的位置)，找到对应的游戏物体并改变其位置
    /// </summary>
    /// <param name="playerDatas"></param>
    public void OnSyncPositionEvent(List<PlayerData> playerDatas)
    {
        foreach (PlayerData item in playerDatas)
        {
            Debug.Log("改变游戏物体位置--OnSyncPositionEvent：" + item.Username);
            //根据用户名获取对应的游戏物体
            GameObject go = DictTool.GetValue<string, GameObject>(playerDict, item.Username);
            if (go != null)
            {
                go.transform.position = new Vector3() { x = item.Pos.X, y = item.Pos.Y, z = item.Pos.Z };
            }
        }
    }
}
