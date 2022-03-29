using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WeaponController : MonoBehaviour
{

    [Header("Infos atuais do Player")]
    private _PlayerInfoController playerInfoController;
    private _GameController gameController;

    private PlayerScript playerScript;

    // [Header("Projeteis")]

    [Header("Animations atuais da Arma")]
    public GameObject[] WeaponAnimationsMelee;
    public GameObject[] WeaponAnimationsBow;

    public GameObject[] WeaponAnimationsStaff;

    public Weapon armaEquipada;
    
    void Start()
    {
        this.gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this.ResetAllGameObjects(WeaponAnimationsMelee);        
        this.ResetAllGameObjects(WeaponAnimationsBow);        
        this.ResetAllGameObjects(WeaponAnimationsStaff);        
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
        var WeaponAnimations = GetWeaponAnimations(this.armaEquipada.typeWeapon());
        foreach(GameObject weaponAnimmation in WeaponAnimations) {
            weaponAnimmation.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
    }

    /// <summary>
    /// Carrega as sprints da arma selecionada no Game Controle
    /// </summary>
    private void CarregarArma() 
    {

        if(this.armaEquipada == null || this.playerInfoController.indexArma != this.armaEquipada.index){
            this.armaEquipada = gameController.weapons[this.playerInfoController.indexArma];
            GameObject[] WeaponAnimations = GetWeaponAnimations(this.armaEquipada.typeWeapon());
            print($"Carregou Arma : {this.armaEquipada.nameItem} ({this.armaEquipada.index})");
            WeaponAnimations[0].GetComponent<SpriteRenderer>().sprite = this.armaEquipada.sprites[0];
            WeaponAnimations[1].GetComponent<SpriteRenderer>().sprite = this.armaEquipada.sprites[1];
            WeaponAnimations[2].GetComponent<SpriteRenderer>().sprite = this.armaEquipada.sprites[2];
        }
    }

    private GameObject[] GetWeaponAnimations(Weapon.TypeWeapon typeWeapon)
    {
        switch (typeWeapon)
        {
            case Weapon.TypeWeapon.STAFF: 
                return WeaponAnimationsStaff;
            case Weapon.TypeWeapon.BOW:
                return WeaponAnimationsBow;
            default:
                return WeaponAnimationsMelee;
        }
    }

    private void activeWeaponAnimation(int idWeaponAnimation)
    {
        var WeaponAnimations = GetWeaponAnimations(this.armaEquipada.typeWeapon());
        this.ResetAllGameObjects(WeaponAnimations);
        WeaponAnimations[idWeaponAnimation].SetActive(true);
        if(idWeaponAnimation == 2)
        {
            if(armaEquipada.typeWeapon() == Weapon.TypeWeapon.BOW) TrowArrow();
            if(armaEquipada.typeWeapon() == Weapon.TypeWeapon.STAFF) TrowMagic();
        }
    }

    private void TrowArrow()
    {

        // GameObject tempArrow = Instantiate (this.Arrow, hand.position, hand.localRotation);
        // tempArrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10,10) * 10, 200));
        print($"Trow some Arrow!!!");
    }

    private void TrowMagic()
    {
        print($"Trow some magic!!!");
    }

    private void ResetAllGameObjects(GameObject[] gameObjects)
    {
        foreach (var o in gameObjects){
            o.SetActive(false);
        }
    }

    public void ResetAllWeaponsAnimations()
    {
        var WeaponAnimations = GetWeaponAnimations(this.armaEquipada.typeWeapon());
        ResetAllGameObjects(WeaponAnimations);
    }
}
