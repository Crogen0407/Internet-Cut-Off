using UnityEngine.SceneManagement;
using UnityEngine;

public class playButton : MonoBehaviour
{
    public void onPlayClick()
    {
        SceneManager.LoadScene("Background");
    }

    public void onQuitClick()
    {
        Application.Quit();
    }
}
