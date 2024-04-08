using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SlimeGirlfriend : Core
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private LoveState loveState;

    [SerializeField] private GameManager gameManager;

    private bool reachedByPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        Set(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        SelectState();
    }

    void SelectState()
    {
        if (reachedByPlayer)
        {
            Set(loveState);
        } else
        {
            Set(idleState);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            reachedByPlayer = true;
            StartCoroutine(ShowLevelFinishedScreen());
        }
    }

    IEnumerator ShowLevelFinishedScreen()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(2);

        gameManager.LevelFinished();
    }
}

