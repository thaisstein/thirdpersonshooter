using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] float shotsPerSecond;
    [SerializeField] float bulletSpeed;
    [SerializeField] BulletPool pool;
    [SerializeField] Transform muzzle;
    [SerializeField] float bulletMaxDistance = 200;
    [SerializeField] GameObject flashPrefab;
    [SerializeField] float flashDuration = 0.05f;
    StarterAssets.StarterAssetsInputs input;
    AudioSource audioSound;


    Camera cam;
    float t;
    void Start()
    {
        cam = Camera.main;
        input = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
        audioSound = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (input.aim && input.shoot)
        {
            if (t > 1 / shotsPerSecond)
            {
                StartCoroutine(Flash());
                GameObject bullet = pool.GetObject();
                bullet.transform.position = muzzle.position;
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetDirection(cam.transform.forward);
                bulletScript.SetSpeed(bulletSpeed);
                bulletScript.SetPool(pool);
                bulletScript.SetMaxDistance(bulletMaxDistance);
                bulletScript.SetStartPos();
                audioSound.Play();
                t = 0;
            }
            t += Time.deltaTime;
        }
    }
    IEnumerator Flash()
    {
        flashPrefab.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        flashPrefab.SetActive(false);
    }
}
