using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform agent;
    [SerializeField] GameObject Arrow;
    [SerializeField] int posX, posY;  
  
    [SerializeField] private int _width, _height;
    
    [SerializeField] private Transform _cam;

    [SerializeField] private float _tileSize;

    private Dictionary<Vector2, Tile> _tiles;

    public Tile[] _tilesBase;

    private int cont,contArrow;

    private bool shoot = false;
    private bool buttonShoot=false;
    private float Move;
    private float rot;

    void Start()
    {   
        //gera o grid no start 
        GenerateGrid();
        SerialManagerScript.WhenReceiveDataCall += ReciveData; //abre porta para comunicar com arduino - recebe dados 
    }
    
    //recebe data do arduino por meio de string e depois converte para float
    public void ReciveData(string incomingString)
    {
        float.TryParse(incomingString, out Move);
    }

    void Update()
    {   
        //verifica de o a bool buttonshot é true e se temos flecha(munição) para atirar com o cont arrow
        if(buttonShoot == true && contArrow == 0) 
        {
            shoot = true; 
        }

        //se for verdade dispara a Coroutine de atirar
        if(shoot == true)
        {
            StartCoroutine(ShootArrow());  
        }
    }

    //verifica com qual gameobjects o player esta em contato pela tag do collider isso tudo para mudar a layer para aparecer na camera
    // somente os quadrados adjacentes  
    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "Wind")
       {
          SerialManagerScript.SendInfo("v"); //manda info para o arduino se mexer rapidamente simulando o vento
          Menu.Instance.Hole.SetActive(true); 
       }
       else
       {
          Menu.Instance.Hole.SetActive(false);
       }

       if(col.gameObject.tag == "Wumpus")
       {
           Menu.Instance.LosePanel.SetActive(true); //se entrar em contato com o boss morre e chama o painel do menu 

           SerialManagerScript.SendInfo("l"); //acende a led vermelha no arduino 
       }

       if(col.gameObject.tag == "Hole")
       {
           Menu.Instance.LosePanel.SetActive(true);

           SerialManagerScript.SendInfo("l");  //acende a led azul no arduino  
       }

       if(col.gameObject.tag == "Stink")
       {
          SerialManagerScript.SendInfo("p");
          Menu.Instance.Wumpus.SetActive(true);
       }
       else
       {
          Menu.Instance.Wumpus.SetActive(false);
       }

       if(col.gameObject.tag == "Gold")
       {
           Menu.Instance.WinPanel.SetActive(true);
           
           SerialManagerScript.SendInfo("u");
       }  
      
    }

    public void Shoot() //função de menu 
    {
       buttonShoot = true;
    }

#region MovementInput  //essa região é para fazer toda a logica de movimento manupulando o transform do gameObject

    public void Frente()
    {
        //nao pode andar enquanto atira por isso a verificação 
        if(shoot != true)
        {    
            //verifica se o menu esta aberto pois não pode ter movimento nesse periodo
            if(Menu.Instance.Finish != true)
            {

                if(rot == 0f  && agent.transform.position.y < 3)  //movimento frente quando estiver apontando cima  
                {
                    agent.transform.position += new Vector3( posX, 1 + posY, 0);
                }

                if((rot == 180f || rot == -180f) &&  agent.transform.position.y > 0)  //movimento frente quando estiver apontando para baixo
                {
                    agent.transform.position -= new Vector3(posX, 1 - posY, 0);
                }

                if(rot == 90f && (agent.transform.position.x > 0 && agent.transform.position.x < 3)) //movimento frente quando estiver apontando para esquerda
                {
                    agent.transform.position -= new Vector3(1 - posX, posY, 0);
                }

                if(rot == -90f && agent.transform.position.x <3) //movimento frente quando estiver apontando para direita 
                { 
                    agent.transform.position += new Vector3(1 + posX, posY, 0);
                }
                SerialManagerScript.SendInfo("a"); //envia para o carrinho andar para frente
            }
            
        }
    }
    public void Tras()
    {
        if(shoot != true)
        { 
            if(Menu.Instance.Finish != true)
            {
                if(rot== 0f && agent.transform.position.y > 0)
                {
                    agent.transform.position -= new Vector3(posX, 1-posY, 0);
                }

                if((rot== 180f || rot == -180f) && agent.transform.position.y < 3)
                {
                    agent.transform.position += new Vector3(posX, 1+posY, 0);
                }

                if(rot== 90f && agent.transform.position.x < 3)
                {
                    agent.transform.position += new Vector3(1 + posX, posY, 0);
                }

                if(rot== -90f && agent.transform.position.x > 0)
                {
                    agent.transform.position -= new Vector3(1 - posX, posY, 0);
                }
                SerialManagerScript.SendInfo("b");
            }
        }
    }
    public void Direita()
    {
        if(shoot != true)
        {   
            if(Menu.Instance.Finish != true)
            {
                if(rot != -180f) //garante que so vou rodar ate o meio 
                {
                    rot += (-90f);
                    agent.transform.rotation = Quaternion.Euler(0f,0f, rot) ;
                    SerialManagerScript.SendInfo("c");
                }
            }
        }
    }
    public void Esquerda()
    {
        if(shoot != true)
        {
            if(Menu.Instance.Finish != true)
            {
                if(rot != 180f) //garante que so vou rodar ate o meio 
                {
                    rot += (90f);
                    agent.transform.rotation =  Quaternion.Euler(0f,0f,rot);
                    SerialManagerScript.SendInfo("d");
                }
            }
        }
    }

#endregion

#region Arrow

    //couroutine de atirar 
    private IEnumerator ShootArrow()
    {
        //ativa o gameObject e aplica o movimento na direção Up(0,1,0)
        Arrow.SetActive(true);
        Arrow.transform.Translate(Vector3.up * Time.deltaTime); 
        yield return new WaitForSeconds(3); //espera 3 segundo e desativa a flecha

        contArrow++;
        shoot = false;

        if(contArrow == 1)
        {
            NoArrow();
        }
    }

    void NoArrow()
    {
        Destroy(Arrow.gameObject); //destroy flecha 
    }

#endregion

#region Grid
    
    //fução de gerar grid/ percorre um for gerando cada um com um offtset pre determidado 
    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                
                var spawnedTile = Instantiate(_tilesBase[cont], new Vector3(x, y), Quaternion.identity);
                
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.transform.localScale = Vector3.one *_tileSize;
               
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                 
                cont++; 
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10); //coloca camera no centro da tela sempre
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

#endregion
}
