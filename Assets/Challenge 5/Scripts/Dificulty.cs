using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dificulty : MonoBehaviour
{
    public int difficulty; //Dificultad ordenada de 1 a 3
    private Button _button; 
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty); //cuando toca boton llama a funcion
    }
    private void SetDifficulty()
    {
        gameManager.StartGame(difficulty);//entra en la funcion de gamemanager StartGame
    }
}
