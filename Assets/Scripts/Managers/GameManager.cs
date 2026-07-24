using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; }

    public float GameTime { get; private set; }

    public int BlueKills { get; private set; }

    public int RedKills { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (State != GameState.Playing)
        {
            return;
        }

        GameTime += Time.deltaTime;
    }

    public void StartGame()
    {
        State = GameState.Playing;
        OnGameStateChanged?.Invoke(State);
    }

    public void Victory()
    {
        if (State != GameState.Playing)
        {
            return;
        }

        State = GameState.Victory;

        Time.timeScale = 0f;

        OnGameStateChanged?.Invoke(State);
    }

    public void Defeat()
    {
        if (State != GameState.Playing)
        {
            return;
        }

        State = GameState.Defeat;

        Time.timeScale = 0f;

        OnGameStateChanged?.Invoke(State);
    }

    public void AddKill(Team team)
    {
        if (team == Team.Blue)
        {
            BlueKills++;
        }
        else
        {
            RedKills++;
        }
    }
}