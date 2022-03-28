using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WeaponController : MonoBehaviour
{

    [Header("Infos atuais do Player")]
    private _PlayerInfoController playerInfoController;
    private _GameController gameController;

    private PlayerScript playerScript;

    [Header("Infos atuais da Arma")]
    public GameObject[] WeaponAnimationMelees;
    public GameObject[] WeaponAnimationRanges;

    public GameObject[] WeaponAnimations
    {
        get 
        {
            if(armaEquipada != null && armaEquipada.IsMelee())
            {
                return WeaponAnimationMelees;
            }
            return WeaponAnimationRanges;
        }
    }

    public Weapon armaEquipada;
    
    void Start()
    {
        this.gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this.ResetAllWeaponsAnimations();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        CarregarArma();
    }

    /// <summary>
    ///  Função para mudar o material do personagem e da arma. 
    ///  Usado para saber se o material do personagem vai ser sensivel a luz ou não. 
    ///  Nesse exemplo ele é usado ao entrar e sair em uma caverna.
    /// </summary>
    /// <param name="novoMaterial"></param>
    public void SetMaterial(Material novoMaterial)
    {
        // Muda o material de todas os efeitos de armas
        foreach(GameObject weaponAnimmation in this.WeaponAnimations) {
            weaponAnimmation.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
    }

    public void CarregarArma() 
    {
        if(this.armaEquipada == null || this.playerInfoController.indexArma != this.armaEquipada.index){
            this.armaEquipada = gameController.weapons[this.playerInfoController.indexArma];
            print($"Carregou Arma : {this.armaEquipada.nameItem} ({this.armaEquipada.index})");
            this.WeaponAnimations[0].GetComponent<SpriteRenderer>().sprite = this.armaEquipada.sprites[0];
            this.WeaponAnimations[1].GetComponent<SpriteRenderer>().sprite = this.armaEquipada.sprites[1];
            this.WeaponAnimations[2].GetComponent<SpriteRenderer>().sprite = this.armaEquipada.sprites[2];
        }
    }

    private void activeWeaponAnimation(int idWeaponAnimation)
    {
        print($"WEAPON-CONTROLER: ACTIVE WEAPON ANIMATION: {idWeaponAnimation}");
        this.ResetAllWeaponsAnimations();
        this.WeaponAnimations[idWeaponAnimation].SetActive(true);
    }

    public void ResetAllWeaponsAnimations()
    {   
        foreach (var wa in this.WeaponAnimations){
            wa.SetActive(false);
        }
    }
}
