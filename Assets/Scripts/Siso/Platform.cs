using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isPlaying = true;

    public float rotatioSpeed = 1f;
    public float turnBackSpeed = 0.02f;

    private Vector3 origin = new Vector3(0.0f, 0.0f, 180.0f);

    private void Start()
    {
        transform.eulerAngles = origin;
    }

    private void FixedUpdate()
    {
        if (isPlaying)
        {
            Rotate();
            TurnBack();
        }
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void Resume()
    {
        isPlaying = true;
    }

    void Rotate()
    {
        float input = Input.GetAxis("Horizontal");
        
        if(transform.eulerAngles.z >= 120.0f && transform.eulerAngles.z < 240.0f)
            transform.Rotate(Vector3.back * rotatioSpeed * input);
    }

    void TurnBack()
    {
        Vector3 currentRotatioin = transform.eulerAngles;

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, origin, turnBackSpeed);
    }
}
