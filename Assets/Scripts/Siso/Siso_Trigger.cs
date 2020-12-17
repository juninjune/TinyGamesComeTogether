using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siso_Trigger : MonoBehaviour
{
    Siso siso;

    private void Awake()
    {
        siso = FindObjectOfType<Siso>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            siso.ScoreUp();
            siso.PlayNextScene();
            siso.SpawnScore();

            gameObject.SetActive(false);
        }
    }
}
