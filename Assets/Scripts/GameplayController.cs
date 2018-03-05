using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public Control rabbit;
    public Text mainText;
    int contador;
    public Text counter;
    public Button reiniciar;
    public Button siguiente;

    public GameObject pauseMenu;
    public Camera mainCamera;

    AudioSource music;

    bool nexxt;
    Text next;
    bool enable;

    //Define los triggers de eliminado, finalNivel y contadorSuma. Pone el contador a 0 y los botones para reiniciar están desactivados
    void Start () {

        Time.timeScale = 1;

        rabbit.eliminado += Muerto;
        rabbit.finalNivel += Final;
        rabbit.contadorSuma += Contador;
        rabbit.enabled = true;
        mainText.text = "";
        contador = 0;
        counter.text = "0";
        reiniciar.enabled = false;
        reiniciar.GetComponentInChildren<Text>().enabled = false;
        siguiente.enabled = false;
        next = siguiente.GetComponentInChildren<Text>();
        next.enabled = false;

        pauseMenu.SetActive(false);

        music = mainCamera.GetComponentInChildren<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (Time.timeScale == 1) {
                Time.timeScale = 0;
                music.Pause();
            }
            else if (Time.timeScale == 0)
            {   
                Time.timeScale = 1;
                music.Play();
            }
        }
    }

    public void Contador()
    {
        contador++;
        counter.text = contador.ToString();
    }

    public void Reiniciar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);       
    }

    public void Muerto()
    {
        
        rabbit.setVelocity();
        rabbit.enabled = false;
        mainText.text = "Game Over";
        reiniciar.enabled = true;
        reiniciar.GetComponentInChildren<Text>().enabled = true;      
    }

    void Final(){
        
        rabbit.setVelocity();
        rabbit.enabled = false;
        mainText.text = "Victoria!";
        siguiente.enabled = true;
        if (SceneManager.GetActiveScene().name.Equals("Game2"))
        {
            next.text = "Creditos";
        }
        next.enabled = true;
    }

    public void SiguientePantalla()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("Game"))
        {
            SceneManager.LoadScene("Game2");
        }
        else if (scene.name.Equals("Game2"))
        {
            SceneManager.LoadScene("Creditos");
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
}
