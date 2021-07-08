
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMnager : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
     int levelCount;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex==0)
        CallMenus(0);
        levelCount = SceneManager.sceneCountInBuildSettings;
        
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

    }
    public void CallCancelAllButton()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }

    }

    public void CallCancelButton(int index)
    {
        CallMenus(index);

    }

    public void PlayButton()
    {
        CallplayLevel();
        CallCancelAllButton();
    }

    public void NextButton()
    {
        CallNextLevel();
        CallCancelAllButton();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CallCancelAllButton();
    }

    public void CallWinMenu()
    {
        CallMenus(5);
    }
    public void CallLoseMenu()
    {
        CallMenus(6);
    }
    public void BackToAnotherMenuButton(int index)
    {
        CallMenus(index);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void CallplayLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1 % levelCount) );
    }
    public  void CallNextLevel()
    {
        SceneManager.LoadScene(((SceneManager.GetActiveScene().buildIndex + 1) % levelCount) + 1);
    }
    
}
