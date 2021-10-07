using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    [Header("Scripts externos")]
    private _GameController gameController;

    [Header("Valores")]
    public int valor;
    public string tipoMoeda;
    
    void Start(){
        this.gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
    }
    
    /// <summary>
    ///  Coleta e armazena o dinheiro, depois destroi o objeto da cena
    /// </summary>
    public void Coletar(){
        gameController.qntDinheiro += valor; // Coleta a grana
        Destroy(this.gameObject); // destroi a moeda
    }
}
