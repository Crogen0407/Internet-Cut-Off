using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class playButton : MonoBehaviour
{
    [SerializeField] private AudioSource ClickSound;
    [SerializeField] private SpriteRenderer SceneEffect;
    bool CanBtClick;
    float a = 0;

    private void Start()
    {
        CanBtClick = true;
        SceneEffect.color = new Color(0, 0, 0, 0);
    }

    public void onPlayClick()
    {
        if (CanBtClick == true) StartCoroutine(onPlaySound("Background"));
    }
    
    public IEnumerator onPlaySound(string Scene)
    {
        ClickSound.Play();
        yield return new WaitForSeconds(0.25f);
        if (Scene == null) Application.Quit();
        else
        {
            while (true)
            {
                if (a >= 1) break;
                yield return new WaitForSeconds(0.1f);
                a += 0.1f;
                SceneEffect.color = new Color(0,0,0,a);
            }

            SceneManager.LoadScene(Scene);
        }

        yield return null;
    }

    public void onQuitClick()
    {
        if (CanBtClick == true) StartCoroutine(onPlaySound(null));
    }
}
