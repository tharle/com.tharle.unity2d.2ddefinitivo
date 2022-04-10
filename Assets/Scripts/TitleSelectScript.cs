using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TitleSelectScript : MonoBehaviour
{

    public Personagem.Index personagemIndex;

    public void SelectionarPersonagem(string personagemIndexString)
    {
        Personagem.Index personagemIndexTemp;
        if (!Enum.TryParse(personagemIndexString, out personagemIndexTemp))
        {
            personagemIndex = Personagem.Index.BAR_BORI_BLUECLAWS;
            return;
        }

        if(personagemIndexTemp == personagemIndex){
            LoadScene();  
            return;
        }

        personagemIndex = personagemIndexTemp;

    }

    public void LoadScene()
    {
        PlayerPrefs.SetString(PlayerPrefsConst.PERSONAGEM_INDEX, personagemIndex.ToString());
        SceneManager.LoadScene(TypeScene.SCENE_FIRST.ToString());
    }
}
