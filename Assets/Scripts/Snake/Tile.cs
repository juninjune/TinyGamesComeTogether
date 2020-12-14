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

    Color Black = Color.black;
    Color White = Color.white;
    Color Red = Color.red;

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
                myRenderer.material.color = Black;
                break;

            case TILE_STATE.BODY:
                myRenderer.material.color = White;
                break;

            case TILE_STATE.SCORE:
                myRenderer.material.color = Red;
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
