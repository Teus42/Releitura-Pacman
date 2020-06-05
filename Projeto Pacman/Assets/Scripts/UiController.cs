using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public void Iniciar()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();                
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
