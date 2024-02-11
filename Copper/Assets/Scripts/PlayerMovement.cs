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
    private bool canUseHammer = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canUseHammer)
        {
            UseHammer();
        }
    }

    void FixedUpdate()
    {
        if(_rigidbody)
        {
            Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
            
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
