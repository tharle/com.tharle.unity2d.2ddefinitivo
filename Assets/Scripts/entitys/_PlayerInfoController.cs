using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class _PlayerInfoController : MonoBehaviour
{   

    // Vida e energias
    public int vidaAtual;
    public int vidaMax;

    // Itens Equipados
    public Weapon.Index indexArma; // arma equipada

    // Armazenamento de itens e grana
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

    // -----------------------------------------------
    // FUNÇÕES PRIVADAS
    // -----------------------------------------------
    
}
