using UnityEngine;

public class Knife : MonoBehaviour
{
    GameObject Player;
    sbyte Face;
    // Start is called before the first frame update
    void Start()
    {
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
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0f, 1) * Time.deltaTime * 20f * Face);

    }
}
