using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed;
    Vector3 direction;
    Rigidbody rb;
    private BulletPool pool;
    private Vector3 startPos;
    private float maxDistance;
    private ParticleSystem particles;
    MeshRenderer rend;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particles = GetComponent<ParticleSystem>();
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        rb.velocity = speed * direction.normalized;
        if (Vector3.Distance(transform.position, startPos) > maxDistance)
        {
            pool.ReturnObject(gameObject);
        }
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void SetDirection(Vector3 value)
    {
        direction = value;
    }

    public void SetPool(BulletPool value)
    {
        pool = value;
    }

    public void SetStartPos()
    {
        startPos = transform.position;
    }

    public void SetMaxDistance(float value)
    {
        maxDistance = value;
    }


    private void OnCollisionEnter(Collision collision)
    {

        particles.Emit(100);
        rend.enabled = false;
        speed = 0;
        StartCoroutine(WaitParticules());
    }

    IEnumerator WaitParticules()
    {
        while (particles.IsAlive())
        {
            yield return null;
        }
        rend.enabled = true;
        pool.ReturnObject(gameObject);
    }
}
