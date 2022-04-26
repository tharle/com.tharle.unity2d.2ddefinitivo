using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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
    public List<Item> Itens;
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
        Itens = new List<Item>();
        MockItens();
    }

    private void Update() 
    {
        if(IsLoadAllMisc) LoadMisc();
        if(IsLoadAllSlots) LoadSlots();
    }

    private void MockItens() // TODO erase-me 
    {   
        Itens.Add(_dataBaseController.BuscarArma(Weapon.Index.AXE_WOOD));
        Itens.Add(_dataBaseController.BuscarArma(Weapon.Index.BOW_WOOD));
        Itens.Add(_dataBaseController.BuscarArma(Weapon.Index.MACE_WOOD));
        Itens.Add(_dataBaseController.BuscarArma(Weapon.Index.STAFF_ARCANE));
        Itens.Add(_dataBaseController.BuscarArma(Weapon.Index.SWORD_WOOD));
    }

    private void LoadSlots()
    {
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
        var item = Itens.Count() > posSlot ?  Itens.ElementAt(posSlot) : null;

        if(item != null) 
        {
            imageSlot.sprite = item.SpriteItem;
            imageSlot.color = Color.white;
            txtSlot.text = item.NameItem;
            slot.interactable = true;
            if(item is Weapon) {
                var weapon = (Weapon) item;
                print($"COMPARE INDEX WEAPONS: {weapon.index} == {_playerInfoController.indexArma}");
                txtSlot.color = weapon.index == _playerInfoController.indexArma? Color.green : Color.white;
            } else {
                txtSlot.color = Color.white;
            }
        } else {
            imageSlot.sprite = null;
            imageSlot.color = Color.clear;
            txtSlot.text = "";
            slot.interactable = false;
        }

    }

    private void LoadMisc()
    {
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

        if(item is Weapon) _playerInfoController.SelectWeapon((item as Weapon).index);

        IsLoadAllSlots = true; // Force Load all slots

        // TODO add validation for select weapon vs caracter selected
    }

    public void WeaponChanged()
    {
        IsLoadAllSlots = true;

        print("REPONDING WEAPON CHANGED");
    }
}
