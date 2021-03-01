using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // import para alterar os estados de animações

public class ReSkinScript : MonoBehaviour {

    [Header("Objetos internos")]
    private SpriteRenderer spriteRenderer;

    [Header("Skins")]
    public Sprite[] spritesSkins; // Coleção de sprites que serão as skins do personagem
    public string spriteSheetName; // Nome da spritesheet da 'skin'
    public string loadedSpriteSheetName; // Nome do spriteSheet em uso
    
    private Dictionary<string, Sprite> spriteSheetMap; // Onde vai ser a troca dentro do personagem(?)

    // Start is called before the first frame update
    void Start() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.LoadSpriteSheet();
    }

    // Update is called once per frame
    void LateUpdate() { 
        if(this.loadedSpriteSheetName != spriteSheetName) { // Se foi mudado o nome da skins
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
    /// Função que carrega todas as sprintes no spritesSkins a partir de um nome 
    /// </summary>
    private void LoadSpriteSheet() {
        this.spritesSkins = Resources.LoadAll<Sprite>(this.spriteSheetName); // Carrega as sprints
        this.spriteSheetMap = this.spritesSkins.ToDictionary(spriteSkin => spriteSkin.name, spriteSkin => spriteSkin); // Indexa as sprints pelo nome da ação
        this.loadedSpriteSheetName = this.spriteSheetName;
    }
}
