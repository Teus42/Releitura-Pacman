using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject sobre;
    public Text highscore;
    public void Iniciar()
    {
        SceneManager.LoadScene("Game");
    }

    void Update()
    {
        highscore.text = PlayerPrefs.GetInt("highscore").ToString();
        Debug.Log(PlayerPrefs.GetInt("highscore"));
    }
    public void Exit()
    {
        Application.Quit();                
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("highscore",0);
    }

    public void Sobre()
    {
        sobre.SetActive(true);
    }
    public void CloseSobre()
    {
        sobre.SetActive(false);
    }
}
