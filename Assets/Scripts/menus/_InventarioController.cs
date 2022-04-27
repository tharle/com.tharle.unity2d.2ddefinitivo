using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _InventarioController : MonoBehaviour
{

    [Header("Controladores Externos")]
    private _PlayerInfoController _playerInfoController;
    private _GameController _gameController;

    [Header("Referencias Objetos do Menu")]
    public Button[] Slots;
    public TextMeshProUGUI TxtQntPotionHP;
    public TextMeshProUGUI TxtQntPotionMP;
    public TextMeshProUGUI TxtQntArrow;

    [Header("Precisa atualizar")]
    public bool IsLoadAllSlots;

    [Header("Infos dos objetos")]
    private int _qntPotionHp;
    private int _qntPotionMP;
    private int _qntArrows;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        _playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        IsLoadAllSlots = true;
    }

    private void Update() 
    {
        LoadMisc();
        if(IsLoadAllSlots) LoadSlots();
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
        var item = _playerInfoController.GetItensCount() > posSlot ?  _playerInfoController.GetItemSlot(posSlot) : null;

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
        if(_qntPotionHp != _playerInfoController.QntPotionHp) 
        {
            _qntPotionHp = _playerInfoController.QntPotionHp;
            TxtQntPotionHP.SetText($"x{FormatQuantityText(_qntPotionHp)}");
        }

        if(_qntPotionMP != _playerInfoController.QntPotionMP) 
        {
            _qntPotionMP = _playerInfoController.QntPotionMP;
            TxtQntPotionMP.SetText($"x{FormatQuantityText(_qntPotionMP)}");
        }

        if(_qntArrows != _playerInfoController.QntArrows) 
        {
            _qntArrows = _playerInfoController.QntArrows;
            TxtQntArrow.SetText($"x{FormatQuantityText(_qntArrows)}");
        }
    }

    private string FormatQuantityText(int quantity)
    {
        string quantityStr;
        quantityStr = quantity < 10 ? "0":"";
        quantityStr += quantity.ToString();
        return quantityStr;
    }

    
    public void SelectSlotMenu(int posSlot)
    {}
    //     var item = Itens[posSlot];

    //     if(item == null) return;

    //     if(item is Weapon) _playerInfoController.SelectWeapon((item as Weapon).index);

    //     IsLoadAllSlots = true; // Force Load all slots

    //     // TODO add validation for select weapon vs caracter selected
    // }

    public void WeaponChanged()
    {
        IsLoadAllSlots = true;

        print("REPONDING WEAPON CHANGED");
    }
}
