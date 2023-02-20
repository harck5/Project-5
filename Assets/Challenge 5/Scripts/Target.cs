using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float lifeTime = 1f;
    private GameManager gameManager;

    public int points;

    public GameObject explosionParticle;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Destroy(gameObject, lifeTime);
    }
    private void OnMouseDown()
    {
        if (!gameManager.isGameOver)
        {
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.isGameOver = true;
            }
            else if (gameObject.CompareTag("Good"))
            {
                gameManager.UpdateScore(points);
            }
            Instantiate(original: explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        gameManager.targetPositionsInScene.Remove(transform.position);
    }

}
