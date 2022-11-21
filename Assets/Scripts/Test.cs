using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private Transform playerTr;

    private Vector3 startPos;

    bool checking = false;

    private void Start()
    {
        playerTr = GetComponent<Transform>();

        startPos = transform.position;

        StartCoroutine(InputCheck());
    }

    IEnumerator InputCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if (playerTr.position == startPos)
            {
                if (!checking)
                {
                    Debug.Log("Check Start");
                    StartCoroutine(TimeCheck());
                }
            }
            else
            {
                startPos = playerTr.position;
                checking = false;
                StopCoroutine(TimeCheck());
            }
        }        
    }
    IEnumerator TimeCheck()
    {
        checking = true;
        yield return new WaitForSeconds(10.0f);

        SceneManager.LoadScene("Main");
    }
}
