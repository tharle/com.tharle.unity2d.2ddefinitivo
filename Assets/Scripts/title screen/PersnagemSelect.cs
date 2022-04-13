using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using System.Linq;

public class PersnagemSelect : MonoBehaviour
{
    [Header("Scripts externos")]
    private _DataBaseController _dataBase;
    private TitleSelectScript _titleSelect;
    

    [Header("Configuracoes de botão")]
    public Personagem.Index personagemIndex;
    private Color selectColor = Color.green;
    private Color unSelectColor = Color.gray;
    private Personagem personagem;
    private Image image;
    private Dictionary<string, Sprite> spriteSheetMap;


    // Start is called before the first frame update
    void Start()
    {
        _dataBase = FindObjectOfType(typeof(_DataBaseController)) as _DataBaseController;
        _titleSelect = FindObjectOfType(typeof(TitleSelectScript)) as TitleSelectScript;
        personagem = _dataBase.BuscarPersonagem(personagemIndex);
        image = GetComponent<Image> ();

        LoadText();
        LoadSpriteSheet();
        LoadSpriteImage();
    }

    public void LoadText()
    {
     //   textMesh.text = personagem.Nome;
    }

    public void LoadSpriteImage()
    {
        this.image.sprite = spriteSheetMap["idle_1"];
    }

    /// <summary>
    /// Função que carrega todas as sprintes no spritesSkins a partir de um nome
    /// </summary>
    private void LoadSpriteSheet() {
        var spritesSkins = Resources.LoadAll<Sprite>(personagem.SpritesName); // Carrega as sprints da configuracao setada manualmente
        
        spriteSheetMap = spritesSkins.ToDictionary(spriteSkin => spriteSkin.name, spriteSkin => spriteSkin); // Indexa as sprints pelo nome da ação
    }
}
