using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMnager : MonoBehaviour
{
    [SerializeField] GameObject[] menus;

    // Start is called before the first frame update
    void Start()
    {

        CallMenus(0);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKey(KeyCode.Escape))
            {
                CallMenus(2);

               
            }

        
    }
    public void CallMenus(int index)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == index)
                menus[i].SetActive(true);
            else
                menus[i].SetActive(false);
        }

        Time.timeScale = 0;
    }
    public void CallCancelButtonWithTimeScale()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }

        Time.timeScale = 1;
    }

    public void CallCancelButton(int index )
    {
        CallMenus(index);

    }

    public void PlayButton()
    {
        //scenemanger
        CallCancelButtonWithTimeScale();
    }

    public void NextButton()
    {
        //scenemanger.load biuld scen index +1
        CallCancelButtonWithTimeScale();
    }
    public void RestartButton()
    {
        //scenemanger.loadactive scene
        CallCancelButtonWithTimeScale();
    }

    public void BackToAnotherMenuButton(int index)
    {
        //scenemanger
        CallMenus(index);
    }

    public void ExitButton()
    {
        //scenemanger
        Application.Quit();
    }


}
