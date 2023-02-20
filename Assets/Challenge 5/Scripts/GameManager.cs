using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;
    private float minX = -3.75f;
    private float minY = -3.75f;
    private float distanceBetweenSquares = 2.5f;

    public bool isGameOver;
    public float spawnRate = 1f;
    public List<Vector3> targetPositionsInScene;
    public Vector3 randomPos;

    public TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        isGameOver = false;
        StartCoroutine("SpawnRandomTarget");
        scoreText.text = $"Score: \n{score}";

    }
    void Update()
    {
        
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
}
