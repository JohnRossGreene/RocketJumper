using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{

    public Transform toFollow;
    public Collider col;
    // Start is called before the first frame update
    void Start()
    {
        // col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       col.transform.position=toFollow.position;

    }
}
