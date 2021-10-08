using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour{

    [Header("Scripts externos")]
    private PlayerScript playerScript;
    private _PlayerInfoController playerInfoController;

    [Header("Configuração de Barra de Vida")]
    public Image[] hpBar; // Barra de vida no canto
    public Sprite halfLife;
    public Sprite fullLife;

    // Start is called before the first frame update
    void Start() {
        this.playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
    }

    // Update is called once per frame
    void Update() {
        controleBarraVida();
    }

    /// <summary>
    ///  Método que calcula e controla a Barra de vida UI
    /// </summary>
    private void controleBarraVida() {
        float percVida = ((float) playerInfoController.vidaAtual / (float) playerInfoController.vidaMax) * 10; // Calcula percentual de vida 0 - 1

        for(int i = 0, vida = 1; i < hpBar.Length; i++, vida+=2){
            Image imgHP = hpBar[i];
            imgHP.enabled = true;
            if(vida + 1 <= percVida) {// Ta full
                imgHP.sprite = fullLife;
            } else if(vida <= percVida) { // Ta na metade
                imgHP.sprite = halfLife;
            } else {
                imgHP.enabled = false; // Ta zerado
            }
        }
    }
}
