using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneScript : MonoBehaviour
{
    public static bool IsLevelOver = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!IsLevelOver)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
