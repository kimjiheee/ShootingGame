using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{
    int score = 0;      //점수 저장용
    static GameManager instance = null;     //static이 붙어서 주소가 고정됨 -> 모든 클래스들이 인스턴스 될 때 같은 주소 가르킴
    public Text scoreText;
    
    private Player player;

    public static GameManager Inst  //프로퍼티
    {
        get =>  instance;
    }

    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = $"Score : {score:d4}";
        }
    }

    public Player MainPlayer { get => player; }

    //델리게이트 : 대리자. 함수를 등록할 수 있는 변수 (c언어의 함수포인터 발전형)
    //public delegate void UI_Refresh_Delegate();         //UI_Refresh_Delegate라는 이름의 델리게이트 타입 만든 것 (파라메터,리턴타입 없는 함수만 저장가능한 델리게이트)
    //public UI_Refresh_Delegate onScoreChange = null;    //UI_Refresh_Delegate 타입으로 OnScoreChange라는 이름의 델리게이트 변수 만든 것

    private void Awake()
    //싱글톤: 디자인 패턴(~식으로 코딩하니 좋더라) 중 하나. 클래스의 인스턴스가 단 하나만 존재하도록 만드는 것
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);     //씬을 넘어가도(다른 씬이 로드되어도) destroy하지 마라.
            instance.Initialize();
        }

        else     //먼저 만들어진 gamemager가 있다면 = 인스턴스가 널이 아니라면
        {   
            if(instance != this)    //먼저 만들어진게 내가 아니라면
            {
                Destroy(this.gameObject);     //자기 자신이 제거됨 -> 첫번째 만들어진 것만 남는다
            }
        }
    }

    void Initialize()
    {
        score = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


}
