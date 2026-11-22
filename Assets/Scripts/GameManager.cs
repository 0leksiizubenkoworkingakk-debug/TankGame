using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UI Ui;
    
    [Header("Player tank")]
    [SerializeField] private GameObject Player;
    
    [Header("Enemyes tanks")]
    [SerializeField] private List<GameObject> Enemyes;
    
    
    
    
    private IDamageable PlayerTank;
    private List<IDamageable> EnemyesTanks = new List<IDamageable>();
    private int enemyTankDestroyCounter = 0;
    void Start()
    {
        Ui.gameObject.SetActive(false);

        PlayerTank = Player.GetComponent<IDamageable>();
        PlayerTank.Died += DestroyMyTank;

        for (int i = 0; i < Enemyes.Count; i++)
        {
            EnemyesTanks.Add(Enemyes[i].GetComponent<IDamageable>());
            EnemyesTanks[i].Died += DestroyEnemyTank;
        }
        
        
    }

    private void OnDestroy()
    {
        
        PlayerTank.Died -= DestroyMyTank;

        for (int i = 0; i < Enemyes.Count; i++)
        {
           
            EnemyesTanks[i].Died -= DestroyEnemyTank;
        }
    }


 private void DestroyMyTank()
{
    Ui.gameObject.SetActive(true);
    
    if (enemyTankDestroyCounter < Enemyes.Count) return;
    Ui.GameLoose();
}

private void DestroyEnemyTank()
{
    Ui.gameObject.SetActive(true);
    Ui.GameWin();
}
}