using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VIEWMODE { ONE, TWO, FOUR}

public class LayoutController : MonoBehaviour
{
    private Camera[] cameras;
    private VIEWMODE viewMode;

    const float MARGIN = 0.03f;
    const float ZERO = 0.0f;
    const float QUARTER = 0.25f;
    const float HALF = 0.5f;
    const float FULL = 1.0f;

    const VIEWMODE ONE = VIEWMODE.ONE;
    const VIEWMODE TWO = VIEWMODE.TWO;
    const VIEWMODE FOUR = VIEWMODE.FOUR;


    // Start is called before the first frame update
    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }
        SetViewMode(ONE);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Rect FullScreen = new Rect(MARGIN * HALF, MARGIN * HALF, FULL - MARGIN, FULL - MARGIN);

    Rect FistOnHalf = new Rect(MARGIN * HALF, QUARTER, HALF - MARGIN, HALF - MARGIN);
    Rect SecondOnHalf = new Rect(HALF + MARGIN* HALF, QUARTER , HALF - MARGIN, HALF - MARGIN);

    void SetViewMode(VIEWMODE _viewMode)
    {
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
