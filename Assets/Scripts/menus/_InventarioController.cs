using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _InventarioController : MonoBehaviour
{

    [Header("Controladores Externos")]
    private _DataBaseController _dataBaseController;
    private _PlayerInfoController _playerInfoController;

    [Header("Referencias Objetos do Menu")]
    public Button[] Slots;
    public TextMeshProUGUI TxtQntPotionHP;
    public TextMeshProUGUI TxtQntPotionMP;
    public TextMeshProUGUI TxtQntArrow;
    
    [Header("Infos dos objetos")]
    public Item[] Itens;
    public int QntPotionHp;
    public int QntPotionMP;
    public int QntArrows;

    [Header("Precisa atualizar")]
    public bool IsLoadAllSlots;
    public bool IsLoadAllMisc;

    // Start is called before the first frame update
    void Start()
    {
        _dataBaseController = FindObjectOfType(typeof(_DataBaseController)) as _DataBaseController;
        _playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        IsLoadAllSlots = true;
        IsLoadAllMisc = true;
        Itens = new Item[10];
        MockItens();
    }

    private void Update() 
    {
        if(IsLoadAllMisc) LoadMisc();
        if(IsLoadAllSlots) LoadSlots();   
    }

    private void MockItens() // TODO erase-me 
    {   
        Itens[0] = _dataBaseController.BuscarArma(Weapon.Index.AXE_WOOD);
        Itens[1] = _dataBaseController.BuscarArma(Weapon.Index.BOW_WOOD);
        Itens[2] = _dataBaseController.BuscarArma(Weapon.Index.MACE_WOOD);
        Itens[3] = _dataBaseController.BuscarArma(Weapon.Index.STAFF_ARCANE);
        Itens[4] = _dataBaseController.BuscarArma(Weapon.Index.SWORD_WOOD);
    }

    private void LoadSlots()
    {
        print("INVENTARIO - LOAD ALL SLOTS!");
        for (int posSlot = 0; posSlot < Slots.Length; posSlot++)
        {
            LoadSlotImageByPos(posSlot);
        }
        IsLoadAllSlots = false;
    }

    private void LoadSlotImageByPos(int posSlot) 
    {
        var slot = Slots[posSlot];
        var imageSlot = slot.transform.GetChild(0).GetComponent<Image>();
        var txtSlot = slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        var item = Itens[posSlot];
        imageSlot.sprite = item != null? item.SpriteItem : null;
        imageSlot.color = item != null? Color.white : Color.clear;
        txtSlot.text = item != null? item.NameItem : "";
        slot.interactable = item != null;

    }

    private void LoadMisc()
    {
        print("INVENTARIO - LOAD ALL MISC!");
        TxtQntPotionHP.SetText($"x{FormatQuantityText(QntPotionHp)}");
        TxtQntPotionMP.SetText($"x{FormatQuantityText(QntPotionMP)}");
        TxtQntArrow.SetText($"x{FormatQuantityText(QntArrows)}");
        IsLoadAllMisc = false;
    }

    private string FormatQuantityText(int quantity)
    {
        string quantityStr;
        quantityStr = quantity < 10 ? "0":"";
        quantityStr += quantity.ToString();
        return quantityStr;
    }

    /// <summary>
    /// Equipa a arma selecionada
    /// </summary>
    /// <param name="posSlot">Poisção em qual foi selecionado a arma</param>
    public void SelectSlotMenu(int posSlot)
    {
        var item = Itens[posSlot];

        if(item == null) return;

        if(item is Weapon) EquipeWeapon(item as Weapon);
    }

    private void EquipeWeapon(Weapon weapon)
    {
        _playerInfoController.indexArma = weapon.index;
    }
}
