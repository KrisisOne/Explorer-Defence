using UnityEngine;
using UnityEngine.SceneManagement;

public class Navegacion : MonoBehaviour {

    public void IrAMenu(){

        SceneManager.LoadScene("Menu");

    }
    public void IrAModoHistoria(){

        SceneManager.LoadScene("Game");

    }

    public void IrAMejorasRobot(){

        SceneManager.LoadScene("Mejoras");

    }

    public void IrAOpciones(){

        SceneManager.LoadScene("Opciones");

    }

}