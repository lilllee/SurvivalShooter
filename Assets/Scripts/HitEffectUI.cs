using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitEffectUI : MonoBehaviour
{
    public Text damageText;     //데미지 표시 텍스트 레퍼런스

    private void Update()
    {
        transform.LookAt(Camera.main.transform);        //늘 메인카메라를 바라봐라
    }

    //데미지를 표시하는 연출 재생 함수
    public void ShowDamageEffect(int _damage)
    {
        damageText.text = _damage.ToString();

        //트윈을 해주고
        damageText.transform.DOScale(1.5f, 0.5f).From();
        damageText.DOColor(Color.clear, 0.5f).SetDelay(0.5f);

        //1초 후 삭제
        Destroy(this.gameObject, 1f);
    }

}
