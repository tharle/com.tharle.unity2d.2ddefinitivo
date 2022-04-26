using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class _PlayerInfoController : MonoBehaviour
{   
    [Header("Scripts externos")]
    private _InventarioController _inventarioController;

    [Header("Caracteristicas")]
    public Personagem.Index indexPersonagem;

    [Header("Vida e energias")]
    public int VidaAtual;
    public int VidaMax;

    [Header("Equipamentos")]
    public Weapon.Index indexArma; // arma equipada
    private Weapon.Index _indexArmaSelected;

    [Header("Inventario e grana")]
    public int qntDinheiro; // Armazena a quantidade de dinheiro

    private void Start() 
    {
        this._inventarioController = FindObjectOfType(typeof(_InventarioController)) as _InventarioController;
        // Load Personagem Selecionado na tela de titulo
        CarregarPersonagem();
            
    }

    private void Update()
    {
        if(indexArma != _indexArmaSelected) SelectWeapon(indexArma);
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

    public void SelectWeapon(Weapon.Index indexWeapon) 
    {
        indexArma = indexWeapon;
        _indexArmaSelected = indexWeapon;
        this._inventarioController.WeaponChanged();
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
