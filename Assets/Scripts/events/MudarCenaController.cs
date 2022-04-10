using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCenaController : MonoBehaviour
{

    [Header("Scripts externos")]
    private FadeScript fade;
    private _GameController _gameController;

    /// <summary>
    ///  <seealso cref="TypeScene"> Cenas </seealso> nas quais o jogador será transportado
    /// </summary>
    [Header("Configuração")]
    public TypeScene   cenaDestino;
    // Start is called before the first frame update
    void Start() {
        this.fade = FindObjectOfType(typeof(FadeScript)) as FadeScript;
        this._gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void Interacao()
    {
        StartCoroutine("MudarCena");
    }


    IEnumerator MudarCena() 
    {
        //Fade In
        this.fade.StartFadeIn();
        yield return new WaitWhile(() => this.fade.IsBlackout());
        //Fade Out
        this.fade.StartFadeOut();

        if(cenaDestino == TypeScene.SCENE_TITLE)
        {
            DestroyImmediate(_gameController.gameObject);
        }

        SceneManager.LoadScene(cenaDestino.ToString());     
    }
}
