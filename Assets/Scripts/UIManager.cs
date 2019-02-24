using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    
    public Image[] UIHearts = new Image[0];
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Image[] slots = new Image[0];

    public GameObject GameOverButton;

    // max health == 20
    [Min(1)]
    public int health;
    public int totalHearts;
    

    #region Game Setup

    private void Awake()
    {
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void Start()
    {
        GameOverButton.SetActive(false);
        Time.timeScale = 1;
        DisplayHealth();
        SubscribeToEvents();
    }

    private void Update()
    {
    }
    #endregion

    #region Player Health
    public void UpdateHealthAndHearts(int health, int totalHearts)
    {
        this.health = health;
        this.totalHearts = totalHearts;

        DisplayHealth();

        if(health <= 0)
        {

            GameOverButton.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void DisplayHealth()
    {
        bool notSet = false;
        for (int aux = 0; aux < UIHearts.Length; aux++)
        {
            var life = (aux + 1) * 10;


            if (life <= health)
                UIHearts[aux].sprite = fullHeart;
            else if (health > 0 && health % 10 != 0 && !notSet)
            {
                notSet = true;
                UIHearts[aux].sprite = halfHeart;
            }
            else
                UIHearts[aux].sprite = emptyHeart;


            if (aux < totalHearts)
                UIHearts[aux].enabled = true;
            else
                UIHearts[aux].enabled = false;
        }
    }

    #endregion

    #region Game Flow

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    #endregion

    #region Game Events

    public void SubscribeToEvents()
    {
        
    }

    private void WeaponPickup(object sender, PickupEventArgs e)
    {
        
    }

    private void ItemPickup(object sender, PickupEventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion

}
