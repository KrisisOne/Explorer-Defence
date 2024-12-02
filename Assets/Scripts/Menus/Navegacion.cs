using UnityEngine;
using UnityEngine.SceneManagement;

public class Navegacion : MonoBehaviour {

    public void IrAMenu(){

        SceneManager.LoadScene("Menu");

    }
    public void IrAModoHistoria(){

        SceneManager.LoadScene("Seleccion_nivel");

    }

    public void IrANivel1(){

        SceneManager.LoadScene("Nivel1");

    }

    public void IrANivel2(){

        SceneManager.LoadScene("Nivel2");

    }

    public void IrANivel3(){

        SceneManager.LoadScene("Nivel3");

    }

    public void IrANivel4(){

        SceneManager.LoadScene("Nivel4");

    }

    public void IrANivel5(){

        SceneManager.LoadScene("Nivel5");

    }

    public void IrAMejorasRobot(){

        SceneManager.LoadScene("Mejoras");

    }

    public void IrAOpciones(){

        SceneManager.LoadScene("Opciones");

    }

}