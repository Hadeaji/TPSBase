using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 200f;
    public float maxHealth = 200f;
    private Material[] childMats;
    private bool isDead = false;

    private float timeDelay = 1f;
    private float timer = 0f;


    public float radius = 5.0F;
    public float power = 10.0F;
    void Start()
    {
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        Renderer ma = child.GetComponent<Renderer>();
        childMats = ma.materials;
        Debug.Log(childMats);
        foreach (Material mat in childMats)
        {
            mat.SetFloat("_EmissiveExposureWeight", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLife();
        Animate();

        //gameObject.GetComponent<Rigidbody>().useGravity = false;
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 10f, 0);
    }

    void CheckLife()
    {
        if (isDead) return;
        if(health <= 0)
        {
            isDead = true;
        }
    }
    void Animate()
    {
        if (isDead == true)
        {
            timer += Time.deltaTime;
            foreach (Material mat in childMats)
            {
                mat.SetFloat("_Dissolve", timer);
            }
        }
        if (timer >= 0.7f)
        {

            isDead= false;
            timer = 0f;
            Destroy(gameObject);
        }
        //if (timer >= 0.01f)
        //{
        //    //gameObject.GetComponent<Rigidbody>().useGravity = false;
        //    //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 1f, 0);
        //}
    }

}
