using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Copper : MonoBehaviour
{
    
    private Vector3 originScale;
    private Vector3 originPosition;
    // Start is called before the first frame update
    void Start()
    {
        originScale = this.transform.localScale;
        originPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetShape();
        }
    }

    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Hammer")
        {
            Extend();
        }
    }

    private void Extend()
    {
        Vector3 newScale = this.transform.localScale;
        newScale = new Vector3(newScale.x * 1.05f, newScale.y * 0.8f, newScale.z * 1.2f);
        if(newScale.y <= 0.02f) { return; }
        this.transform.DOScaleX(newScale.x, 1);
        this.transform.DOScaleY(newScale.y, 1);
        this.transform.DOScaleZ(newScale.z, 1);
    }

    private void ResetShape()
    {
        this.transform.DOScaleX(originScale.y, 1);
        this.transform.DOScaleY(originScale.y, 1);
        this.transform.DOScaleZ(originScale.z, 1);
        this.transform.position = originPosition;
    }
}
