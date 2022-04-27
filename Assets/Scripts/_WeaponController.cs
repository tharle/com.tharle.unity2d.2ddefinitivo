using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WeaponController : MonoBehaviour
{

    [Header("Infos atuais do Player")]
    private _PlayerInfoController _playerInfoController;
    private _GameController _gameController;

    private PlayerScript playerScript;
    private _DataBaseController _dataBase;

    // [Header("Projeteis")]

    [Header("Animations atuais da Arma")]
    public GameObject[] WeaponAnimationsMelee;
    public GameObject[] WeaponAnimationsBow;
    public GameObject[] WeaponAnimationsBowArrows;
    public GameObject[] WeaponAnimationsStaff;
    public Weapon armaEquipada;
    public int ReloadFromSlotInventory;

    [Header("Projeteis")]
    public GameObject ArrowPrefab, MagicPrefab;
    public Transform ArrowSpawn, MagicSpawn;
    
    void Start()
    {
        this._gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        this._playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this.playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        this._dataBase = FindObjectOfType(typeof(_DataBaseController)) as _DataBaseController;
        this.ReloadFromSlotInventory = -1;
        ResetAllWeaponsAnimations();
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
        foreach(GameObject weaponAnimmation in WeaponAnimationsMelee) {
            weaponAnimmation.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
        foreach(GameObject weaponAnimmation in WeaponAnimationsBow) {
            weaponAnimmation.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
        foreach(GameObject weaponAnimmation in WeaponAnimationsBowArrows) {
            weaponAnimmation.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
        foreach(GameObject weaponAnimmation in WeaponAnimationsStaff) {
            weaponAnimmation.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
    }

    /// <summary>
    /// Carrega as sprints da arma selecionada no Game Controle
    /// </summary>
    private void CarregarArma() 
    {
        if(this.armaEquipada == null || this._playerInfoController.indexArma != this.armaEquipada.index || ReloadFromSlotInventory != -1){
            // TODO pode existir um caso que ele "recarregue" uma arma sem a necessidade
            _gameController.ValidarPersonagemEArmaEquipada(); // verifica e valida arma selecionada com a personagem ja selecionada
            CarregarArmaInventario();

            GameObject[] WeaponAnimations = GetWeaponAnimations(this.armaEquipada.typeWeapon());
            print($"Carregou Arma : {this.armaEquipada.NameItem} ({this.armaEquipada.index})");

            for (int i = 0; i <  this.armaEquipada.sprites.Length; i++)
            {
                WeaponAnimations[i].GetComponent<SpriteRenderer>().sprite = this.armaEquipada.sprites[i];    
            }
        }
    }

    private void CarregarArmaInventario()
    {
        if(ReloadFromSlotInventory != -1)
        {
            var item = _playerInfoController.GetItemSlot(ReloadFromSlotInventory);
            if (item != null && item is Weapon) 
            {
                this.armaEquipada =  item as Weapon;
                this._playerInfoController.indexArma = armaEquipada.index; // Fix para impedir de recarregar arma anterior
            }
            ReloadFromSlotInventory = -1;
        } else 
        {
            this.armaEquipada = _dataBase.BuscarArma(this._playerInfoController.indexArma); // TODO change for search from inventory insted
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
    }

    private void TrowArrow()
    {
        print($"Trow some Arrow!!!");
        GameObject tempArrow = Instantiate (this.ArrowPrefab, this.ArrowSpawn.position, this.ArrowSpawn.localRotation);
        var velocity = 5;

        if (this.playerScript.lookLeft){// Flip arrow
            tempArrow.transform.localScale = new Vector3(
                tempArrow.transform.localScale.x * -1, 
                tempArrow.transform.localScale.y,
                tempArrow.transform.localScale.z
            );
            velocity *= -1;
        }
        tempArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
    }

    private void TrowMagic()
    {
        print($"Trow some magic!!!");
        GameObject tempMagic = Instantiate (this.MagicPrefab, this.MagicSpawn.position, this.MagicSpawn.localRotation);
        var velocity = this.playerScript.lookLeft ? -3 : 3;
        tempMagic.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
    }

    private void ResetAllGameObjects(GameObject[] gameObjects)
    {
        foreach (var o in gameObjects){
            o.SetActive(false);
        }
    }

    public void ResetWeaponsAnimationsForEqupedWeapon()
    {
        var WeaponAnimations = GetWeaponAnimations(this.armaEquipada.typeWeapon());
        ResetAllGameObjects(WeaponAnimations);
    }

    public void ResetAllWeaponsAnimations()
    {
        this.ResetAllGameObjects(WeaponAnimationsMelee);
        this.ResetAllGameObjects(WeaponAnimationsBow);
        this.ResetAllGameObjects(WeaponAnimationsStaff);
    }

    

    public float GetIdAnimationWeapon(){
        return armaEquipada != null? armaEquipada.IdAnimationWeapon : 0f;
    }
}
