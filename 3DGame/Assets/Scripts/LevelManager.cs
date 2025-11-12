using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform player;
    public int score;
    public int levelItems;
    public AudioClip[] levelSounds;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
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
