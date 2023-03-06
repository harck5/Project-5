using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;
    private float minX = -3.75f;
    private float minY = -3.75f;
    private float distanceBetweenSquares = 2.5f;

    public bool isGameOver;
    private float spawnRate = 1f;
    public List<Vector3> targetPositionsInScene;
    public Vector3 randomPos;
    public bool hasPowerupShield;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject startGamePanel;

    private int score;

    private int lives;

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minX + Random.Range(0, 4) * distanceBetweenSquares;
        float spawnPosY = minY + Random.Range(0, 4) * distanceBetweenSquares;
        return new Vector3(spawnPosX, spawnPosY, 0);
    }
    private IEnumerator SpawnRandomTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate); //esperar tiempo
            int randomIndex = Random.Range(0, targetPrefabs.Length);//que prefab aparecera
            randomPos = RandomSpawnPosition();//posicion aleatoria para spawnear
            while (targetPositionsInScene.Contains(randomPos))//si posicion esta ocupada
            {
                randomPos = RandomSpawnPosition();//buscar otra posicion
            }
            Instantiate(targetPrefabs[randomIndex], randomPos, targetPrefabs[randomIndex].transform.rotation);//instanciar con la posicion y la rotacion del propio prefab
            targetPositionsInScene.Add(randomPos);//añadir a la lista de ocupados
        }
    }
    public void UpdateScore(int newPoints)
    {
        score += newPoints;
        scoreText.text = $"Score: \n{score}";
    }
    public void StartGame(int difficulty)
    {
        isGameOver = false;
        score = 0;
        UpdateScore(newPoints: 0);
        lives = 3;
        livesText.text = $"Lives: \n{lives}";
        spawnRate /= difficulty;
        StartCoroutine(SpawnRandomTarget());
        startGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    public void MinusLife()
    {
        lives--;
        livesText.text = $"Lives: \n{lives}";
        if (lives <= 0)
        {
            GameOver();
        }
    }
}
