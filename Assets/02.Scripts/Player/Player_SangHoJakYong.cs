using UnityEngine;

public class Player_SangHoJakYong : MonoBehaviour
{
    GameObject PlayerMain;
    GameObject ButtonObj_Copy;//雌硲拙遂徹 朝杷遂亀績. ぞぞぞぞぞぞぞ 蟹 設梅臓?? ぞぞぞ^^
    Rigidbody2D ri;
    sbyte Face;// 1 R, -1 L

    [SerializeField] GameObject ButtonObj;
    [SerializeField] GameObject ButtonPos;

    void Start()
    {
        PlayerMain = GameObject.Find("Player");
        ri = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Face = PlayerMain.GetComponent<Player>().Face;
        SangHoJakYong();
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }

    void SangHoJakYong()
    {
        //据沙 覗軒鍋 督雨亜 焼観, 差紫吉 神崎詮闘 督雨
        Destroy(ButtonObj_Copy);
        RaycastHit2D RaySi = Physics2D.Raycast(ri.position, Vector3.right * Face, 1, LayerMask.GetMask("Water"));
        Debug.DrawRay(ri.position, Vector3.right * Face, new Color(0, 1, 0));
        if (RaySi.collider != null)
        {

            if (RaySi.distance < 1.0f)
            {
                //据沙覗軒鍋 督雨 号走研 是廃 差紫
                ButtonObj_Copy = Instantiate(ButtonObj, ButtonPos.transform.position, ButtonPos.transform.rotation);
            }
        }
    }
}
