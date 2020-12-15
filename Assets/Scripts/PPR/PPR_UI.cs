using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPR_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] countdown;

    private PPR ppr;

    private void Start()
    {
        ppr = GetComponent<PPR>();

        DisableAllUI();
    }

    void ShowUI(int _index)
    {
        DisableAllUI();

        countdown[_index].SetActive(true);
    }

    void DisableAllUI()
    {
        foreach (GameObject item in countdown)
        {
            item.SetActive(false);
        }
    }

    public void CountDown()
    {
        StartCoroutine(CountDownCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        yield return new WaitForSeconds(2f);
        ShowUI(0);
        yield return new WaitForSeconds(1f);
        ShowUI(1);
        yield return new WaitForSeconds(1f);
        ShowUI(2);
        yield return new WaitForSeconds(1f);
        ShowUI(3);
        ppr.StartRun();
        yield return new WaitForSeconds(2f);
        DisableAllUI();

    }
}
