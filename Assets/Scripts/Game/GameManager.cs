using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Player prefab to instanciate")]
    private GameObject playerPrefab;

    [SerializeField]
    [Tooltip("Where to spawn the players")]
    private Transform[] playerSpawnPoints;

    // The game has "hardcoded" number of players (2 to 4)
    private int minPlayersCounter = 2;
    private int maxPlayersCounter = 4;
    private int effectivePlayerCounter = 0;

    private bool isPaused = false;

    private void Awake()
    {
        Assert.IsNotNull(this.playerPrefab, "Missing asset (player prefab)");
        Assert.IsNotNull(this.playerSpawnPoints, "Missing asset (spawnpoints)");
        Assert.IsTrue(this.playerSpawnPoints.Length >= this.minPlayersCounter, "Invalid asset (Not enought spawnpoints)");
        Assert.IsTrue(this.playerSpawnPoints.Length <= this.maxPlayersCounter, "Invalid asset (Too many spawnpoints)");
    }

    private void Start()
    {
        // TODO: For debug, for now we spawn 2 players (to update from Player selection)
        this.AddPlayerInGame();
        this.AddPlayerInGame();
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        this.PauseGame();
    }

    public void PauseGame()
    {
        Debug.Log("Pause the game");
        this.isPaused = true;
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume the game");
        this.isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void TogglePausegame()
    {
        if(this.isPaused)
        {
            this.ResumeGame();
        }
        else
        {
            this.PauseGame();
        }
    }

    public void AddPlayerInGame()
    {
        if(this.effectivePlayerCounter < this.maxPlayersCounter)
        {
            Instantiate(this.playerPrefab, this.playerSpawnPoints[this.effectivePlayerCounter]);
            this.effectivePlayerCounter++;
        }
    }
}
