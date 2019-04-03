using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPanel : MonoBehaviour
{
    public GameObject loginPanel;

    public void OnRegisterButton()
    {

    }

    public void OnBackButton()
    {
        loginPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
