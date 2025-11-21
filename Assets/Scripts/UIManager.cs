using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

//Clase para manejar el menu dentro del juego
public class UIManager : MonoBehaviour
{
    //Variables

    [SerializeField] private GameObject PausePanel;

    //Referencia al renderer del menu de opciones
    [SerializeField] private GameObject OptionsMenu;

    //Referencia al renderer del panel de configuraciones
    [SerializeField] private GameObject SettingsPanel;
    
    //Referencia al boton de reinicio
    [SerializeField]private Button RestartButton;

    //Referencia al boton de Opciones
    [SerializeField] private Button MenuButton;

    //Variable de control, maneja estado de pausa
    private bool isPaused = false;

    //Dropdown de calidad grafica
    [SerializeField]public TMP_Dropdown QualityDropdown;

    [SerializeField] public Slider masterVolume, musicVolume, sfxVolume;

    public AudioMixer audioMixer;


    //Se ejecuta al iniciar el juego
    void Start()
    {
        //Asegurarse que ambos menus esten ocultos al iniciar el juego
        if(OptionsMenu != null) OptionsMenu.SetActive(false);
        if(SettingsPanel != null) SettingsPanel.SetActive(false);
        if(PausePanel != null) PausePanel.SetActive(false);
    }

    //Metodo para manejar el boton de Menu
    public void onMenuPress()
    {
        // Alternamos el estado de pausa (si estaba pausado, se reanuda, y viceversa)
        isPaused = !isPaused;

        //Pausamos o reanudamos el tiempo de juego (0 = pausado, 1 = normal)
        Time.timeScale = isPaused ? 0 : 1f;

        //Mostramos u ocultamos el menu de opciones segun el estado de pausa
        if (OptionsMenu != null) OptionsMenu.SetActive(isPaused);

        //Asegurarse que el panel de configuraciones este oculto al abrir el menu
        if(SettingsPanel != null) SettingsPanel.SetActive(false);

        //Asegurarse que el panel oscuro al hacer pausa sea visible
        if(PausePanel != null) PausePanel.SetActive(isPaused);

        //Deshabilitar el boton de reinicio mientras el juego este pausado
        if(RestartButton != null) RestartButton.interactable = !isPaused;
    }

    //Metodo para manejar el boton de  Reinicio
    public void onRestartPress()
    {
        //Reanudar el juego por si estaba pausado
        Time.timeScale = 1f;
        //Recargamos la escena actual(reiniciar)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Metodo para manejar el boton de  Reanudar
    public void onResumePress()
    {
        //Inicializar estado como no pausado
        isPaused = false;
        //Reanuda el tiempo del juego
        Time.timeScale = 1f;

        //Oculta el panel del menu de opciones
        if(OptionsMenu != null) OptionsMenu.SetActive(false);

        //Oculta el panel de configuraciones
        if(SettingsPanel != null) SettingsPanel.SetActive(false);

        if(PausePanel != null) PausePanel.SetActive(false);
    }

    //Metodo para manejar el boton de Configuraciones (audio, controles, video,etc)
    public void onSettingsPress()
    {
        //Ocultar el menu de opciones
        if(OptionsMenu != null) OptionsMenu.SetActive(false);

        //Mostrar el panel de configuraciones
        if(SettingsPanel != null) SettingsPanel.SetActive(true);
    }

    //Metodo para manejar el boton de regresar a las opciones del menu
    public void onSettingsBackPress()
    {
        //Ocultar el panel de configuraciones
        if(SettingsPanel != null) SettingsPanel.SetActive(false);

        //Mostrar el menu de opciones
        if(OptionsMenu != null) OptionsMenu.SetActive(true);
    }

    public void onChangeQuality()
    {
        QualitySettings.SetQualityLevel(QualityDropdown.value);
    }

    public void onHomePress()
    {
        
    }

    public void onExitPress()
    {
        Application.Quit();
    }

    public void changeMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void changeMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void changeSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", volume);
    }
}