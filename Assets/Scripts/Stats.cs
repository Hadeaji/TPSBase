using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float maxHp = 500f;
    [SerializeField] private float hp;
    private float qCD = 0f;
    private float rCD = 0f;

    public HpBar healthBar;

    private void Start()
    {
        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);
    }
    public void TakeDamage(float damageTaken)
    {
        hp -= damageTaken;
        healthBar.SetHealth(hp);

        // cam force
        CinemachineShake.Instance.ShakeCamera(5f, 0.3f);
    }

    private void Update()
    {
        isDead();
    }

    private void isDead()
    {
        if (hp <= 0f)
        {
            GameManager.Instance.EndGame();
        }
    }
}
