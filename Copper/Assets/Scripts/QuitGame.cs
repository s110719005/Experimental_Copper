using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "";
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Hammer")
        {
            if(sceneName != "")
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("EXIT");
                Application.Quit();
            }
        }
    }
}
