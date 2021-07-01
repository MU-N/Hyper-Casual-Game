using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMnager : MonoBehaviour
{
    [SerializeField] GameObject[] menus;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void CallCancelButton()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }


}
