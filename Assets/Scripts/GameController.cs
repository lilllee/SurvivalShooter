using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;      //DOTween 에 필요!

public class GameController : Singleton<GameController>
{
    public int Score                            //스코어 프로퍼티
    {
        get
        {
            return score;
        }
        set
        {

            int scoreDiff = value - score;      //스코어 차이

            score = value;
            ScoreUI.text = "SCORE : " + score.ToString();

            //#Todo - 더해지는 스코어 값이 클 수록 효과도 더 크게 나타나게!
            ScoreUI.DOColor(Color.green, 0.5f).From();     //초록색에서 0.33초 동안 원래 색으로 돌아오라.
            ScoreUI.transform.DOScale(1f + (float)scoreDiff / 100f, 1f).From().SetEase(Ease.OutElastic);

            //스코어값이 베스트스코어보다 크면 베스트스코어 값을 업데이트
            if(score > bestScore)
                BestScore = score;
        }
    }

    public int BestScore        //베스트스코어 프로퍼티
    {
        get
        {
            return bestScore;
        }
        set
        {
            bestScore = value;
            BestscoreUI.text = "BESTSCORE : " + bestScore.ToString();
            BestscoreUI.DOColor(Color.magenta, 0.5f).From();
            BestscoreUI.transform.DOScale(1.5f, 0.5f).From().SetEase(Ease.OutElastic);
        }
    }

    private int score;
    public Text ScoreUI;        //스코어 표시용 UI

    private int bestScore;      //베스트 스코어 저장용
    public Text BestscoreUI;    //베스트스코어 표시용 UI


    public bool isGameOver = false;
    private Animator animator;

    //override = 상속받은 함수를 재정의
    protected override void Awake()
    {
        base.Awake();       //부모 클래스의 함수를 불러줌

        animator = GetComponent<Animator>();
        BestScore = PlayerPrefs.GetInt("BestScore", 0); //베스트스코어 로드
    }

    //게임오버 처리
    public void GameOver()
    {
        //게임오버는 한번만 되도록
        if (isGameOver)
            return;
        
        animator.SetTrigger("GameOver");        //게임오버 애니메이션 재생

        isGameOver = true;                      //게임오버 플래그 on

        //일정 시간 지나면 자동으로 리스타트 되도록 (현재 씬을 다시 로드함)

        PlayerPrefs.SetInt("BestScore", bestScore);     //베스트스코어 저장
    }
}
