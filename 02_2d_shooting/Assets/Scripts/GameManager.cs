using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{
    int score = 0;      //���� �����
    static GameManager instance = null;     //static�� �پ �ּҰ� ������ -> ��� Ŭ�������� �ν��Ͻ� �� �� ���� �ּ� ����Ŵ
    public Text scoreText;
    
    private Player player;

    public static GameManager Inst  //������Ƽ
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

    //��������Ʈ : �븮��. �Լ��� ����� �� �ִ� ���� (c����� �Լ������� ������)
    //public delegate void UI_Refresh_Delegate();         //UI_Refresh_Delegate��� �̸��� ��������Ʈ Ÿ�� ���� �� (�Ķ����,����Ÿ�� ���� �Լ��� ���尡���� ��������Ʈ)
    //public UI_Refresh_Delegate onScoreChange = null;    //UI_Refresh_Delegate Ÿ������ OnScoreChange��� �̸��� ��������Ʈ ���� ���� ��

    private void Awake()
    //�̱���: ������ ����(~������ �ڵ��ϴ� ������) �� �ϳ�. Ŭ������ �ν��Ͻ��� �� �ϳ��� �����ϵ��� ����� ��
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);     //���� �Ѿ��(�ٸ� ���� �ε�Ǿ) destroy���� ����.
            instance.Initialize();
        }

        else     //���� ������� gamemager�� �ִٸ� = �ν��Ͻ��� ���� �ƴ϶��
        {   
            if(instance != this)    //���� ��������� ���� �ƴ϶��
            {
                Destroy(this.gameObject);     //�ڱ� �ڽ��� ���ŵ� -> ù��° ������� �͸� ���´�
            }
        }
    }

    void Initialize()
    {
        score = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


}
