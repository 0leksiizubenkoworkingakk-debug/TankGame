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

    private int enemyTankDestroyCounter = 0;


    private IDamageable PlayerTank;
    private List<IDamageable> EnemyesTanks = new List<IDamageable>();

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
        Ui.GameLoose();
    }

    private void DestroyEnemyTank()
    {
        enemyTankDestroyCounter++;

        if (enemyTankDestroyCounter < Enemyes.Count) return;

        Ui.gameObject.SetActive(true);
        Ui.GameWin();
    }

}