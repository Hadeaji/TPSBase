using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] private float fogDamage = 20;
    // Update is called once per frame

    private float damageTimer;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;
        damageTimer += Time.deltaTime;
        if (damageTimer > 1f)
        {

            other.GetComponent<Stats>().TakeDamage(fogDamage);

            damageTimer = 0f;
        }
    }

}
