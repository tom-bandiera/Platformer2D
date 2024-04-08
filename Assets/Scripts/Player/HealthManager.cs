using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;
    public Image[] hearts;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private GameManager gameManager;

    private bool isDead = false;

    private void Awake()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var heart in hearts)
        {
            heart.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && isDead == false)
        {
            isDead = true;
            gameManager.GameOver();
        }
    }
}
