using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void ResetMap()
    {
        SceneManager.LoadSceneAsync("StickManTest");
    }
}