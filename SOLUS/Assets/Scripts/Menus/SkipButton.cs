using UnityEngine;
using UnityEngine.Playables;

public class SkipButton : MonoBehaviour
{
    public GameObject loader;
    public PlayableDirector director;

    private void OnEnable()
    {
        director.Play();
    }

    public void LoadScene()
    {
        loader.SetActive(true);
    }
}