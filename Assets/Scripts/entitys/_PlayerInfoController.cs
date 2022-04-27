using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class _PlayerInfoController : MonoBehaviour
{   
    [Header("Scripts externos")]
    private _InventarioController _inventarioController;
    private _DataBaseController _dataBase;

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
    private List<Item> _itens;
    public int QntPotionHp;
    public int QntPotionMP;
    public int QntArrows;

    private void Start() 
    {
        this._inventarioController = FindObjectOfType(typeof(_InventarioController)) as _InventarioController;
        this._dataBase = FindObjectOfType(typeof(_DataBaseController)) as _DataBaseController;
        _itens = new List<Item>();
        // Load Personagem Selecionado na tela de titulo
        CarregarPersonagem();
        MockItens();
    }

    private void Update()
    {
        if(indexArma != _indexArmaSelected) SelectWeapon(indexArma);
    }

     private void MockItens() // TODO erase-me 
    {   
        AddItem(_dataBase.BuscarArma(Weapon.Index.AXE_WOOD));
        AddItem(_dataBase.BuscarArma(Weapon.Index.BOW_WOOD));
        AddItem(_dataBase.BuscarArma(Weapon.Index.MACE_WOOD));
        AddItem(_dataBase.BuscarArma(Weapon.Index.STAFF_ARCANE));
        AddItem(_dataBase.BuscarArma(Weapon.Index.SWORD_WOOD));
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

    public Item GetItemSlot(int posSlot)
    {
        if(_itens.Count() > posSlot) return _itens.ElementAt(posSlot);

        return null;
    }

    public int GetItensCount()
    {
        return _itens.Count();
    }

    public void AddItem(Item item)
    {
        if(_itens.Count() < 10) {
            _itens.Add(item);
            _inventarioController.WeaponChanged();
        }
    }

    public void SetItemSlot(int posSlot, Item item)
    {
        if(_itens.Count() > posSlot) {
            //_itens.Insert(posSlot, item);
            _itens[posSlot] = item;
            _inventarioController.WeaponChanged();
        }
    }

     public void RemoveItem(int posSlot)
    {
        if(_itens.Count() > posSlot) {
            _itens.RemoveAt(posSlot);
            _inventarioController.WeaponChanged();
        }
    }

    // -----------------------------------------------
    // FUNÇÕES PRIVADAS
    // -----------------------------------------------

}
