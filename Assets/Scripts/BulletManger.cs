using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManger : MonoBehaviour
{

    public GameObject bullet;
    public int MaxBullets;
    private Queue<GameObject> m_bulletPool;
    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    private void _BuildBulletPool()
    {
        //create empty queue structure
        m_bulletPool = new Queue<GameObject>();

        for (int count = 0; count < MaxBullets; count++)
        {
            var tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            tempBullet.transform.parent = transform;//parent of transform is bullet manager
            m_bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position)
    {
        var newBullet = m_bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        return newBullet;
    }

    public void ReturnBullet(GameObject ReturnedBullet)
    {
        ReturnedBullet.SetActive(false);
        m_bulletPool.Enqueue(ReturnedBullet);
    }


}
