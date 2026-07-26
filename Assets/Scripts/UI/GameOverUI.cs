using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _resultText;

    private void Start()
    {
        _panel.SetActive(false);
        GameManager.Instance.OnGameStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(GameState state)
    {
        if (state == GameState.Victory)
        {
            _panel.SetActive(true);
            _resultText.text = "ПОБЕДА";
        }
        else if (state == GameState.Defeat)
        {
            _panel.SetActive(true);
            _resultText.text = "ПОРАЖЕНИЕ";
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= HandleStateChanged;
        }
    }
}