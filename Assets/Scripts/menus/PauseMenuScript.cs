using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    [Header("Paineis disponiveis")]
    public GameObject panelMenu;
    public GameObject panelItens;

    [Header("Variaveis para salvar status anterior")]
    private GameState _oldGameState;

    [Header("Objetos externos")]
    private _GameController _gameController;

    private void Start()
    {
        _gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        panelMenu.SetActive(false);
        panelItens.SetActive(false);
    }

    void Update(){
        VerifyGameState();
    }

    private void VerifyGameState()
    {
        if(_oldGameState == _gameController.CurrentGameState) return;
        
        _oldGameState = _gameController.CurrentGameState;

        this.panelMenu.SetActive(_gameController.CurrentGameState == GameState.PAUSE_MENU); // Abre o menu principal
        this.panelItens.SetActive(_gameController.CurrentGameState == GameState.PAUSE_ITENS); // Abre o menu de inventario
    }

    public void OnClickCancelButton()
    {
        _gameController.CallCloseEvent();
    }

    public void onClickMenuItens()
    {
        _gameController.CurrentGameState = GameState.PAUSE_ITENS;
    }

    public void onClickMenuOptions()
    {
        _gameController.CurrentGameState = GameState.GAME_PLAY;
    }

    public void onClickMenuStatus()
    {
        _gameController.CurrentGameState = GameState.GAME_PLAY;
    }
}
