using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_ColliderUpdate : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D c;
    [SerializeField] private PolygonCollider2D[] CameraLimits;

    [SerializeField] private GameObject[] InternetFlatForm = new GameObject[10];
    [SerializeField] private GameObject[] ClearPos = new GameObject[3];

    [SerializeField] private GameObject[] RealWorldObj = new GameObject[10];
    [SerializeField] private GameObject[] InternetWorldObj = new GameObject[10];
    /* 0 : 벽 & 바닥
     * 1 : 문
     * 2 : 바닥
     * 3 : 뒷배경
     * 4 : 클리어 위치
     */

    private int Stage = 1;

    bool Real = true;

    void Start()
    {
        InternetWorldObj[2] = InternetFlatForm[Stage - 1];
        WorldSet();
    }

    //테스트용 없앨 예정
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Real = !Real;
            WorldSet();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ClearCheck")
        {
            collision.gameObject.SetActive(false);
            InternetWorldObj[1].SetActive(false);
        }
        else if (collision.gameObject.name == "StageFinish")
        {
            if (Stage == 3 || Stage == 5 || Stage == 10) Real = true;

            ClearStage();
        }
    }

    private void ClearStage()
    {
        if (Stage < 4) InternetWorldObj[4] = ClearPos[Stage - 1];
        else InternetWorldObj[4] = new GameObject();

        Stage++;
        InternetWorldObj[2] = InternetFlatForm[Stage - 1];

        WorldSet();
    }

    

    void WorldSet()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Real)
            {
                RealWorldObj[i].SetActive(true);
                InternetWorldObj[i].SetActive(false);
            }
            else
            {
                RealWorldObj[i].SetActive(false);
                InternetWorldObj[i].SetActive(true);
            }
        }

        c.m_BoundingShape2D = CameraLimits[Convert.ToInt32(Real)];

        transform.position = new Vector3(0, -3, 0);
    }
}
