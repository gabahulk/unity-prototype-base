using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBehavior : MonoBehaviour
{
    public Transform canonBallPos;
    public GameObject canonBallPrefab;
    public float speed = 1;
    public int initialNumberOfPooledInstance = 10;

    private ObjectPooler pool;

    private void Awake()
    {
        pool = new ObjectPooler(initialNumberOfPooledInstance, canonBallPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = pool.GetObject();
        bullet.transform.position = canonBallPos.position;
        bullet.GetComponent<CanonBallBehavior>().Fire();
    }
}
