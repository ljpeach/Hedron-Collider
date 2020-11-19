using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "ExitObject")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            //Debug.Log("Victory!");
        }

    }
}
