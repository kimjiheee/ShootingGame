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
    public GameObject shootPrefab; //�������� gameobject Ÿ�Կ� ���
    public GameObject gameOver;
    public Transform[] firePosition; //�迭.. ����� �ֵ� ��Ƹ��..?
    public Image[] UIlife;
    public GameObject reButton;
    public float moveSpeed = 2.0f;
    float boostSpeed = 1.0f;
    Vector3 direction = Vector3.zero;

    bool isDead = false;

    public AudioClip audioHit;
    public AudioClip audioShot;
   
    int life = 3;                    //�÷��̾� ��� ǥ��
    public int Life 
    {
        get => life;
        set
        {
            life = value;      //���� 1 ����
            onHit?.Invoke();        //oNhIT ��������Ʈ ����(UI ����
        }
    }    
    
         //action :c#�� �̸� ����� ���� delegate Ÿ��
    public Action onHit = null;         //�÷��̾ ������ ���� ������ ����� ��������Ʈ

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
                             //transform.position : ���� ������Ʈ�� ��ġ
        }
        else
        {   //�ڷ� �̴� �� ���ϱ�
            rigid.AddForce(Vector2.left * 0.1f, ForceMode2D.Impulse);      //forcemode2d���� force�� impulse �ΰ��� ����.
            //10���� ������ �� ���ϱ�                                      //force�� �޿�� �̴°Ű� impulse�� ��!! �̴°�
            rigid.AddTorque(10.0f);
        }
    }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        anim.SetFloat(Animator.StringToHash("InputY"), direction.y);
        //�ؽ��Լ� : �����͸� Ư�� ũ���� ������ ��ົ���� ������ִ� �Լ�
        //�ؽ��浹: �ٸ� �����͸� �ؽ� �Լ��� ���ȴµ� ���� �ؽ� ���� ���� ���
    }

    public void OnFireInput(InputAction.CallbackContext context) //�Ѿ� �߻�
    {
        if (context.started)  //Ű�� ������ �������� �� (Ű����� started�� performed ���� ����. �е�� �վ�)/ ������ � �̿� ����
        {
            StartCoroutine(fireContinue);
        }

        //else if(context.performed) //Ű�� ������ ������ ��    

        if (context.canceled)    //Ű�� ���� ��
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
                GameObject obj = Instantiate(shootPrefab);  //�Ѿ� ����
                obj.transform.position = firePosition[i].position;
                obj.transform.rotation = firePosition[i].rotation;
            }
            flash.SetActive(true);  //��Ȱ��ȭ �Ǿ��ִ� ���� ������Ʈ Ȱ��ȭ
            StartCoroutine(FlashOff());
            PlaySound("SHOT");

            yield return new WaitForSeconds(0.2f); //0.2�� ���
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

            gameObject.layer = LayerMask.NameToLayer("Border");     // �÷��̾��� ���̾ Border�� �����ؼ� ���� �� �ε�ġ�� �����

            Life -= 1;
            UIlife[life].color = new Color(1, 1, 1, 0.1f);

            if (life>0)
            {
                Invoke("OffDamaged", 1.5f);
            }
            else    //������
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
        input.currentActionMap.Disable();   //���� �ȵǰ� �Է� ����

        rigid.gravityScale = 1.0f;          //�߷� �޾Ƽ� ����������
        rigid.freezeRotation = false;       //freeze Ǯ�� = ȸ�� ���Ƴ��Ҵ� �� Ǯ��

        GetComponent<CapsuleCollider2D>().enabled = false;      //�ٸ� �ֵ�� �ε����� �� ����

        gameOver.SetActive(true);
    }

    void OffDamaged()
    {
        sprenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void PlaySound(string action)
    {
        //�Ҹ� ���
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


