using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Player you;
    void Start()
    {
        currentHealth = maxHealth;

        you = FindObjectOfType<Player>();
    }


    void Update()
    {

    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        currentHealth -= damage;
        you.KnockBack(direction);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }
}