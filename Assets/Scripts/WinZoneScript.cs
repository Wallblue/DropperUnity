using UnityEngine;

public class WinZoneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.IsCurrentLevelOver = true;
    }
}
