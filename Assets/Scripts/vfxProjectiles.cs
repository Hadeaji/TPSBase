using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxProjectiles : MonoBehaviour
{
    public float speed;
    [SerializeField] private Transform vfxDestroyEffect;
    public float damage;
    public Vector3 deathPosition;
    private float distanceBetween;
    public Transform targetTransform;

    private void Start()
    {
        distanceBetween = Vector3.Distance(gameObject.transform.position, deathPosition);
    }
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        CheckDeath();
    }
    void CheckDeath()
    {
        Vector3 a = gameObject.transform.position;
        Vector3 b = deathPosition;
        float c = Vector3.Distance(a, b);
        if (c > distanceBetween)
        {
            DoDamage();
            Destroy(gameObject);
        }
        else
        {
            distanceBetween = c;
        }

    }

    void DoDamage()
    {
        if (targetTransform != null)
        {
            //Debug.Log(targetTransform.tag);
            var targetScript = targetTransform.GetComponent<Target>();
            if (targetScript != null)
            {
                // Hit target
                targetScript.TakeDamage(damage);
            }
        }
    }
}
