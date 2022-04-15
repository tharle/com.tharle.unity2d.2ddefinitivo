using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [Header("Variaveis para salvar status anterior")]
    private GameState _oldGameState;

    [Header("Objetos externos")]
    private _GameController _gameController;

    [Header("Paineis disponiveis")]
    public GameObject PanelMenu;
    public GameObject PanelItens;

    [Header("Primeiro Elemento de Cada painel")]
    public Button FirstPanelMenuButton;
    public Button FirstPanelItensButton;

    private void Start()
    {
        _gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        PanelMenu.SetActive(false);
        PanelItens.SetActive(false);
    }

    void Update(){
        VerifyGameState();
    }

    private void VerifyGameState()
    {
        if(_oldGameState == _gameController.CurrentGameState) return;
        
        _oldGameState = _gameController.CurrentGameState;

    
        this.PanelMenu.SetActive(_gameController.CurrentGameState == GameState.PAUSE_MENU); // Abre o menu principal
        this.PanelItens.SetActive(_gameController.CurrentGameState == GameState.PAUSE_ITENS ); // Abre o inventario
        SelectFirstButton();
    }

    private void SelectFirstButton()
    {
        if(_gameController.CurrentGameState == GameState.PAUSE_MENU ) FirstPanelMenuButton.Select();
        if(_gameController.CurrentGameState == GameState.PAUSE_ITENS ) FirstPanelItensButton.Select();
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
