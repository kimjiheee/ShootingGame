using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprenderer;
    AudioSource audioSource;
    IEnumerator fireContinue;
    readonly int anim_hash_InputY = Animator.StringToHash("InputY");
    
    public GameObject flash;
    public GameObject shootPrefab; //프리팹은 gameobject 타입에 담기
    public GameObject gameOver;
    public Transform[] firePosition; //배열.. 비슷한 애들 모아모아..?
    public Image[] UIlife;
    public GameObject reButton;
    public float moveSpeed = 2.0f;
    float boostSpeed = 1.0f;
    Vector3 direction = Vector3.zero;

    bool isDead = false;

    public AudioClip audioHit;
    public AudioClip audioShot;
   
    int life = 3;                    //플레이어 목숨 표시
    public int Life 
    {
        get => life;
        set
        {
            life = value;      //생명 1 감소
            onHit?.Invoke();        //oNhIT 델리게이트 실행(UI 갱신
        }
    }    
    
         //action :c#이 미리 만들어 놓은 delegate 타입
    public Action onHit = null;         //플레이어가 적에게 맞을 때마다 실행될 델리게이트

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        fireContinue = FireCoroutine();
    }

     void FixedUpdate()  
    {
        Move();
    }

    void Move()
    {   
        if(!isDead)
        {
            rigid.MovePosition(transform.position + (direction * moveSpeed * boostSpeed * Time.fixedDeltaTime));
                             //transform.position : 현재 오브젝트의 위치
        }
        else
        {   //뒤로 미는 힘 더하기
            rigid.AddForce(Vector2.left * 0.1f, ForceMode2D.Impulse);      //forcemode2d에는 force와 impulse 두가지 존재.
            //10도씩 돌리는 힘 더하기                                      //force는 쭈우욱 미는거고 impulse는 팍!! 미는거
            rigid.AddTorque(10.0f);
        }
    }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        anim.SetFloat(Animator.StringToHash("InputY"), direction.y);
        //해시함수 : 데이터를 특정 크기의 유일한 요약본으로 만들어주는 함수
        //해시충돌: 다른 데이터를 해시 함수로 돌렸는데 같은 해시 값이 나온 경우
    }

    public void OnFireInput(InputAction.CallbackContext context) //총알 발사
    {
        if (context.started)  //키를 누르기 시작했을 때 (키보드는 started와 performed 차이 없음. 패드는 잇어)/ 차지샷 등에 이용 가능
        {
            StartCoroutine(fireContinue);
        }

        //else if(context.performed) //키를 완전히 눌렀을 때    

        if (context.canceled)    //키를 뗐을 때
        {
            StopCoroutine(fireContinue);
        }
    }

    public void OnBoostInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            boostSpeed = 2.0f;
        }
        if(context.canceled)
        {
            boostSpeed = 1.0f;
        }
    }

    IEnumerator FireCoroutine()
    {
        while(true)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject obj = Instantiate(shootPrefab);  //총알 생성
                obj.transform.position = firePosition[i].position;
                obj.transform.rotation = firePosition[i].rotation;
            }
            flash.SetActive(true);  //비활성화 되어있던 게임 오브젝트 활성화
            StartCoroutine(FlashOff());
            PlaySound("SHOT");

            yield return new WaitForSeconds(0.2f); //0.2초 대기
        }
    }

    IEnumerator FlashOff()
    {
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid"))       
        {
            PlaySound("HIT");
            sprenderer.color = new Color(1, 1, 1, 0.4f);

            gameObject.layer = LayerMask.NameToLayer("Border");     // 플레이어의 레이어를 Border로 변경해서 적과 안 부딪치게 만들기

            Life -= 1;
            UIlife[life].color = new Color(1, 1, 1, 0.1f);

            if (life>0)
            {
                Invoke("OffDamaged", 1.5f);
            }
            else    //죽으면
            {
                Dead();
                reButton.SetActive(true);
            }
        }
    }

    void Dead()
    {
        isDead = true;

        Debug.Log("Game Over");
        PlayerInput input = GetComponent<PlayerInput>();
        input.currentActionMap.Disable();   //조종 안되게 입력 막고

        rigid.gravityScale = 1.0f;          //중력 받아서 떨어지도록
        rigid.freezeRotation = false;       //freeze 풀기 = 회전 막아놓았던 것 풀기

        GetComponent<CapsuleCollider2D>().enabled = false;      //다른 애들과 부딪히는 것 방지

        gameOver.SetActive(true);
    }

    void OffDamaged()
    {
        sprenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void PlaySound(string action)
    {
        //소리 재생
        switch (action)
        {
            case "HIT":
                audioSource.clip = audioHit;
                break;
            case "SHOT":
                audioSource.clip = audioShot;
                break;
        }
        audioSource.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}


