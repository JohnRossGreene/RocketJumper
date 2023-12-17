using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public bool destroyOnHit;

    [Header("Effects")]
    public GameObject muzzleEffect;
    public GameObject hitEffect;

    [Header("Explosive Projectile")]
    public bool isExplosive;
    public float explosionRadius;
    public float explosionForce;
    public int explosionDamage;
    public GameObject explosionEffect;

    private Rigidbody rb;
    private bool hitTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(muzzleEffect != null)
        {
            Instantiate(muzzleEffect, transform.position, Quaternion.identity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hitTarget)
        {
            return;
        }
        else
        {
            hitTarget = true;
        }
    }

}