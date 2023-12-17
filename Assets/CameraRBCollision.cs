using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRBCollision : MonoBehaviour
{
    public Transform head;
    public Transform feet;

    public Collider bodyCol;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(bodyCol, GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(head.position.x, feet.position.y, head.position.z);
    }
}
