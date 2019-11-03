using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallBehavior : MonoBehaviour
{
    public float bulletSpeed;
    public float timeToDie;

    private bool isFired;

    // Update is called once per frame
    void Update()
    {
        if (isFired)
        {
            this.transform.Translate(new Vector3(0, Time.deltaTime * bulletSpeed));
        }
    }

    public void Fire()
    {
        isFired = true;
        StartCoroutine("Deactivate");
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(timeToDie);
        isFired = false;
        this.gameObject.SetActive(false);
    }
}
