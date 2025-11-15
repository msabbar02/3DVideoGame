using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform player;
    public int score;
    public AudioClip[] levelSounds;
    public int levelItems; 
    public Transform contenedorDeMonedas; 
    private int totalLevelItems; 
  

 
    public TextMeshProUGUI itemsRecogidosText; 
    public TextMeshProUGUI itemsTotalText;     

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (contenedorDeMonedas != null)
        {
            totalLevelItems = contenedorDeMonedas.childCount;
        }
        else
        {
            Debug.LogError("¡Error! No has asignado el 'contenedorDeMonedas' en el LevelManager.");
            totalLevelItems = 0;
        }
        ActualizarUITexto();
    }

    public void AddLevelItem()
    {
        levelItems += 1; 
        ActualizarUITexto(); 
    }

    void ActualizarUITexto()
    {
        if (itemsRecogidosText != null)
        {
            itemsRecogidosText.text = levelItems.ToString();
        }
    
        if (itemsTotalText != null)
        {
            itemsTotalText.text = totalLevelItems.ToString();
        }
    }
    public void PlaySound(AudioClip audioClip, Vector3 position)
    {
        GameObject obj = SoundFXPooler.current.GetPooledObject();
        AudioSource source = obj.GetComponent<AudioSource>();
        obj.transform.position = position;
        obj.SetActive(true);
        source.PlayOneShot(audioClip);
        StartCoroutine(DisableSoundAfterPlay(source));
    }
    IEnumerator DisableSoundAfterPlay(AudioSource source)
    {
        while (source.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
            source.gameObject.SetActive(false);
        }
    }
}