using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("����ϰų�ó�ֱ�")]
    Rigidbody2D ri;
    Animator animer;

    [Header("�÷��̾� �⺻����")]
    [SerializeField] float PlayerHP;
    [SerializeField] float PlayerSpeed;
    [SerializeField] float JumpScale;
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    bool DDang= false;
    bool isDasing;
    public sbyte Face =1; // 1 R, -1 L

    public Vector3 developmentVelocity;

    [Header("�÷��̾� �⺻����")]
    [SerializeField] GameObject S_Jump;
    [SerializeField] GameObject S_Dash;

    void Start()
    {
        ri = GetComponent<Rigidbody2D>();
        animer = GetComponent<Animator>();
    }

    void Update()
    {
        animer.SetFloat("Run", Mathf.Abs(ri.velocity.x));
        Move();
        JumpGamji();
        Jump();
        Dash();
        SetFace();
    }

#region ������
    private void Move()
    {
        //�̵�
        if (isDasing == false) 
        { 
            Vector3 vel = new Vector3(1,0,0) * Input.GetAxisRaw("Horizontal") * PlayerSpeed * 1;
            vel.y = ri.velocity.y;
            vel += developmentVelocity;
            ri.velocity = vel; 
        }
        //Face
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            Face = -1;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            Face = 1;
        }
    }

    void SetFace()
    {
        if(Face == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);//������ȯ
        }
        else if (Face == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);//������ȯ
        }
    }
    #endregion

#region ��������
    private void JumpGamji()
    {
        //Debug.DrawRay(ri.position, Vector3.down, new Color(0,1,0));
        RaycastHit2D RaySir = Physics2D.Raycast(ri.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (ri.velocity.y < 0)
        {
            if (RaySir.collider != null)
            {
                if (RaySir.distance < 1.0f)
                {
                    animer.SetBool("Jump", false);
                    DDang = false;
                }
            }
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !DDang)
        {
            Sound_Jump();
            animer.SetBool("Jump", true);
            ri.AddForce(Vector3.up * JumpScale, ForceMode2D.Impulse);
            DDang = true;
        }
    }
    #endregion

#region �뽬
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.S) && isDasing == false)
        {
            Sound_Dash();
            isDasing = true;
            animer.SetBool("Dash", true);
            ri.AddForce(new Vector3(dashSpeed * Face, 0, 0), ForceMode2D.Impulse);
            Invoke("EndDash", dashTime+0.2f);
        }
    }
    void EndDash()
    {
        isDasing = false;
        animer.SetBool("Dash", false);
    }
    #endregion

#region ����
    void Sound_Jump()
    {
        //���������� �ı� ������ ���� ����
        var copy = Instantiate(S_Jump, this.transform.position, this.transform.rotation);
        //���� ������ �ı��� �ƴ�, ����� ������Ʈ �ı�
        Destroy(copy, 1.5f);
    }
    void Sound_Dash()
    {
        //���������� �ı� ������ ���� ����
        var copy = Instantiate(S_Dash, this.transform.position, this.transform.rotation);
        //���� ������ �ı��� �ƴ�, ����� ������Ʈ �ı�
        Destroy(copy, 1.5f);
    }
    #endregion


    void AnimationAllOff()
    {
        animer.SetBool("Run", false);
        animer.SetBool("Jump", false);
        animer.SetBool("Dash", false);
        animer.SetBool("Attack", false);
    }
}
