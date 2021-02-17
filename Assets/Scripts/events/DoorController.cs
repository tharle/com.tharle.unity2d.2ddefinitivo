using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Classe que gerencia os eventos de portas
/// </summary>
public class DoorController : MonoBehaviour {

    [Header("Scripts externos")]
    private PlayerScript playerScript;
    private FadeScript fadeScript;
    
    [Header("Configuração")]
    public Transform destino;

    void Start() {
        this.playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        this.fadeScript = FindObjectOfType(typeof(FadeScript)) as FadeScript;
    }

    public void Interacao() {
        StartCoroutine("AcionarPorta");
    }

    IEnumerator AcionarPorta() {
        print("O jogador abriu a porta");

        this.playerScript.transform.gameObject.SetActive(false); // Esconde personagem

        //Fade In
        this.fadeScript.StartFadeIn();
        yield return new WaitWhile(() => this.fadeScript.IsBlackout());

        yield return new WaitForSeconds(.5f); // Da um tempo entre fade In e o Fade out

        //Fade Out
        this.fadeScript.StartFadeOut();

        this.playerScript.transform.gameObject.SetActive(true); // Exibe personagem

        //Teleportar o personagem pro destido
        playerScript.transform.position = destino.position;
    }
}
