using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Proj : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public float minFalloff;
    [Header("Explosion Effect Settings [DEBUG]")]
    public GameObject outerSphere;
    public GameObject midSphere;
    public GameObject innerSphere;
    public float timeout = 3f;
    private bool firstCollisionOccured;
    public GameObject explosionEffect;
    public GameObject smokeEffect;

//TODO make launcher and rocket ignore player, but maintain overlapsphere
        //check if overlapsphere instantiation inherits Physics.ignorecollision
//Bitch nvm that was easy TODO something else

    private void Start()
{
    //Ignoring rocket collision with player collider and rocket launcher
    GameObject playercol = GameObject.FindGameObjectWithTag("PlayerCol");
    GameObject launcher = GameObject.FindGameObjectWithTag("Launcher");
    Physics.IgnoreCollision(playercol.GetComponent<Collider>(), GetComponent<Collider>());
    Physics.IgnoreCollision(launcher.GetComponent<Collider>(), GetComponent<Collider>());
    // Instantiate(smokeEffect, transform.position, transform.rotation);

}
    void OnCollisionEnter(Collision collision)
    {
        //TODO Add particle system for launch

        // if (!firstCollisionOccured)
        // {
        //     firstCollisionOccured = true;
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     GameObject playercol = GameObject.FindGameObjectWithTag("PlayerCol");
        //     GameObject launcher = GameObject.FindGameObjectWithTag("Launcher");
        //     Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        //     Physics.IgnoreCollision(playercol.GetComponent<Collider>(), GetComponent<Collider>());
        //     Physics.IgnoreCollision(launcher.GetComponent<Collider>(), GetComponent<Collider>());
        // }      

        Explode();
    }
    void OnCollisionExit(Collision collision)
    {
        //TODO Add particle system for launch

        // if (firstCollisionOccured)
        // {
        //     firstCollisionOccured = false;
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     GameObject playercol = GameObject.FindGameObjectWithTag("PlayerCol");
        //     GameObject launcher = GameObject.FindGameObjectWithTag("Launcher");
        //     Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        //     Physics.IgnoreCollision(playercol.GetComponent<Collider>(), GetComponent<Collider>(), false);
        //     Physics.IgnoreCollision(launcher.GetComponent<Collider>(), GetComponent<Collider>(), false);
        // }

        Explode();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


    void Explode()
    {
        //create explosion
            //TODO add explosion effect
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        //Get all colliders within overlapsphere
        foreach (Collider hitCollider in colliders)
        {
                //Bad name but you get the idea
                Rigidbody playerRb = hitCollider.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    //Get distance from center of sphere to get amount of force to apply
                    Vector3 explosionDir = playerRb.transform.position - transform.position;
                    float distance = explosionDir.magnitude;
                    float explosionStrength;
                    if (distance < minFalloff)
                    {
                        explosionStrength = .85f;
                        Debug.Log(explosionStrength);
                    }
                    else{
                        explosionStrength = 1f - (distance / explosionRadius);
                        explosionStrength = Mathf.Clamp(explosionStrength, 0f, 10f);
                        Debug.Log(explosionStrength);

                    }

                    // Debug.Log(explosionStrength);

                    //creating debug explosions part 2
                    // GameObject explosion1 = Instantiate (outerSphere, transform.position, transform.rotation);
                    // explosion1.transform.localScale = new Vector3(explosionRadius*2, explosionRadius*2, explosionRadius*2);
                    // GameObject explosion2 = Instantiate (midSphere, transform.position, transform.rotation);
                    // explosion2.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
                    // GameObject explosion3 = Instantiate (innerSphere, transform.position, transform.rotation);
                    // explosion3.transform.localScale = new Vector3(explosionRadius/2, explosionRadius/2, explosionRadius/2);


                    explosionDir = new Vector3(explosionDir.normalized.x,(explosionDir.normalized.y + explosionForce/50),explosionDir.normalized.z);
                    playerRb.AddForce(explosionDir * explosionForce * explosionStrength, ForceMode.Impulse);
                    if (playerRb.gameObject.tag == "Player")
                    {
                        Debug.Log ("Rocket JUMP!");
                    }
                    // Destroy(explosion1, timeout);
                    // Destroy(explosion2, timeout+0.25f);
                    // Destroy(explosion3, timeout+.5f);
                    // StartCoroutine(DestroySphereAfterDelay(explosion1, timeout));
                    // StartCoroutine(DestroySphereAfterDelay(explosion2, timeout+0.5f));
                    // StartCoroutine(DestroySphereAfterDelay(explosion3, timeout+1f));
                }


        Instantiate(explosionEffect, transform.position, transform.rotation);
        MeshRenderer render = gameObject.GetComponent<MeshRenderer>();
        render.enabled = false;
        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.Play();
        Rigidbody selfRB = gameObject.GetComponent<Rigidbody>();
        selfRB.isKinematic = true;
        selfRB.detectCollisions = false;
        Destroy(gameObject,3);
    }
//     IEnumerator DestroySphereAfterDelay(GameObject sphere, float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         Debug.Log("Destroying sphere after delay: " + delay);
//         Destroy(sphere.gameObject);
//     }
}
}