using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public void ChangeSceneNow(string sceneName)
     {
         SceneManager.LoadScene(sceneName);
     }
}