using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VIEWMODE { ONE, TWO, FOUR}

public class LayoutController : MonoBehaviour
{
    private List<Camera> cameras;
    private VIEWMODE viewMode;

    Camera pongCam;
    Camera snakeCam;
    Camera sisoCam;
    Camera pPRCam;

    const float MARGIN = 0.03f;
    const float ZERO = 0.0f;
    const float QUARTER = 0.25f;
    const float HALF = 0.5f;
    const float FULL = 1.0f;

    const VIEWMODE ONE = VIEWMODE.ONE;
    const VIEWMODE TWO = VIEWMODE.TWO;
    const VIEWMODE FOUR = VIEWMODE.FOUR;

    GAME_TITLE Pong = GAME_TITLE.PONG;
    GAME_TITLE Snake = GAME_TITLE.SNAKE;
    GAME_TITLE Siso = GAME_TITLE.SISO;
    GAME_TITLE Ppr = GAME_TITLE.PPR;

    public void Initialize()
    {
        cameras = new List<Camera>();

        //pongCam = GameManager.instance.gameDic[Pong].GetCamera();
        snakeCam = GameManager.instance.gameDic[Snake].GetCamera();
        sisoCam = GameManager.instance.gameDic[Siso].GetCamera();
        pPRCam = GameManager.instance.gameDic[Ppr].GetCamera();

        //cameras.Add(pongCam);
        cameras.Add(snakeCam);
        cameras.Add(sisoCam);
        cameras.Add(pPRCam);

        SetViewMode(ONE);
    }

    Rect FullScreen = new Rect(MARGIN * HALF, MARGIN * HALF, FULL - MARGIN, FULL - MARGIN);

    Rect FistOnHalf = new Rect(MARGIN * HALF, QUARTER, HALF - MARGIN, HALF - MARGIN);
    Rect SecondOnHalf = new Rect(HALF + MARGIN* HALF, QUARTER , HALF - MARGIN, HALF - MARGIN);

    public void SetFeaturedGames(GAME_TITLE _target)
    {
        SwapToIndex0(GameManager.instance.gameDic[_target].GetCamera());

        SetViewMode(ONE);
    }

    public void SetFeaturedGames(GAME_TITLE _target, GAME_TITLE _target2)
    {
        SwapToIndex0(GameManager.instance.gameDic[_target2].GetCamera());
        SwapToIndex0(GameManager.instance.gameDic[_target].GetCamera());

        SetViewMode(TWO);
    }

    public void SetFeaturedGames(GAME_TITLE _target, GAME_TITLE _target2, GAME_TITLE _target3, GAME_TITLE _target4)
    {
        SwapToIndex0(GameManager.instance.gameDic[_target4].GetCamera());
        SwapToIndex0(GameManager.instance.gameDic[_target3].GetCamera());
        SwapToIndex0(GameManager.instance.gameDic[_target2].GetCamera());
        SwapToIndex0(GameManager.instance.gameDic[_target].GetCamera());

        SetViewMode(FOUR);
    }

    public void SwapToIndex0(Camera cam)
    {
        cameras.Remove(cam);
        cameras.Insert(0, cam);
    }

    void SetViewMode(VIEWMODE _viewMode)
    {     
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }

        switch (_viewMode)
        {
            case ONE:
                cameras[0].rect = FullScreen;
                cameras[0].enabled = true;

                break;

            case TWO:
                cameras[0].rect = FistOnHalf;
                cameras[1].rect = SecondOnHalf;
                cameras[0].enabled = true;
                cameras[1].enabled = true;

                break;

            case FOUR:


                break;
            
            default:
                break;
        }


        viewMode = _viewMode;
    }
}
