using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [SerializeField] private GameObject bulletPrefab;
    private Stack<GameObject> pool;
    // Start is called before the first frame update
    void Start()
    {
        pool = new Stack<GameObject>();
    }

    public GameObject GetObject()
    {
        if(pool.Count > 0)
        {
            GameObject obj = pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
        return Instantiate(bulletPrefab);
    }

    public void ReturnObject(GameObject obj)
    {
        obj.GetComponent<TrailRenderer>().Clear();
        obj.GetComponent<Bullet>().SetSpeed(0);
        if (pool.Count >= maxCapacity)
        {
            Destroy(obj.gameObject);
            return;
        }
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}