using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneScript : MonoBehaviour
{
    public bool isException = false;

    public static bool IsLevelOver = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsLevelOver || isException)
        {
            Physics.gravity = new Vector3(
                0,
                -9.41f,
                0
            );
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
