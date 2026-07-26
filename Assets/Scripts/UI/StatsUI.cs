using UnityEngine;
using UnityEngine.UI;
using TMPro;    

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _blueKillsText;
    [SerializeField] private TMP_Text _redKillsText;
    [SerializeField] private TMP_Text _timerText;

    private void Update()
    {
        _blueKillsText.text = $"Blue: {GameManager.Instance.BlueKills}";
        _redKillsText.text = $"Red: {GameManager.Instance.RedKills}";

        int minutes = Mathf.FloorToInt(GameManager.Instance.GameTime / 60f);
        int seconds = Mathf.FloorToInt(GameManager.Instance.GameTime % 60f);
        _timerText.text = $"{minutes:00}:{seconds:00}";
    }
}