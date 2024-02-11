using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperManager : MonoBehaviour
{
    [SerializeField]
    private List<Copper> coppers;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ResetCoppers()
    {
        foreach (var copper in coppers)
        {
            copper.Reset();
        }
    }
}
