using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    private GameObject _panelMenuHud;
    private _GameController _gameController;

    private void Start()
    {
        _panelMenuHud = this.transform.GetChild(0).gameObject;
        _gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        
    }

    void Update(){
        VeriftActiveMenuPause();
    }

    public void VeriftActiveMenuPause()
    {
        this._panelMenuHud.SetActive(_gameController.CurrentState == GameState.PAUSE);
    }
}
