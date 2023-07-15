using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;  
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu Instance;

    public GameObject WinPanel;
    public GameObject LosePanel;

    public GameObject Hole;
    public GameObject Wumpus;

    public bool Finish = false;


    void Awake()
    {
      Instance = this;
    }

    public void Retry()
    {
      int index = Random.Range(1, 4);
      SceneManager.LoadScene("Scene" + index);
    }



}
