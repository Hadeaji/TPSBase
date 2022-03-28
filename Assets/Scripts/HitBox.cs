using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private GameObject parentBody;
    [SerializeField] private float boxDamage = 40f;
    private bool attackingState;

    // Update is called once per frame
    void Update()
    {
        UpdateAttackState();
    }

    private void UpdateAttackState()
    {
        attackingState = parentBody.GetComponent<NavMesh>().isAttacking;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!attackingState) return;
        if (other.tag != "Player") return;
        //reset state
        attackingState = false;
        parentBody.GetComponent<NavMesh>().isAttacking = false;

        // Do damage to player 
        other.GetComponent<Stats>().TakeDamage(boxDamage);


        Debug.Log("On Trigger" + other.tag);

    }
}
