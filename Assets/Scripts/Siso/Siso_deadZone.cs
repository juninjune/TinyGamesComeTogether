using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siso_deadZone : MonoBehaviour
{
    Siso siso;

    private void Start()
    {
        siso = FindObjectOfType<Siso>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            siso.GameOver();
    }
}
