using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReSkinScript : MonoBehaviour {

    [Header("Objetos internos")]
    private SpriteRenderer spriteRenderer;
    private _GameController  gameController;
    private _PlayerInfoController playerInfoController;
    private _DataBaseController _dataBase;

    [Header("Skins")]
    public bool isPlayer;
    public Sprite[] spritesSkins; // Coleção de sprites que serão as skins do personagem
    public string spriteSheetName; // Nome da spritesheet da 'skin'
    public string loadedSpriteSheetName; // Nome do spriteSheet em uso
    
    private Dictionary<string, Sprite> spriteSheetMap; // Onde vai ser a troca dentro do personagem(?)

    // Start is called before the first frame update
    void Start() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.gameController = FindObjectOfType(typeof (_GameController)) as _GameController;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this._dataBase = FindObjectOfType(typeof(_DataBaseController)) as _DataBaseController;
        this.LoadSpriteSheet();
    }

    // Update is called once per frame
    void LateUpdate() { 
        if(isPlayer){
            Personagem personagemSelecionado = _dataBase.BuscarPersonagem(this.playerInfoController.indexPersonagem);
            if(personagemSelecionado != null && spriteSheetName != personagemSelecionado.SpritesName){
                this.LoadSpriteSheetPersonagem(personagemSelecionado);
                gameController.ValidarPersonagemEArmaEquipada(); // verifica e valida personagem selecionado com a arma ja selecionada
                this.LoadPlayerInfos(personagemSelecionado);
            }
        }else if(this.loadedSpriteSheetName != spriteSheetName) { // Se foi mudado o nome da skins
            this.LoadSpriteSheet();
        }

        // print("Tentando carregar a animação [" + spriteRenderer.sprite.name + "]");
        //  foreach(KeyValuePair<string, Sprite> skins in spriteSheetMap) {
        //     //Now you can access the key and value both separately from this attachStat as:
        //     Debug.Log(skins.Key);
        //     Debug.Log(skins.Value);
        // }
        this.spriteRenderer.sprite = this.spriteSheetMap[spriteRenderer.sprite.name];
    }

    /// <summary>
    /// Função que carrega todas as sprintes no spritesSkins a partir do personagem selecionado
    /// </summary>
    private void LoadSpriteSheetPersonagem(Personagem personagemSelecionado) {
        this.spriteSheetName = personagemSelecionado.SpritesName; // Carrega as sprints do persnagem
        LoadSpriteSheet();
    }


    /// <summary>
    /// Função que carrega todas as sprintes no spritesSkins a partir de um nome
    /// </summary>
    private void LoadSpriteSheet() {
        this.spritesSkins = Resources.LoadAll<Sprite>(spriteSheetName); // Carrega as sprints da configuracao setada manualmente
        
        this.spriteSheetMap = this.spritesSkins.ToDictionary(spriteSkin => spriteSkin.name, spriteSkin => spriteSkin); // Indexa as sprints pelo nome da ação
        this.loadedSpriteSheetName = this.spriteSheetName;
    }

    private void LoadPlayerInfos(Personagem personagem){
        this.playerInfoController.CarregarPersonagemInfos(personagem, true);
    }
}
