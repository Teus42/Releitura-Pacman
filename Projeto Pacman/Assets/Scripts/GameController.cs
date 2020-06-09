using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /******Todos os PowerUp ficam na classse PlayerMovement******/  
    
    public GameObject gg;
    private GameObject _dots;

    public Collider[] colliders;
    
    [SerializeField]
    private float sphereRadius = 20f;

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
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();                
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    void SpawnDots()
    {
        Vector3 spawnDots = new Vector3();
        bool spawnHere = false;     
        
        GameObject[] points;
        points = GameObject.FindGameObjectsWithTag("Points");  
        //GameObject[] walls;
        //walls = GameObject.FindGameObjectsWithTag("Wall");  
        spawnHere = PreventSpawn(spawnDots);
        if(!spawnHere)
        {
            if(points.Length <= 50)
            {   
                float x = Random.Range(-9.26141f,10.77f);
                float z = Random.Range(-10.86f,10.75f);

                spawnDots = new Vector3(x,1.2f,z);                
                
                GameObject newDot = Instantiate(_dots,spawnDots,Quaternion.identity) as GameObject;                                    
                    
                Debug.Log("Points: "+points.Length);                 
            }
        }       
                   
    }

    
    bool PreventSpawn(Vector3 spawnPos)
    {
        colliders = Physics.OverlapSphere(transform.position,sphereRadius);  

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;
            float z = colliders[i].bounds.extents.z;

            float leftExtent = centerPoint.x - width*2;
            float rightExtent = centerPoint.x + width*2;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;
            float zExtent = centerPoint.z - z*2;
            float z2Extent = centerPoint.z + z*2;

            if(spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
            {
                if(spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                {
                    if(spawnPos.z >= zExtent && spawnPos.z <= z2Extent)
                    {                    
                        return false;
                    }
                }
            }
        }  

        return true;
    }
    
}
