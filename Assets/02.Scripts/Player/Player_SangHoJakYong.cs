using System;
using UnityEngine;

public class Player_SangHoJakYong : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKeyCode;
    GameObject PlayerMain;
    GameObject ButtonObj_Copy;//상호작용키 카피용도임. ㅎㅎㅎㅎㅎㅎㅎ 나 잘했징?? ㅎㅎㅎ^^
    Rigidbody2D ri;
    sbyte Face;// 1 R, -1 L

    [SerializeField] GameObject ButtonObj;
    [SerializeField] GameObject ButtonPos;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    void Start()
    {
        PlayerMain = GameObject.Find("Player");
        ri = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Face = PlayerMain.GetComponent<Player>().Face;
        if (_player.isCutScene == false)
        {
            SangHoJakYong();
        }
    }

    void SangHoJakYong()
    {
        //원본 프리펩 파괴가 아닌, 복사된 오브젝트 파괴
        Destroy(ButtonObj_Copy);
        RaycastHit2D RaySi = Physics2D.Raycast(ri.position, Vector3.right * Face, 1, LayerMask.GetMask("Interable"));
        Debug.DrawRay(ri.position, Vector3.right * Face, new Color(0, 1, 0));
        if (RaySi.collider != null)
        {
            if (Input.GetKeyDown(interactionKeyCode))
            {
                RaySi.transform.GetComponent<Interaction>().action?.Invoke();
            }
            
            if (RaySi.distance < 1.0f)
            {
                //원본프리펩 파괴 방지를 위한 복사
                ButtonObj_Copy = Instantiate(ButtonObj, ButtonPos.transform.position, ButtonPos.transform.rotation);
            }
        }
    }
}
