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
            transform.localScale = new Vector3(-1, 1, 1);//방향전환
        }
        else if (Face == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);//방향원래
        }
        ri.AddForce(Vector3.right * Face * Power, ForceMode2D.Impulse);
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
