using System;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    Animator animer;

    bool canSwing = true;
    bool canThrow = true;

    private StageController _stageController;
    
    [Header("������Ʈ")]
    [SerializeField] GameObject Knife;
    /*[SerializeField] GameObject SwingArea;//�������ݹ���������Ʈ, �ݶ��̴���.*/
    [SerializeField] GameObject KnifePos;
    [SerializeField] GameObject SwingPos;//�������ݿ�

    [Header("�÷��̾� ���ݻ���")]
    [SerializeField] GameObject S_Swing;
    [SerializeField] GameObject S_Throw;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    void Start()
    { 
        animer = GetComponent<Animator>();
        _stageController = GameManager.Instance.StageController;
    }

    void Update()
    {
        if (_player.isCutScene == false)
        {
            InputSys();
        }
    }

    void InputSys()
    {
        if (Input.GetKeyDown(KeyCode.D) && _stageController.OnRealWorld == false)
        {
            KnifeAttack();
        }
        if (Input.GetKeyDown(KeyCode.F) && _stageController.OnRealWorld == false)
        {
            ThrowAttack();
        }
    }

    public void KnifeAttack()
    {
        if(canSwing == true)
        {
            AnimationAllOff();
            animer.SetBool("Attack", true);
            Sound_Swing();
            canSwing = false;
            /*//���������� �ı� ������ ���� ����
            var copy = Instantiate(SwingArea, SwingPos.transform.position, SwingPos.transform.rotation);
            //���� ������ �ı��� �ƴ�, ����� ������Ʈ �ı�
            Destroy(copy, 0.5f);*/
            Invoke("AfterKnifeAttack", 0.25f);
            Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position + new Vector3(_player.Face * 1.5f, 0), 1.2f);
            foreach (var item in coll)
            {
                HealthSystem itemHealth = item.GetComponent<HealthSystem>();
                if (itemHealth != null && item.CompareTag("Enemy"))
                {
                    itemHealth.Hp -= 10;
                }
            }
        }
    }
    void AfterKnifeAttack()
    {
        animer.SetBool("Attack", false);
        canSwing = true;
    }

    void ThrowAttack()
    {
        if (canThrow == true)
        {
            AnimationAllOff();
            animer.SetBool("Attack", true);
            Sound_Throw();
            canThrow = false;
            print("throw");
            Instantiate(Knife, KnifePos.transform.position, KnifePos.transform.rotation);
            Invoke("AnimaionOff_ThrowAttack", 0.5f);
            Invoke("AfterThrowAttack", 1f);
            
        }
    }
    void AnimaionOff_ThrowAttack()
    {
        animer.SetBool("Attack", false);
    }
    void AfterThrowAttack()
    {
        animer.SetBool("Attack", false);
        canThrow = true;
    }

#region ����
    void Sound_Swing()
    {
        //���������� �ı� ������ ���� ����
        var copy = Instantiate(S_Swing, this.transform.position, this.transform.rotation);
        //���� ������ �ı��� �ƴ�, ����� ������Ʈ �ı�
        Destroy(copy, 1.5f);
    }
    void Sound_Throw()
    {
        //���������� �ı� ������ ���� ����
        var copy = Instantiate(S_Throw, this.transform.position, this.transform.rotation);
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
