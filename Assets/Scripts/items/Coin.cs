using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    [Header("Scripts externos")]
    private _PlayerInfoController playerInfoController;

    [Header("Valores")]
    public int valor;
    public string tipoMoeda;
    
    void Start(){
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
    }
    
    /// <summary>
    ///  Coleta e armazena o dinheiro, depois destroi o objeto da cena
    /// </summary>
    public void Coletar(){
        playerInfoController.qntDinheiro += valor; // Coleta a grana
        Destroy(this.gameObject); // destroi a moeda
    }
}
