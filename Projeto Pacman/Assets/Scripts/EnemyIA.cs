using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{   
    //Perseguir e Posição Inicial
    private GameObject _player;
    private NavMeshAgent _agent;   
    private Vector3 _posInicial; 

    //Checando Player e Fantasma
    private bool _chkPlayerVivo;
    public bool _chkGhostVivo;

    //Variavel publica, interagi com o PlayerMovement
    //para quando pegar o PP(Renomear para bigPill)
    //fazer os fantasmas fugirem
    public bool _medo;

    //Materiais para mudar as cores dos fantasmas
    private Material _medoChange;
    private Material _morte;
    private Material _ghostColor;   
    public GameObject cheats;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        
        _medo = false;        
        _chkGhostVivo = true;

        _posInicial = this.gameObject.transform.position;
        _ghostColor = this.gameObject.GetComponent<MeshRenderer>().material;

        _medoChange = (Material)Resources.Load("Medo", typeof(Material));
        _morte = (Material)Resources.Load("Dead", typeof(Material)); 
    }
    void Update()
    {
        
        if(GameObject.FindGameObjectWithTag("Player")){_chkPlayerVivo = true;}else{_chkPlayerVivo = false;}
        
        if(_chkGhostVivo)
        {
            this.gameObject.GetComponent<MeshCollider>().isTrigger = false;  
            Perseguir();
        }else
        {
            this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            this.gameObject.GetComponent<MeshRenderer>().material = _morte;            
            VoltarBase();                        
        }      
        
        rx();   
        if(PlayerMovement._gameOver==true){cheats.SetActive(false);}          
    }
    public void Perseguir()
    {
        if(_chkPlayerVivo)
        {
            if(!_medo)
            {
                //
                _agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
                this.gameObject.GetComponent<MeshRenderer>().material = _ghostColor;               

            }else{ 
                //Orientação para onde o fantasma deve ir para fugir do player               
                Vector3 dirToPlayer = transform.position - _player.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;
                _agent.SetDestination(newPos);            
                  
                this.gameObject.GetComponent<MeshRenderer>().material = _medoChange;
            }
        }
    }
    public void Medinho()
    {
        _medo = true;
    }
    public void SemMedo()
    {
        _medo = false;
    }

    public void VoltarBase()
    {    
        //Setar posição pra voltar         
        _agent.SetDestination(_posInicial);        
    }
    private bool _cheatOn = false;
    private void rx()
    {
        //Devs Cheats        
        if(Input.GetKey(KeyCode.F3))
        {
            if(Input.GetKey(KeyCode.M))
            {
                _cheatOn = true;
            }
            if(Input.GetKey(KeyCode.N))
            {
                _cheatOn = false;
                cheats.SetActive(false);
            }
        }

        if(_cheatOn)
        {
            if(Input.GetKeyDown(KeyCode.X)){_medo = true;}    
            if(Input.GetKey(KeyCode.P)){_medo = true;}    
            if(Input.GetKeyUp(KeyCode.X)){_medo = false;}            
            if(Input.GetKeyDown(KeyCode.Z)){_chkGhostVivo = false;}      
            if(Input.GetKey(KeyCode.O)){_chkGhostVivo = false;}      
            if(Input.GetKeyUp(KeyCode.Z)){_chkGhostVivo = true;}
            if(Input.GetKeyDown(KeyCode.H))
            {
                PlayerPrefs.SetInt("highscore", 0);
                PlayerMovement._pontuacao = 0;
            }             
            
            cheats.SetActive(true);         
        } 
    }
}
