using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tweening;

public class Player : MonoBehaviour
{
    [Header("대충암거나처넣기")]
    Rigidbody2D _rigidbody;
    Animator animer;

    [Header("플레이어 기본셋팅")]
    [SerializeField] float PlayerSpeed;
    [SerializeField] float JumpScale;
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    public Material unitMaterial;
    bool DDang= false;
    bool isDasing;
    bool canDash = true;
    bool noRun = false;
    public sbyte Face =1; // 1 R, -1 L
    public bool isCutScene = false;
    public Vector3 developmentVelocity;

    [Header("플레이어 기본사운드")]
    [SerializeField] GameObject S_Jump;
    [SerializeField] GameObject S_Dash;
    private HealthSystem _healthSystem;
    private StageController _stageController;
    private ScreenEffectController _screenEffectController;
    
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        unitMaterial.SetFloat("_Noise", 0);
    }

    private IEnumerator OnNoiseCoroutine()
    {
        transform.Find("w").gameObject.SetActive(false);
        float percent = 0;
        float duration = 0;
        while (percent < 1)
        {
            duration += Time.deltaTime;
            percent = duration / 1f;
            unitMaterial.SetFloat("_Noise", percent);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1);
        _screenEffectController.Fade("_Brightness", 0, 2);
        yield return new WaitForSecondsRealtime(2);
        _stageController.ResetStage(_stageController.CurrentStage);
        _healthSystem.Hp = 30;
    }
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animer = GetComponent<Animator>();
        _stageController = GameManager.Instance.StageController;
        _screenEffectController = GameManager.Instance.ScreenEffectController;

        if (_healthSystem != null)
        {
            _healthSystem.Damaged += () =>
            {
            
            };
        
            _healthSystem.Dead += () =>
            {
                StartCoroutine(OnNoiseCoroutine());
            };
        }
    }

    void Update()
    {
        WallTamji();
        if (noRun == false)
        {
            animer.SetFloat("Run", Mathf.Abs(_rigidbody.velocity.x));
        }
        Move();
        if (isCutScene == false)
        {
            JumpGamji();
            Jump();
            Dash();
        }
        SetFace();
        
    }

#region 움직임
    private void Move()
    {
        //이동
        if (isDasing == false) 
        { 
            Vector3 vel = new Vector3(1,0,0) * (isCutScene == false ? (Input.GetAxisRaw("Horizontal")) : 0) * PlayerSpeed * 1;
            vel.y = _rigidbody.velocity.y;
            vel += developmentVelocity;
            _rigidbody.velocity = vel; 
        }
        //Face
        if (isCutScene == false && isDasing == false)
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                Face = -1;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                Face = 1;
            }
        }
    }

    void SetFace()
    {
        if(Face == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);//방향전환
        }
        else if (Face == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);//방향전환
        }
    }
    #endregion

#region 점프관련
    private void JumpGamji()
    {
        Debug.DrawRay(_rigidbody.position + new Vector2(0.2f, 0), Vector3.down, new Color(0,1,0));
        Debug.DrawRay(_rigidbody.position + new Vector2(-0.2f, 0), Vector3.down, new Color(0, 1, 0));
        RaycastHit2D RaySir = Physics2D.Raycast(_rigidbody.position+new Vector2(0.2f,0), Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (_rigidbody.velocity.y < 0)
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
        RaycastHit2D RaySir2 = Physics2D.Raycast(_rigidbody.position + new Vector2(-0.2f, 0), Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (_rigidbody.velocity.y < 0)
        {
            if (RaySir2.collider != null)
            {
                if (RaySir2.distance < 1.0f)
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
            _rigidbody.AddForce(Vector3.up * JumpScale, ForceMode2D.Impulse);
            DDang = true;
        }
    }
    #endregion

#region 대쉬
    // void Dash()
    // {
    //     if (Input.GetKeyDown(KeyCode.S) && isDasing == false && canDash == true && _stageController.OnRealWorld == false)
    //     {
    //         Sound_Dash();
    //         isDasing = true;
    //         animer.SetBool("Dash", true);
    //         ri.AddForce(new Vector3(dashSpeed * Face, 0, 0), ForceMode2D.Impulse);
    //         Invoke("EndDash", dashTime);
    //     }
    // }
    void EndDash()
    {
        isDasing = false;
        canDash = false;
        animer.SetBool("Dash", false);
        Invoke("AfterEndDash", 1f);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.S) && isDasing == false&& canDash == true)
        {
            Sound_Dash();
            isDasing = true;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Face, dashSpeed, LayerMask.GetMask("Player"));
            if (hit.transform != null)
            {
                Debug.Log(hit.transform.name);
                Tweening.Instance.DOMove(_rigidbody, new Vector2(hit.point.x - Face, transform.position.y), 0.5f, EasingType.EaseInQuad);
            }
            else
            {
                Tweening.Instance.DOMove(_rigidbody, new Vector2(transform.position.x + dashSpeed * Face, transform.position.y), 1f, EasingType.EaseInBack);
            }
            animer.SetBool("Dash", true);
            Invoke("EndDash", dashTime);
        }
    }
    void AfterEndDash()
    {
        canDash = true;
    }
    #endregion

#region 감지
    void WallTamji()
    {
        RaycastHit2D RaySist = Physics2D.Raycast(_rigidbody.position + new Vector2(0, -0.5f), Vector3.right * Face, 1f, LayerMask.GetMask("Platform"));
        Debug.DrawRay(_rigidbody.position + new Vector2(0, -0.5f), Vector3.right * Face, new Color(0, 0.4f, 0));
        if (RaySist.collider != null)
        {
                animer.SetBool("Jump", false);
                animer.SetBool("Dash", false);
                animer.SetBool("Attack", false);
            animer.SetFloat("Run", 0);
            /*animer.SetBool("Idle", true);*/
            noRun = true;

        }
        else if(RaySist.collider == null)
        {
            noRun = false;
            //print("sibal");
        }
    }
    #endregion

#region 사운드
    void Sound_Jump()
    {
        //원본프리펩 파괴 방지를 위한 복사
        var copy = Instantiate(S_Jump, this.transform.position, this.transform.rotation);
        //원본 프리펩 파괴가 아닌, 복사된 오브젝트 파괴
        Destroy(copy, 1.5f);
    }
    void Sound_Dash()
    {
        //원본프리펩 파괴 방지를 위한 복사
        var copy = Instantiate(S_Dash, this.transform.position, this.transform.rotation);
        //원본 프리펩 파괴가 아닌, 복사된 오브젝트 파괴
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
    
    public void SetPlayerCutSceneMode(bool parameter)
    {
        isCutScene = parameter;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.right * Face * dashSpeed, Color.blue);
    }
}
