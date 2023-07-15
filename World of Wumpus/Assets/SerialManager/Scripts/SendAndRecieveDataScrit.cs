using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAndRecieveDataScrit : MonoBehaviour
{
    [SerializeReference] float posX;
 

    // Start is called before the first frame update
    void Start()
    {
        SerialManagerScript.WhenReceiveDataCall += ReciveData;
        
    }

    public void ReciveData(string incomingString)
    {

       float.TryParse(incomingString, out posX);
        
        Debug.Log(incomingString);
       
    }



    void Update()
    {
    

    }

 
}
