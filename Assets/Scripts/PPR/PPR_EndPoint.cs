using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPR_EndPoint : MonoBehaviour
{
    PPR ppr;

    private void Start()
    {
        ppr = FindObjectOfType<PPR>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("충돌함.");

        if (collision.CompareTag("Player"))
        {
            ppr.SwapGame();
        }
    }
}
