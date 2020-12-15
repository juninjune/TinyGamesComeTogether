using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siso_score : MonoBehaviour
{
    private Siso siso;
    private Renderer renderer;

    private bool isActivated = false;

    private void Awake()
    {
        siso = FindObjectOfType<Siso>();
        renderer = GetComponent<Renderer>();

        Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActivated)
        {
            siso.ScoreUp();
            siso.SpawnScore();

            Disable();
        }
    }

    public void Enable()
    {
        renderer.enabled = true;

        isActivated = true;
    }

    public void Disable()
    {
        renderer.enabled = false;

        isActivated = false;
    }

    public bool GetIsActivated()
    {
        return isActivated;
    }
}
