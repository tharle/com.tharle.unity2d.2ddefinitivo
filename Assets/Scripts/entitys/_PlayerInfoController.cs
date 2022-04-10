using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class _PlayerInfoController : MonoBehaviour
{   
    [Header("Caracteristicas")]
    public Personagem.Index indexPersonagem;

    [Header("Vida e energias")]
    public int VidaAtual;
    public int VidaMax;

    [Header("Equipamentos")]
    public Weapon.Index indexArma; // arma equipada

    [Header("Inventario e grana")]
    public int qntDinheiro; // Armazena a quantidade de dinheiro

    private void Start() 
    {
        // Load Personagem Selecionado na tela de titulo
        CarregarPersonagem();
            
    }

     private void CarregarPersonagem()
    {
        string personagemIndexString = PlayerPrefs.GetString(PlayerPrefsConst.PERSONAGEM_INDEX);
        Personagem.Index indexPersonagemTemp;
        if (Enum.TryParse(personagemIndexString, out indexPersonagemTemp))
        {
            indexPersonagem = indexPersonagemTemp;
            indexArma = Weapon.Index._NONE_; // Forca carregar arma padrao
        }

    }



    // -----------------------------------------------
    // FUNÇÕES PUBLICAS
    // -----------------------------------------------
    /// <summary>
    /// Reseta a barra de vida atual do personagem para a vida máxima. 
    /// </summary>
    public void RestaurarVida() {
        print("Restaura a vida: "+VidaAtual+" ----> "+VidaMax);
        this.VidaAtual = this.VidaMax;
    }

    public void CarregarPersonagemInfos(Personagem personagem, bool carregarVida)
    {
        this.VidaMax = personagem.VidaMax;
        this.VidaAtual = carregarVida ? this.VidaMax : this.VidaAtual;
    }

    // -----------------------------------------------
    // FUNÇÕES PRIVADAS
    // -----------------------------------------------

}
