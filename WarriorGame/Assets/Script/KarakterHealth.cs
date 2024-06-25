using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterHealth : MonoBehaviour
{
    //Health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    //EnemySpacing
    public bool enemyattack;
    public float enemytimer;

    void Start()
    {
        currentHealth = maxHealth;
        enemytimer = 1.5f;
    }
    //Düþmanýn bize zarar verme aralýðý
    void EnemyAttackSpacing()
    {
        if (enemyattack==false)
        {
            enemytimer -= Time.deltaTime;
        }
        if (enemytimer<0)
        {
            enemytimer = 0f;
        }
        if(enemytimer==0)
        {
            enemyattack = true;
            enemytimer = 1.5f;
        }

    }
    //Düþmaný Kilitleme 
    void CharacterDamage()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            enemyattack = false;
        }
    }

    //Karakter Zarar Görme
    public void TakeDamage(int damage)
    {
        if (enemyattack)
        {
            currentHealth -= 20;
            enemyattack = false;
        }
        healthBar.SetHealth(currentHealth);
    }

    void Update()
    {
        EnemyAttackSpacing();
        CharacterDamage();

        if(Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20);
        }
    }
}
