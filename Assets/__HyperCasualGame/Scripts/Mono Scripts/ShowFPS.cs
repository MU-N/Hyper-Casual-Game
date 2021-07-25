using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    [SerializeField] TMP_Text fpsText;
    [SerializeField] float deltaTime;
    private void Start()
    {
        gameObject.SetActive(false);
        fpsText.gameObject.SetActive(false);
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
#if UNITY_EDITOR
        gameObject.SetActive(true);
        fpsText.gameObject.SetActive(true);
#endif
    }
}
