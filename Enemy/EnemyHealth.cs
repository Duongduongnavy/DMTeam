using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Image healthBarE;
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        healthBarE.fillAmount = Mathf.Clamp((float)health / maxHealth, 0, 1);
    }
}
