using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioClip deathSound;

    private AudioSource audioSource;      //피격 사운드 재생용
    private Animator animator;            //애니 재생용
    private ParticleSystem hitParticle;   //피격 효과 파티클
    
    public bool isDead = false;
    public int scoreValue = 50;

    public HitEffectUI hitEffectUI;

    void Awake()
    {  
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        hitParticle = GetComponentInChildren<ParticleSystem>();     //자식에서 컴포넌트 찾음
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //데미지 받는 함수
    private void OnTriggerEnter(Collider other)
    {
        //죽으면 데미지 들어가지 않도록
        if (isDead)
            return;

        if (other.CompareTag("Magic"))
        {
            
            currentHealth -= 100;

            audioSource.Play();

            if (currentHealth <= 0)
                Die();
        }
    }
    /*
        //데미지 받는 함수
        public void TakeDamage(int damage, Vector3 hitPos)
        {
            //죽으면 데미지 들어가지 않도록
            if (isDead)
                return;

            HitEffectUI hitEffect = Instantiate(hitEffectUI, transform.position, Quaternion.identity, transform);
            hitEffect.ShowDamageEffect(damage);

            //피격 파티클 효과를 피격 지점에서 재생
            hitParticle.transform.position = hitPos;
            hitParticle.Play();

            currentHealth -= damage;

            audioSource.Play();

            if (currentHealth <= 0)
                Die();
        }
        */
    //죽는 함수
    void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");     //죽는 애니 재생

        GetComponent<Rigidbody>().isKinematic = true;       //죽으면 물리 영향 안 받도록

        //콜라이더를 모두 다 꺼준다
        foreach (Collider c in GetComponents<Collider>())
            c.enabled = false;

        //EnemyMovement에게 이동 멈추라고 SendMessage
        //gameObject.SendMessage("StopMove");
        GetComponent<EnemyAI>().STATE = EnemyAI.EnemyState.DIE;     //EnemyAI의 상태를 DIE로 변경

        audioSource.clip = deathSound;
        audioSource.Play();                     //죽는 소리 재생

        //GameController 싱글톤 인스턴스를 통해 점수를 더해줌
        //GameController.Instance.Score += scoreValue;
        
        //적의 총 개수를 1개 줄여줌
        EnemySpawner.enemyCount--;
        Count.dragonCount++;
    }

    //죽고 난 뒤 처리 (가라앉는 애니메이션)
    void StartSinking()
    {
        print("StartSinking called");

        //DOTween

        //죽는 연출 시작 - 땅바닥으로 가라앉은 후 삭제
        Destroy(gameObject, 2f);
    }



}