using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform[] bgSlots;
    public float scrollingSpeed = 2.5f;
    
    public Transform Planet;
    public float planetSpeed = 7.5f;

    public Transform Planet2;
    public float planet2Speed = 7.5f;

    public Transform[] bgStars;
    public float starSpeed = 5.0f;

    const float BG_WIDTH = 13.5f;

    void Update()
    {
        float minusX = transform.position.x - BG_WIDTH; //백그라운드 X위치에서 왼쪽으로 BG_WIDTH (그림 한장의 폭)만큼 이동한 위치
        
        foreach(Transform bgSlot in bgSlots)
        {
            bgSlot.Translate(-transform.right * scrollingSpeed * Time.deltaTime);

            if(bgSlot.position.x <minusX)
            {
                bgSlot.Translate(transform.right * BG_WIDTH * 3.0f);    //오른쪽으로 bg_width의 세배만큼 이동
            }
        }

        Planet.Translate(-transform.right * planetSpeed * Time.deltaTime);

        if(Planet.position.x <minusX)
        {         //새위치         //행성의 현재 위치에서 오른쪽으로 bg_width만큼 3배~5배 사이로 이동
            Vector3 newPos = Planet.position + transform.right * (BG_WIDTH * 2.5f + Random.Range(0.0f, BG_WIDTH*4.5f));
            newPos.y = transform.position.y + Random.Range(7.5f, 9.5f);
            Planet.position = newPos;
        }

        Planet2.Translate(-transform.right * planet2Speed * Time.deltaTime);

        if (Planet2.position.x < minusX)
        {                    //행성의 현재 위치에서 오른쪽으로 bg_width만큼 2배~4배 사이로 이동
            Vector3 newPos = Planet2.position + transform.right * (BG_WIDTH * 2.5f + Random.Range(0.0f, BG_WIDTH *5.0f));
            Planet2.position = newPos;
        }

        for (int i =0; i<bgStars.Length; i++)
        {
            bgStars[i].transform.Translate(-transform.right * starSpeed * Time.deltaTime);
            if(bgStars[i].position.x < minusX)
            {
                bgStars[i].transform.Translate(transform.right * BG_WIDTH * 3.0f);

                int rand = Random.Range(0, 4);
            }
        }
    }
}
