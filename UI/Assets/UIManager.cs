using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject option;


    public void PressSettings()
    {
        option.SetActive(true);
    }

    public void Return()
    {
        option.SetActive(false);
    }

    public void PressStart()
    {
        SceneManager.LoadScene(1);
    }

    public void PressBack()
    {
        SceneManager.LoadScene(0);
    }
}
