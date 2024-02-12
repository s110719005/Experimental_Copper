using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Hammer")
        {
            Debug.Log("EXIT");
            Application.Quit();
        }
    }
}
