using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPR_Map : MonoBehaviour
{
    Rigidbody2D rigid;

    float speed_save = 0.0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Run(float _input)
    {
        rigid.AddForce(Vector2.left * _input);
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }

    void Save()
    {
        speed_save = rigid.velocity.x;
    }

    void Load()
    {
        rigid.velocity = Vector2.right * speed_save;
    }

    public float GetSpeed()
    {
        return rigid.velocity.x;
    }
}
