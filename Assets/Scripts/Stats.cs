using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 500f;
    private float qCD = 0f;
    private float rCD = 0f;

    public void TakeDamage(float damageTaken)
    {
        hp -= damageTaken;
        Debug.Log(hp);
    }

    private void Update()
    {
        isDead();
    }

    private void isDead()
    {
        if (hp <= 0f)
        {
            Debug.Log("Play Dead");
        }

    }
}
