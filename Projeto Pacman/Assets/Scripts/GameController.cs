using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gg;
    public Text pontos;
    public Text highscore;
    private int hs;
    private int chkScore; 

    void Start()
    {
        hs = PlayerPrefs.GetInt("highscore");
        highscore.text = hs.ToString();
    }
    void Update()
    {             
        pontos.text = PlayerMovement._pontuacao.ToString();
        highscore.text = hs.ToString();

        if(PlayerMovement._pontos == 15)
        {
           gg.SetActive(true);
           Time.timeScale = 0;
           PlayerMovement._vivo = false;
        }
       
           
        chkScore = PlayerMovement._pontuacao;

        if(chkScore >= hs)
        {
            PlayerPrefs.SetInt("highscore", chkScore);
            highscore.text = PlayerMovement._pontuacao.ToString();
        }
       

        Debug.Log(hs);
        Debug.Log(chkScore);

        if(Input.GetKeyDown(KeyCode.R)){SceneManager.LoadScene("Game");}
    }
}
