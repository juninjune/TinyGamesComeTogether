using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE_STATE { EMPTY, BODY, SCORE }

public class Tile : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    private TILE_STATE tileState;

    public Vector2Int Pos { get; set; }


    TILE_STATE empty = TILE_STATE.EMPTY;
    TILE_STATE body = TILE_STATE.BODY;
    TILE_STATE score = TILE_STATE.SCORE;

    Color c_empty = Color.black;
    Color c_body = Color.white;
    Color c_score = Color.red;

    public void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        SetTileState(empty);
    }

    public void SetTileState(TILE_STATE state)
    {
        tileState = state;

        switch (state)
        {
            case TILE_STATE.EMPTY:
                myRenderer.material.color = c_empty;
                break;

            case TILE_STATE.BODY:
                myRenderer.material.color = c_body;
                break;

            case TILE_STATE.SCORE:
                myRenderer.material.color = c_score;
                break;

            default:
                break;
        }
    }

    public TILE_STATE GetTileState()
    {
        return tileState;
    }

    public bool GetIsEmpty()
    {
        return tileState == empty;
    }
}
