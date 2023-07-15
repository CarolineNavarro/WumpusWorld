using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGrid : MonoBehaviour
{
    [SerializeField] int LayerSee;

    private Vector3 scale = new Vector3(0.02f, 0.02f, 0.02f);
    private Vector3 previusScale;
    private Transform[] WumpusParts;

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.layer = LayerSee; //muda a layer para renderizar só adjacentes 

        previusScale = col.transform.localScale; //pega a escala atual e depois aumenta para fazer um efeito 
        col.transform.localScale += scale;
        StartCoroutine(ScaleAnim(col));
       
        //mudando  layer de qual tag 
        if(col.gameObject.tag == "Wind")
        {
            ChangeLayer(col);
        }

        if(col.gameObject.tag == "Stink")
        {
            ChangeLayer(col);
        }

        if(col.gameObject.tag == "Gold")
        {
            ChangeLayer(col);
        }

        if(col.gameObject.tag == "Hole")
        {
            ChangeLayer(col);
        }
        
        if(col.gameObject.tag == "Wumpus")
        {
            WumpusParts = col.GetComponentsInChildren<Transform>(); //como o wumpus tem varios filhos precisa ativar o visual de todos

            foreach (Transform parts in WumpusParts)
            parts.transform.gameObject.layer = LayerSee;

        }

    }

    private IEnumerator ScaleAnim(Collider2D aux) //tempo para a escala do tile voltar ao tamanho normal
    {
        yield return new WaitForSeconds(0.5f);
        aux.transform.localScale = previusScale;       
    }

    void ChangeLayer(Collider2D col)  //mudar layer 
    {
        Transform Child =  col.transform.GetChild(0);
        Child.transform.gameObject.layer = LayerSee; 
    }
}
