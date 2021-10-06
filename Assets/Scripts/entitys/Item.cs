using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
      /// <summary>
    ///  Nome do item. Ele é exibido na loja, nos menus e no log de combate.
    /// </summary>
    public string nameItem {
        get;
    }
    
    /// <summary>
    /// Valor em que o item é vendido na loja   
    /// </summary>
    public float valueBuy {
        get;
    }
    
    /// <summary>
    /// Valor pelo qual o item será comprado pela loja.
    /// </summary>
    public float valueSell {
        get;
    }

    public Item(string nameItem){
        this.nameItem = nameItem;
        this.valueBuy = 0;
        this.valueSell = 0;
    }

    public bool IsWeapon(){
        return GetType() == typeof(Weapon);
    }
}
