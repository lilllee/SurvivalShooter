using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttack = false;
    public float attackRate;            //공격 속도
    private float nextAttackTime;       //다음 공격 시간

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Invoke("Scream", 4f);
    }

    //플레이어가 트리거 영역 안에 들어오면 공격해라
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Magic"))
        {
            animator.SetTrigger("Dead");
            AudioManager.Instance.PlaySound("Dragon1", transform.position);
        }
    }

    private void Update()
    {
        if (!isAttack)
        {
            animator.SetBool("IsBreath", true);
            isAttack = true;
        }
        else
        {
            animator.SetBool("IsBreath", false);
        }
    }

    void Scream()
    {
        AudioManager.Instance.PlaySound("Dragon2", transform.position);
    }
}
