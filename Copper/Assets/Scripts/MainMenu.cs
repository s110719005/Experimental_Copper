using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Copper copper;

    private void Update() 
    {
        if(copper.transform.localScale.z >= 20)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
