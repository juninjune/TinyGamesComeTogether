using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;

    Vector2 myVelocity;
    float myAngularVelocity;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Pause()
    {
        SaveDynamics();
        myRigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    public void Resume()
    {
        myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        LoadDynamics();
    }

    void SaveDynamics()
    {
        myVelocity = myRigidbody2D.velocity;
    }

    void LoadDynamics()
    {
        myRigidbody2D.velocity = myVelocity;
        myRigidbody2D.angularVelocity = myAngularVelocity;
    }
}
