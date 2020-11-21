using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BriefInstructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("shutOff");
    }

    IEnumerator shutOff()
    {
        TextMeshProUGUI instTest = gameObject.GetComponent<TextMeshProUGUI>();
        yield return new WaitForSeconds(2);
        Color32 col = instTest.color;
        while (col.a > 0)
        {
            col.a--;
            instTest.color = col;
            yield return null;
        }
        gameObject.SetActive(false);
        yield break;
    }
}
