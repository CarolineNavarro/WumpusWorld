using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor; 

    [SerializeField] private SpriteRenderer _renderer; 

    public void Init(bool isOffset)
    {
        _renderer.color = _baseColor; //coloca a cor da propiedade no sprite 
    }

}
