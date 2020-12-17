using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PPR_Player : MonoBehaviour
{
    bool isRun = false;

    Animator playerAnimator;

    public void Initialize()
    {
        playerAnimator = GetComponent<Animator>();
        PlayIdleClip();
    }

    public void Pause()
    {
        playerAnimator.speed = 0.0f;
    }

    public void Resume()
    {

    }

    public void PlayRunClip()
    {
        if (isRun)
            return;

        playerAnimator.Play("WoodCutter_run");
        isRun = true;
    }

    public void PlayIdleClip()
    {
        playerAnimator.Play("WoodCutter_idle");
        playerAnimator.speed = 0.33f;
        isRun = false;
    }

    public void UpdateRunSpeed(float _input)
    {
        if (!isRun)
            return;

        playerAnimator.speed = _input;
    }

}
