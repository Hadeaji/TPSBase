using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSp : MonoBehaviour
{
    [SerializeField] public Transform moveTransform;
    [SerializeField] public GameObject fog;
    private NavMeshAgent navMeshAgent;

    private Material[] childMats;
    private bool isDead;
    private float timer = 0f;
    private float deathTimer = 30f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        GameObject child = gameObject.transform.GetChild(0).gameObject;
        Renderer ma = child.GetComponent<Renderer>();
        childMats = ma.materials;

        foreach (Material mat in childMats)
        {
            mat.SetFloat("_EmissiveExposureWeight", 0.5f);
        }
    }

    private void Update()
    {
        navMeshAgent.destination = moveTransform.position;
        CheckAttack();
        AnimateDeath();
    }

    private void CheckAttack()
    {
        // Check if we've reached the destination
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    isDead = true;
                }
            }
        }
    }


    void AnimateDeath()
    {

        deathTimer -= Time.deltaTime;
        if(deathTimer < 0f)
        {
            isDead = true;
        }
        if (isDead == true)
        {

            timer += (Time.deltaTime * 2.5f);
            foreach (Material mat in childMats)
            {
                mat.SetFloat("_Dissolve", timer);
            }
        }
        if (timer >= 0.7f)
        {
            GameManager.Instance.mobsCount -= 1;

            isDead = false;
            timer = 0f;

            // Instantiate fog
            Vector3 pos = gameObject.transform.position;
            pos.y = gameObject.transform.position.y + 1.7f;

            Instantiate(fog, pos, Quaternion.identity);

            Destroy(gameObject);
        }
    }

}
