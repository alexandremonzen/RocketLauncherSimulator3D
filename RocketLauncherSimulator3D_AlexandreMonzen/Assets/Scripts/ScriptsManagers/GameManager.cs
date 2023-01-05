using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;

    private void Awake()
    {
        UnpauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToogleObject(GameObject gameObject)
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void ToggleTrees()
    {
        if(_terrain.treeDistance > 0)
        {
            _terrain.treeDistance = 0;
        }
        else
        {
            _terrain.treeDistance = 1;
        }
    }
}
