using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth2 : MonoBehaviour
{
    public Image healthBar;
    private float amount = 100;
    float x;

    private Animator animator;              //죽는 애니 재생용
    private PlayerMovement playerMovement;  //이동 상태 제어용
    public bool isDead = false;             //사망 체크 플래그
    private bool isDamaged = false;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage2(float damage)
    {
        x = damage / amount;
        healthBar.fillAmount -= x;
        isDamaged = true;                    //맞은 상태 플래그 on
        if (healthBar.fillAmount <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        print(gameObject.name + " : 디짐");

        isDead = true;      //사망 플래그 on

        animator.SetTrigger("Die");         //죽는 애니메이션 재생

        AudioManager.Instance.PlaySound("Player Death", transform.position);

        GetComponent<Rigidbody>().isKinematic = true;       //밀려나지 않게 해줌

        playerMovement.enabled = false;     //이동 못하게 이동 컴포넌트를 꺼줌

        GameController.Instance.GameOver(); //게임오버 불러줌
    }
}
