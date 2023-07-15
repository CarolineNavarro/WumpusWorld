using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject trail; //recebe a trail visual da flecha 

    void OnTriggerEnter2D(Collider2D col) //identificar colisão 
    {
       if(col.gameObject.tag == "Wumpus")  //se colidir com o wumpus mata o boss da fase
       {
            this.gameObject.layer = 0;  //ativa visual do wumpus 
            trail.SetActive(false);
            
            Transform Child = col.transform.GetChild(1); //ativar vfx de explosao junto com os filhos 
            Child.transform.gameObject.SetActive(true);

           StartCoroutine(CallMenu()); //espera uns segundos e chama o menu de derrota 
       }
    }

    private IEnumerator CallMenu()
    {
        yield return new WaitForSeconds(0.8f);
        Menu.Instance.WinPanel.SetActive(true); //ativa a tela de vitoria 
        Menu.Instance.Finish = true;     
    }

}
