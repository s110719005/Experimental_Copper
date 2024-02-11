using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject hammer;
    [SerializeField]
    private CopperManager copperManager;
    private bool canUseHammer = true;
    private Vector3 originPosition;

    //JOYCON
    private List<Joycon> joycons;
    private Vector3 accel;
    private int jc_ind = 0;
    private Copper handCopper = null;

    private Copper interactCopper = null;

    private CopperPlaceSpot interactPlacement = null;
    void Start()
    {
        originPosition = transform.position;
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canUseHammer)
        {
            UseHammer();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(handCopper && interactPlacement) { PlaceCopper(interactPlacement); }
            if(!handCopper && interactCopper) { PickUpCopper(interactCopper); }
        }
        if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];

            accel = j.GetAccel();
            if(accel.y >= 5 && canUseHammer)
            {
                UseHammer();
                j.SetRumble (160, 320, 0.6f, 200);
            }

			if (j.GetButton(Joycon.Button.DPAD_DOWN))
            {
                if(copperManager)
                {
                    copperManager.ResetCoppers();
                }
                transform.position = originPosition;
			} 
			if (j.GetButton(Joycon.Button.DPAD_RIGHT))
            {
                if(handCopper && interactPlacement) { PlaceCopper(interactPlacement); }
                if(!handCopper && interactCopper) { PickUpCopper(interactCopper); }
            }
        }
    }

    void FixedUpdate()
    {
        if(_rigidbody)
        {
            if (joycons.Count > 0)
            {
                Joycon j = joycons [jc_ind];
                Vector3 m_Input = new Vector3(j.GetStick()[0], 0, j.GetStick()[1]);
                _rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
            }
            else
            {
                Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                _rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
            }
            
            
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent<Copper>(out Copper copper))
        {
            interactCopper = copper;
        }
        if(other.gameObject.TryGetComponent<CopperPlaceSpot>(out CopperPlaceSpot copperPlaceSpot))
        {
            interactPlacement = copperPlaceSpot;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.TryGetComponent<Copper>(out Copper copper))
        {
            interactCopper = null;
        }
        if(other.gameObject.TryGetComponent<CopperPlaceSpot>(out CopperPlaceSpot copperPlaceSpot))
        {
            interactPlacement = null;
        }
    }

    private void PickUpCopper(Copper copper)
    {
        if(copper.IsPlaced) { return; }
        handCopper = copper;
        copper.Picked();
        copper.transform.SetParent(transform);
        Debug.Log("Pick");
        interactCopper = null;

    }

    private void PlaceCopper(CopperPlaceSpot placement)
    {
        if(handCopper && !placement.IsFull)
        {
            placement.SetFull();
            handCopper.Placed();
            handCopper.transform.position = new Vector3(placement.transform.position.x, handCopper.transform.position.y, placement.transform.position.z);
            handCopper.transform.eulerAngles = placement.transform.eulerAngles;
            handCopper = null;
            Debug.Log("PLACED");
        }
    }

    private void UseHammer()
    {
        HammerAnimation();
    }
    private void HammerAnimation()
    {
        canUseHammer = false;
        Vector3 endRotation = new Vector3(90, 0, 0);
        DG.Tweening.Sequence hammerAnimationSequence = DOTween.Sequence();
        hammerAnimationSequence.Append(hammer.transform.DORotate(endRotation, 0.2f).SetEase(Ease.InCubic)).
                                Append(hammer.transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.OutCubic));
        hammerAnimationSequence.AppendCallback(() => { canUseHammer = true; });
    }
}
