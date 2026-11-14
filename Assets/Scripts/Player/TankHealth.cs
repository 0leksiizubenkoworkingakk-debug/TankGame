using DefaultNamespace;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField, Min(1)] private float maxHealth = 100;
    [SerializeField] private float currentHealth;


    [SerializeField] private Image healthImage;
    [SerializeField] private Color healthGood;
    [SerializeField] private Color healthNormal;
    [SerializeField] private Color healthBad;
    public bool IsDead { get; private set; }

    public event Action<float, float> HealthChanged;
    public event Action Died;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        HealthChanged?.Invoke(currentHealth, maxHealth);

        //UpdateUi();
    }
    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        HealthChanged?.Invoke(currentHealth, maxHealth);


        //
        //UpdateUi();
        //
        if (currentHealth == 0)
            Die();
    }
    public void RestoreHealth(int amount)
    {
        if (IsDead || amount <= 0) return;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        HealthChanged?.Invoke(currentHealth, maxHealth);

        //UpdateUi();
    }
    // private void UpdateUi()
    // {
    //     if (healthImage)
    //     {
    //         float result = currentHealth / maxHealth;
    //         if (result >= 0.6F)
    //         {
    //             healthImage.color = healthGood;
    //         }
    //         else if(result >= 0.3F)
    //         {
    //             healthImage.color = healthNormal;
    //         }
    //         else
    //         {
    //             healthImage.color = healthBad;
    //         }
    //         
    //         healthImage.fillAmount = result;
    //     }
    // }
    private void Die()
    {
        if (IsDead) return;
        IsDead = true;
        Died?.Invoke();
    }
}
