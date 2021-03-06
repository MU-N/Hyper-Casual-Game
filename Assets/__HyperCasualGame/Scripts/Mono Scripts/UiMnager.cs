
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiMnager : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] GameContrllerData GCD;
    int levelCount;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            CallMenus(0);
        levelCount = SceneManager.sceneCountInBuildSettings;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GCD.RestData();
        }
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            CallMenus(2);
        }
        scoreUI.text = GCD.Score.ToString();
        
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
        //FindObjectOfType<AudioManager>().Play("Lose");
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
        SceneManager.LoadScene(1);
    }
    public void CallNextLevel()
    {
        int nextLevel = (SceneManager.GetActiveScene().buildIndex + 1) % levelCount;
        
        SceneManager.LoadScene(nextLevel == 0 ? 1 :nextLevel );
    }

}
