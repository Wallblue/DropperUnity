using UnityEngine;

public class WinZoneScript : MonoBehaviour
{
    public Levels currentLevel;
    private void OnTriggerEnter(Collider other)
    {
        switch (currentLevel)
        {
            case Levels.Gravity:
                GameManager.IsGravityLevelDone = true;
                break;
            case Levels.MovingPlatforms:
                GameManager.IsMovingPlatformsLevelDone = true;
                break;
        }

        GameManager.IsCurrentLevelOver = true;
    }
}
