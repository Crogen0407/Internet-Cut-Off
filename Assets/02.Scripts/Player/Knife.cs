using System;
using UnityEngine;

public class Knife : MonoBehaviour
{
    GameObject Player;
    Rigidbody2D ri;
    [SerializeField] float Power;
    sbyte Face;
    // Start is called before the first frame update
    void Start()
    {
        ri = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        Face = Player.GetComponent<Player>().Face;
        if (Face == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);//������ȯ
        }
        else if (Face == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);//�������
        }
        ri.AddForce(Vector3.right * Face * Power, ForceMode2D.Impulse);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject triggerObject = other.gameObject;
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (triggerObject.CompareTag("Enemy") || triggerObject.CompareTag("Untagged"))
        {
            if (healthSystem != null)
            {
                healthSystem.Hp -= 10;
            }
            Destroy(gameObject);
        }
    }
}
