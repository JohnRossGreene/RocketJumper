using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    public Transform head;
    public Transform feet;
    float playerHeight = 1.2f;
    public bool isGrounded;


    public Collider player;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(player, GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight);
        gameObject.transform.position = new Vector3(head.position.x, feet.position.y, head.position.z);
    }
}
