using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
      /// <summary>
    ///  Nome do item. Ele é exibido na loja, nos menus e no log de combate.
    /// </summary>
    public string NameItem {
        get;
    }
    
    /// <summary>
    /// Valor em que o item é vendido na loja   
    /// </summary>
    public float ValueBuy {
        get;
    }
    
    /// <summary>
    /// Valor pelo qual o item será comprado pela loja.
    /// </summary>
    public float ValueSell {
        get;
    }

    /// <summary>
    /// Sprite do item que será exibido no inventario
    /// </summary>
    public Sprite SpriteItem;

    public Item(string nameItem, Sprite SpriteItem){
        this.NameItem = nameItem;
        this.ValueBuy = 0;
        this.ValueSell = 0;
        this.SpriteItem = SpriteItem;
    }

    public bool IsWeapon(){
        return GetType() == typeof(Weapon);
    }
}
