using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float invincibilityWindow = 1;
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool invincible;
    private bool dead;
    
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        if (invincible) return; // don't take damage when invincible
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            StartCoroutine("Invincibility");
        }
        else
        {
            if (!dead)
            {
                GameObject player = GameObject.FindWithTag("Player");
                GameObject gameController = GameObject.FindWithTag("GameController");
                gameController.GetComponent<GameController>().GameOver();
                Destroy(player.gameObject);
                dead = true;
            }

        }
    }

    public void SetInvincibility(bool invincibility)
    {
        invincible = invincibility;
    }

    private IEnumerator Invincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityWindow);
        invincible = false;
    }
}
