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
    public bool destinoEscuro;
    public Material materialSenvivelLuz2D;
    public Material materialPadrao2D;

    void Start() {
        this.playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        this.fadeScript = FindObjectOfType(typeof(FadeScript)) as FadeScript;
    }

    public void Interacao() {
        StartCoroutine("AcionarPorta");
    }

    
    /// <summary>
    ///  Função que muda o material do personagme de acordo com o lugar de destino da porta.
    /// Ex: Se o lugar de destino for escuro, então é preciso que o player seja sensivel a luz.
    /// </summary>
    private void controleIluminacaoPlayer(){
        if(this.destinoEscuro){
             this.playerScript.SetMaterial(materialSenvivelLuz2D);
        } else {
            this.playerScript.SetMaterial(materialPadrao2D);
        }
    }

    IEnumerator AcionarPorta() {
        print("O jogador abriu a porta");

        this.playerScript.transform.gameObject.SetActive(false); // Esconde personagem

        //Fade In
        this.fadeScript.StartFadeIn();
        yield return new WaitWhile(() => this.fadeScript.IsBlackout());
        
        controleIluminacaoPlayer();

        //Fade Out
        this.fadeScript.StartFadeOut();

        this.playerScript.transform.gameObject.SetActive(true); // Exibe personagem

        //Teleportar o personagem pro destido
        playerScript.transform.position = destino.position;
    }
}
