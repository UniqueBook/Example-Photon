using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;

public class LoginPanel : MonoBehaviour
{
    public GameObject registerPanel;
    public InputField nameIF;//输入控件
    public InputField pwdIF;
    public Text hintMassage;

    private LoginRequest loginRequest;

    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
    }

    public void OnLoginButton()
    {
        hintMassage.text = "";          //登录时将提示信息清空
        loginRequest.Name = nameIF.text;//获取控件中的文字
        loginRequest.Pwd = pwdIF.text;
        loginRequest.DefaultRequest();//发起请求
    }

    public void OnRegisterButton()
    {
        gameObject.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            //成功则跳转到下一个场景
            SceneManager.LoadScene("Game");
        }
        else
        {
            hintMassage.text = "用户名或密码错误";
        }
    }
}
