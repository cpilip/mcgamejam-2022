using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public Animator m_animator;
    private int m_sceneToLoad;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void FadeToLevel(int buildIndex)
    {
        m_sceneToLoad = buildIndex;
        m_animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(m_sceneToLoad);
    }

}
