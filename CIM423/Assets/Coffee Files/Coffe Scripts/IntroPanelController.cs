using UnityEngine;

public class IntroPanelController : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject gameplayRoot;

    private bool hasStarted = false;

    private void Start()
    {
        if (introPanel != null)
            introPanel.SetActive(true);

        if (gameplayRoot != null)
            gameplayRoot.SetActive(false);

        // Keep time running so XR movement/controllers still work
        Time.timeScale = 1f;
    }

    public void BeginGame()
    {
        if (hasStarted)
            return;

        hasStarted = true;

        if (introPanel != null)
            introPanel.SetActive(false);

        if (gameplayRoot != null)
            gameplayRoot.SetActive(true);
    }
}