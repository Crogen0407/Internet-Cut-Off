using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_shyMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -Vector3.right * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * 5;
        }
    }
}
