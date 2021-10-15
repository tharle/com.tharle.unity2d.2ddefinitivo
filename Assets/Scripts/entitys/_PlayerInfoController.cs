using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class _PlayerInfoController : MonoBehaviour
{   
    [Header("Caracteristicas")]
    public Personagem.Index indexPersonagem;

    [Header("Vida e energias")]
    public int vidaAtual;
    public int vidaMax;

    [Header("Equipamentos")]
    public Weapon.Index indexArma; // arma equipada

    [Header("Inventario e grana")]
    public int qntDinheiro; // Armazena a quantidade de dinheiro


    // -----------------------------------------------
    // FUNÇÕES PUBLICAS
    // -----------------------------------------------
    /// <summary>
    /// Reseta a barra de vida atual do personagem para a vida máxima. 
    /// </summary>
    public void RestaurarVida() {
        print("Restaura a vida: "+vidaAtual+" ----> "+vidaMax);
        this.vidaAtual = this.vidaMax;
    }

    public void CarregarPersonagem(Personagem personagem)
    {
        this.vidaMax = personagem.vidaMax;
    }

    // -----------------------------------------------
    // FUNÇÕES PRIVADAS
    // -----------------------------------------------

}
