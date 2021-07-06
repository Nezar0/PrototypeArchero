using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private GameObject PanelGameOver;
    private void Start()
    {
        Player.OnPlayerDied += ActivatePanel;
    }
    private void ActivatePanel()
    {
        PanelGameOver.gameObject.SetActive(true);
    }
    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }
    private void OnDestroy()
    {
        Player.OnPlayerDied -= ActivatePanel;
    }
}
