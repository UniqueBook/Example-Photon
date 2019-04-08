using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{
    public GameObject loginPanel;
    public InputField usernameIF;
    public InputField passwordIF;

    public Text hintMassage;
    private RegisterRequest registerRequest;

    void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
    }

    public void OnRegisterButton()
    {
        hintMassage.text = "";      //点击注册时先将提示清空
        registerRequest.username = usernameIF.text;
        registerRequest.password = passwordIF.text;
        registerRequest.DefaultRequest();
    }

    public void OnBackButton()
    {
        loginPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            //成功则提示消息，并跳转
            hintMassage.text = "注册成功";
        }
        else
        {
            hintMassage.text = "改用户名已存在";
        }
    }
}
