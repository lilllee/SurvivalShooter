using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth        //현재 체력 Property
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            //값 보정
            if (currentHealth < 0)
                currentHealth = 0;
            else if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            //UI 업데이트
            healthSlider.value = currentHealth;
        }
    }

    public int maxHealth = 100;     //최대 체력
    private int currentHealth;       //현재 체력

    public Image damageImage;       //피격효과 재생용 Image

    public bool isDead = false;     //사망 체크 플래그

    public Color damageColor;       //맞을 때 컬러

    public Slider healthSlider;     //HP 표시 Slider


    private Animator animator;              //죽는 애니 재생용
    private PlayerMovement playerMovement;  //이동 상태 제어용

    private bool isDamaged = false;         //맞았는지 체크하는 bool

    public HitEffectUI hitEffectUI;


    // Start is called before the first frame update
    void Awake() 
    {
        //필요한 컴포넌트의 레퍼런스를 받아옴
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDamaged)
        {
            //damageImage를 빨갛게 해주고
            damageImage.color = damageColor;
            isDamaged = false;
        }
        else
        {
            //damageImage의 alpha를 투명한 색으로 Lerp
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime);
        }
    }

    //데미지를 받는 함수
    public void TakeDamage(int damage)
    {
        //죽었으면 데미지를 받지 않게
        if (isDead)
            return;

        //어떤 컴포넌트로든 Instantiate가 가능 (그 컴포넌트가 프리팝에 있을 경우)
        //Instantiate의 리턴값이 그 컴포넌트로 나옴
        //HitEffectUI hitEffect = Instantiate(hitEffectUI, transform.position, Quaternion.identity);
        HitEffectUI hitEffect = Instantiate(hitEffectUI, transform.position, Quaternion.identity, transform);
        hitEffect.ShowDamageEffect(damage);
        
        AudioManager.Instance.PlaySound("Player Hurt", transform.position);

        CurrentHealth -= damage;        //체력을 까줌

        isDamaged = true;               //맞은 상태 플래그 on

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    //죽었을 때 처리
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
