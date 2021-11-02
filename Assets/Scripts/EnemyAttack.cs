using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;
    public float attackDamage = 10f;       //공격 당 데미지
    private bool isAttack = false;
    public float attackRate;            //공격 속도
    private float nextAttackTime;       //다음 공격 시간

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //플레이어가 트리거 영역 안에 들어오면 공격해라
    private void OnTriggerStay(Collider other)
    {
        //부딪힌 게 플레이어면
        //#Todo - 일정 간격마다 공격이 나가도록
        if (other.CompareTag("Player"))
        {
            if(Time.time >= nextAttackTime)     //쿨타임이 찼으면
            {
                nextAttackTime = Time.time + attackRate;        //다음 공격 시간을 설정
                Attack();
                other.SendMessage("TakeDamage2", attackDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
        void Attack()
        {
            isAttack = true;
            // other.GetComponent<PlayerHealth2>().TakeDamage2(attackDamage);        //강한 결합 (X)
            ;
        }
    }

    

    private void Update()
    {
        if (!isAttack)
        {
            animator.SetBool("IsWalking", true);
            isAttack = true;
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}