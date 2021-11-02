using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using DG.Tweening;

public class MagicShot : MonoBehaviour
{
    public ParticleSystem magicParticle;   //공격 했을때 생성되는파티클 시스템을 선언 2가지 선언
    public Light magicLight;               //공격 했을 때 나오는 빛 선언
    public GameObject magicObject;         //마법 MeshRenderer끈 오브젝트 파일
    public AudioMixerGroup shot;


    public Image barImage;
    private float amount = 0.4f;
    private float fillAmount = 0.15f;

    public Canvas following;
    public Transform sp;
    

    private void Awake()
    {

        //magicParticle = GetComponent<ParticleSystem>();
        magicLight = GetComponent<Light>();
    
    }
    // Update is called once per frame
    void Update()
    {
        //barImage.DOFillAmount(1f,Time.deltaTime).SetEase(Ease.Linear);
         barImage.fillAmount += Time.deltaTime * fillAmount;
        if (Input.GetButtonDown("Fire1")&& barImage.fillAmount > 0.1f)     //마우스 좌클릭을 눌렀을때 실행 
        {
            AudioManager.Instance.PlaySound("PiSung PaSung", transform.position);
            barImage.fillAmount -= amount;
            ShotMagic();                      
        }
        following.transform.position = transform.root.position;
    }
    void ShotMagic()
    {
        // magicParticle.Play();                                    //첫번째 배열에 들어있는 마법 파티클 실행
        Instantiate(magicObject, sp.position, transform.rotation);   //오브젝트 생성 플레이어 위치에서
        //AudioManager.Instance.PlaySound(new string[] { "Voice1", "Voice2", "Voice3", "Voice4" }, transform.position); 

    }
}
