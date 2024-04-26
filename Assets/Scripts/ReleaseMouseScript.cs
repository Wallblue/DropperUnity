using UnityEngine;

public class ReleaseMouseScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        UnlockCursor();
    }


    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}