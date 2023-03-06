using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float lifeTime = 2f;
    private GameManager gameManager;

    public int points;//puntuacion de los objetos


    public GameObject explosionParticle;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Destroy(gameObject, lifeTime);//autodestruccion cada x tiempo
    }
    private void OnMouseDown()
    {
        if (!gameManager.isGameOver)
        {
            if (gameObject.CompareTag("Bad"))
            {
                if (gameManager.hasPowerupShield)
                {
                    gameManager.hasPowerupShield = false;
                }
                else
                {
                    gameManager.MinusLife();
                }
            }
            else if (gameObject.CompareTag("Good"))
            {
                gameManager.UpdateScore(points);
            }
            else if (gameObject.CompareTag("Shield"))
            {
                gameManager.hasPowerupShield = true;
            }
            Instantiate(original: explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        gameManager.targetPositionsInScene.Remove(transform.position);//dejamos libre la posicion
    }

}
