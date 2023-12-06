using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("대충암거나처넣기")]
    Rigidbody2D ri;
    Animator animer;

    [Header("플레이어 기본셋팅")]
    [SerializeField] float PlayerHP;
    [SerializeField] float PlayerSpeed;
    [SerializeField] float JumpScale;
    [SerializeField] bool DDang;

    void Start()
    {
        ri = GetComponent<Rigidbody2D>();
        animer = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Gamji();
        Jump();
    }

    private void Move()
    {
        //이동
        Vector3 vel = transform.right * Input.GetAxisRaw("Horizontal") * PlayerSpeed * 1;
        vel.y = ri.velocity.y;
        ri.velocity = vel;
        //방향전환과 전환애니메이션
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);//방향전환
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);//방향원래
        }
        if (Input.GetAxisRaw("Horizontal") == 0)//애니메이숀
        {
            animer.SetBool("Run", false);
        }
        else
        {
            animer.SetBool("Run", true);
        }
    }
    private void Gamji()
    {
        //Debug.DrawRay(ri.position, Vector3.down, new Color(0,1,0));
        RaycastHit2D RaySir = Physics2D.Raycast(ri.position, Vector3.down, 1, LayerMask.GetMask("platform"));
        if (ri.velocity.y < 0)
        {
            if (RaySir.collider != null)
            {
                if (RaySir.distance < 1.0f)
                {
                    animer.SetBool("Jump", false);
                    //DDang = false;
                }
            }
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && !DDang)
        {
            animer.SetBool("Jump", true);
            ri.AddForce(Vector3.up * JumpScale, ForceMode2D.Impulse);
            //DDang = true;
        }
    }

    void KnifeAttack()
    {

    }
    void ThrowAttack()
    {

    }
}
