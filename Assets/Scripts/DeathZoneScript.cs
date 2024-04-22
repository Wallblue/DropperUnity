using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneScript : MonoBehaviour
{
    public bool isException;

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.IsCurrentLevelOver || isException)
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
