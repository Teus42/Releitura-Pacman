using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /******Todos os PowerUp ficam na classse PlayerMovement******/  
    
    private GameObject _dots;    
    Vector3 spawnDots = new Vector3();
    private float spawnRange = 0.3f;
    public Text pontos;
    public Text highscore;
    private int hs;
    private int chkScore; 

    void Start()
    {
        hs = PlayerPrefs.GetInt("highscore");
        highscore.text = hs.ToString();
        _dots = (GameObject)Resources.Load("dots",typeof(GameObject));
       
    }
    void Update()
    {
        //Atualizar os txts             
        pontos.text = PlayerMovement._pontuacao.ToString();
        highscore.text = hs.ToString();

        //Numero de dots para encerrar o jogo
        /*
        if(PlayerMovement._pontos == 15)
        {
           gg.SetActive(true);
           Time.timeScale = 0;
           PlayerMovement._vivo = false;
        }
        */    

        //Checando se o score é maior que o highscore atual           
        chkScore = PlayerMovement._pontuacao;
        if(chkScore >= hs)
        {
            PlayerPrefs.SetInt("highscore", chkScore);
            highscore.text = PlayerMovement._pontuacao.ToString();
        }        

        if(Input.GetKeyDown(KeyCode.R))
        {
            Continuar();           
        }           
        
    }  

    void FixedUpdate()
    {
        PosSpawn();
        SpawnDots();
    }

    public void Continuar()
    {
        Time.timeScale = 1;       
        SceneManager.LoadScene("Game");
    }
    public void Menu()
    {
        Time.timeScale = 1;        
        Destroy (GameObject.Find("AudioManager"));
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();                
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    void PosSpawn()
    {
        float x = Random.Range(-10.59f,11.27f);
        float z = Random.Range(-10.06f,10.21f);
        spawnDots = new Vector3(x,1.2f,z); 
    }
    void SpawnDots()
    {       

        bool spawnHere = false;     
        
        GameObject[] points;
        points = GameObject.FindGameObjectsWithTag("Points");        
        spawnHere = spawnPosIsLegal(spawnDots,spawnRange);
        
        if(spawnHere)
        {
            if(points.Length <= 100)
            { 
                GameObject newDot = Instantiate(_dots,spawnDots,Quaternion.identity) as GameObject;
            }
        }       
                   
    }

    private bool spawnPosIsLegal(Vector3 pos, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, radius);

        foreach(Collider collider in colliders)
        {
            GameObject go = collider.gameObject;

            if (go.transform.CompareTag("Player"))
            {
                return false;
            }
            if (go.transform.CompareTag("Map"))
            {
                return false;
            }
            if (go.transform.CompareTag("Wall"))
            {
                return false;
            }
            if (go.transform.CompareTag("Points"))
            {
                return false;
            }
        }

        return true;        
    }   

    /*public GameObject gizmosSphere;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gizmosSphere.transform.position,spawnRange);
    }*/
  
   
}
