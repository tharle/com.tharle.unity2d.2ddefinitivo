using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Classe que gerencia os eventos de portas
/// </summary>
public class DoorController : MonoBehaviour {

    [Header("Scripts externos")]
    private PlayerScript playerScript;
    
    [Header("Configuração")]
    public Transform destino;

    void Start() {
        this.playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
    }

    public void Interacao() {
        print("O jogador abriu a porta");
        playerScript.transform.position = destino.position;

    }
}
