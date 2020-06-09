using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject boots;

    private Animator _anim;
    //Movementação
    private Rigidbody rb;
    private Vector3 move;          
    private float speed = 8.0f;  //4 normal
    private float _speedNormal;  

    //Vivo    
    public static bool _vivo;  
    
    //Power Up de Comer ou BigPill
    private bool bigPillOn = false;
    private float bigPillTime = 10.0f;
    private float _bpt;

    //Power Up de Correr
    private bool bootsOn = false;
    private float bootsTime = 5.0f;
    private float _bt;
    
    //Tempo de spawn das botas
    private GameObject chkBoots;
    private float timeSpawnBoots = 8.0f;
    private float _tsb;

    //Ghost voltar a ativa
    private bool ghostOff;
    private float ghostTime = 15.0f;
    private float _gt;

    //Pontuação
    private int point;
    public static int _pontos;
    public static int _pontuacao;

    //Tela de GameOver
    public GameObject gameOver;

    //Txt de tempo na hud
    public Text tempoBigPill;
    public Text tempoBoots;
    public Text tempoGhostMedo;

    void Start()
    {
        _anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>(); 
        player = this.gameObject;   
        _vivo = true;              
        _speedNormal = speed;
        _bt = bootsTime;
        _gt = ghostTime;
        _bpt = bigPillTime;
        _tsb = timeSpawnBoots;
        boots = (GameObject)Resources.Load("runBoots", typeof (GameObject));
    }

    void Update()
    {
        
        move = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y,Input.GetAxis("Vertical"));   
        _pontos = point;  

        GameObject[] fantasmas;
        fantasmas = GameObject.FindGameObjectsWithTag("Enemy");         

        if(bigPillOn)
        {            
            bigPillTime -= 1 * Time.deltaTime;            
        }
        if(bigPillTime < 0f)
        {            
            for (int i = 0; i < fantasmas.Length; i++)
            {
                fantasmas[i].GetComponent<EnemyIA>().SemMedo();   
            }
            bigPillOn = false;
            bigPillTime = _bpt;
        }        

        if(bootsOn)
        {
            speed = 8f;
            bootsTime -= 1 * Time.deltaTime;                             
        }
        if(bootsTime < 0f)
        {           
            bootsOn = false;
            bootsTime = _bt;
            speed = _speedNormal;
        }

        if(ghostOff){ghostTime -= 1 * Time.deltaTime;}      
        if(ghostTime < 0f)
        {            
            for (int i = 0; i < fantasmas.Length; i++)
            {
                fantasmas[i].GetComponent<EnemyIA>()._chkGhostVivo = true;   
            }
            ghostOff = false;
            ghostTime = _gt;
        }       

        Tempos();
        SpawnBotas();
    }

    private void Tempos()
    {
        tempoBigPill.text = string.Format("{0:0}", bigPillTime); 
        tempoBoots.text = string.Format("{0:0}", bootsTime); 
        tempoGhostMedo.text = string.Format("{0:0}", ghostTime); 
    }
    void FixedUpdate()
    {
        if(_vivo)
        {
            rotation();
            movement(move);  
        }   

        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            _anim.SetBool("isWalking", true);
        }else if(Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
             _anim.SetBool("isWalking", true);
        }else
        {
            _anim.SetBool("isWalking", false);
        }   
    }

    private void movement(Vector3 dir)
    {
        rb.MovePosition(transform.position + (dir * speed * Time.deltaTime));
             
    }   
    private void rotation()
    {          
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0f ,-90f,0f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0f ,90f, 0f);
        }
        
        if (Input.GetAxisRaw ("Vertical") > 0) 
        {
            transform.rotation = Quaternion.Euler(0f ,-180f, 0f);
        } 
        else if (Input.GetAxisRaw ("Vertical") < 0)
        {
            transform.rotation = Quaternion.Euler(0f ,0f, 0f);
        }
         

        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.rotation = Quaternion.Euler(0f ,-120f, 0f);
        } 
        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.rotation = Quaternion.Euler(0f ,-45f, 0f);
        } 
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.rotation = Quaternion.Euler(0f ,120f, 0f);
        } 
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.rotation = Quaternion.Euler(0f ,45f, 0f);
        } 
        
        
        //MEME player.transform.Rotate(-89.885f,Input.GetAxis("Horizontal")*60*Time.deltaTime,0);  
        
             
    }
    void OnCollisionEnter(Collision coll)
    {       
        GameObject[] fantasmas;
        fantasmas = GameObject.FindGameObjectsWithTag("Enemy");    

        if(coll.gameObject.tag == "Points" && coll.collider.gameObject.layer == LayerMask.NameToLayer("Points"))
        {           
            point++;
            _pontuacao += 100;         
            Destroy(coll.gameObject);           
        }
        if(coll.gameObject.tag == "Collect" && coll.collider.gameObject.layer == LayerMask.NameToLayer("Boots"))
        {          
            _pontuacao += 200;            
            bootsOn = true;         
            Destroy(coll.gameObject);            
        }

        if(coll.gameObject.tag == "Collect" && coll.collider.gameObject.layer == LayerMask.NameToLayer("BigPill"))
        {      
            for (int i = 0; i < fantasmas.Length; i++)
            {
                fantasmas[i].GetComponent<EnemyIA>().Medinho();   
            }
            _pontuacao += 500; 
            bigPillTime = +_bpt; 
            bigPillOn = true;        
                     
            Destroy(coll.gameObject);                            
        }
        
        if(coll.gameObject.tag == "Enemy")
        {   
            if(!bigPillOn)
            {      
                _vivo = false; 
                Destroy(this.gameObject);
                gameOver.SetActive(true);

            }else
            {         
                _pontuacao += 1000;       
                ghostOff = true;
                coll.gameObject.GetComponent<EnemyIA>()._chkGhostVivo = false;                                                 
            }
                       
        }
    }    
    private void SpawnBotas()
    {
        Vector3[] a = new Vector3[4];
        a [0] = new Vector3(-8.78f,1.44f,-7.33f);//C
        a [1]= new Vector3(9.44f,1.44f,5.56f);//C
        a [2]= new Vector3(-9.001f,1.44f,7.602f);//C
        a [3]= new Vector3(6.02f,1.44f,-7.262f);//C

        int bootsPos;
        bootsPos = Random.Range(0,a.Length);

        bool _bootsSpawnOn = false;
        
        if(GameObject.Find("runBoots(Clone)"))
        {          
           _bootsSpawnOn = false;
        }else
        {
            if(!_bootsSpawnOn)
            {
                timeSpawnBoots -= 1 * Time.deltaTime;
                if(timeSpawnBoots <0f)
                {
                    chkBoots = Instantiate(boots,a[bootsPos],Quaternion.identity);
                    timeSpawnBoots = _tsb;
                    _bootsSpawnOn = true;                                
                }
            }            
        } 
    } 
}
