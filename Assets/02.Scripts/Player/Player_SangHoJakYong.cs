using UnityEngine;

public class Player_SangHoJakYong : MonoBehaviour
{
    GameObject PlayerMain;
    GameObject ButtonObj_Copy;//��ȣ�ۿ�Ű ī�ǿ뵵��. �������������� �� ����¡?? ������^^
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
        //���� ������ �ı��� �ƴ�, ����� ������Ʈ �ı�
        Destroy(ButtonObj_Copy);
        RaycastHit2D RaySi = Physics2D.Raycast(ri.position, Vector3.right * Face, 1, LayerMask.GetMask("Water"));
        Debug.DrawRay(ri.position, Vector3.right * Face, new Color(0, 1, 0));
        if (RaySi.collider != null)
        {

            if (RaySi.distance < 1.0f)
            {
                //���������� �ı� ������ ���� ����
                ButtonObj_Copy = Instantiate(ButtonObj, ButtonPos.transform.position, ButtonPos.transform.rotation);
            }
        }
    }
}
