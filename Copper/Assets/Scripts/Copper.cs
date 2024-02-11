using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Copper : MonoBehaviour
{
    
    private Vector3 originScale;
    private Vector3 originPosition;
    [SerializeField]
    private Vector3 stretchSpeed;
    [SerializeField]
    private bool canStretch = true;
    [SerializeField]
    private bool isForeverCharged = false;
    [SerializeField]
    private bool isGoal = false;
    public bool IsForeverCharged => isForeverCharged;
    private bool isTempCharged = false;
    private bool IsTempCharged => isTempCharged;

    private Copper motherCharger;

    [SerializeField]
    private Renderer _renderer;
    private Color originColor;
    // Start is called before the first frame update
    void Start()
    {
        originScale = this.transform.localScale;
        originPosition = this.transform.position;
        originColor = _renderer.material.color;
        if(isForeverCharged)
        {
            SetCharged();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Hammer")
        {
            Extend();
        }
        if(other.gameObject.TryGetComponent<Copper>(out Copper copper))
        {
            CheckCharge(copper);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(!isForeverCharged)
        {
            if(other.gameObject.TryGetComponent<Copper>(out Copper copper))
            {
                if(motherCharger == copper)
                {
                    isTempCharged = false;
                    SetUnCharged();
                    motherCharger = null;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(!isForeverCharged && !isTempCharged)
        {
            if(other.gameObject.TryGetComponent<Copper>(out Copper copper))
            {
                CheckCharge(copper);
            }
        }
    }

    private void CheckCharge(Copper copper)
    {
        if(copper.IsForeverCharged || copper.IsTempCharged)
        {
            isTempCharged = true;
            SetCharged();
            motherCharger = copper;
            if(isGoal)
            {
                GameCore.Instance.EndGame();
            }
        }
    }

    private void SetCharged()
    {
        _renderer.material.color = Color.blue;
    }
    private void SetUnCharged()
    {
        _renderer.material.color = originColor;
    }

    private void Extend()
    {
        if(!canStretch) { return; }
        Vector3 newScale = this.transform.localScale;
        newScale = new Vector3(newScale.x * stretchSpeed.x, newScale.y * stretchSpeed.y, newScale.z * stretchSpeed.z);
        if(newScale.y <= 0.02f) { return; }
        this.transform.DOScaleX(newScale.x, 1);
        this.transform.DOScaleY(newScale.y, 1);
        this.transform.DOScaleZ(newScale.z, 1);
    }

    public void Reset()
    {
        this.transform.DOScaleX(originScale.x, 1);
        this.transform.DOScaleY(originScale.y, 1);
        this.transform.DOScaleZ(originScale.z, 1);
        this.transform.position = originPosition;
        if(!isForeverCharged)
        {
            isTempCharged = false;
            SetUnCharged();
        }
    }
}
