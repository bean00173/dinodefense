using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public enum AudioType
{
    ambient,
    music,
    effects,
    master
}

public enum GameState
{
    Loading,
    Menu,
    Preparation,
    Simulation,
}

public class AudioManager : MonoBehaviour
{
    public static Dictionary<string, AudioClip> ambient = new Dictionary<string, AudioClip>();
    public static Dictionary<string, AudioClip> music = new Dictionary<string, AudioClip>();
    public static Dictionary<string, AudioClip> effects = new Dictionary<string, AudioClip>();

    public AudioMixer masterMixer;
    public static GameState currentState { get; private set; }

    public static Dictionary<AudioType, Dictionary<string , AudioClip>> audioClips = new Dictionary<AudioType, Dictionary<string, AudioClip>>();

    public static AudioManager instance;

    public bool loaded = false;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            //Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DoLoad();
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.instance.currentScene.name);
        switch (GameManager.instance.currentScene.name)
        {
            case "MainMenu": currentState = GameState.Menu; break;
            case "LoadingScreen": currentState = GameState.Loading; break;
            default: currentState = LevelManager.instance.state == levelState.prep ? GameState.Preparation : GameState.Simulation; break;

        }

        if (GameManager.instance.currentScene.name == "TowerTest" || GameManager.instance.currentScene.name == "LevelTest")
        {

        }
    }

    private void DoLoad()
    {
        audioClips.Add(AudioType.ambient, ambient);
        audioClips.Add(AudioType.music, music);
        audioClips.Add(AudioType.effects, effects);

        //AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (KeyValuePair<AudioType, Dictionary<string, AudioClip>> kvp in audioClips)
        {
            AudioClip[] clips = Resources.LoadAll<AudioClip>($"Audio/{kvp.Key.ToString()}");
            foreach (AudioClip clip in clips)
            {
                kvp.Value.Add(clip.name, clip);
            }
        }

        loaded = true;
    }

    public AudioClip GetClip(AudioType listName, string id)
    {
        foreach(KeyValuePair<AudioType, Dictionary<string, AudioClip>> kvp in audioClips)
        {
            if(kvp.Key == listName)
            {
                foreach(KeyValuePair<string, AudioClip> kvp2 in kvp.Value)
                {
                    if(kvp2.Key == id) return kvp2.Value;
                }
            }
        }

        return null;
    }

    public AudioClip GetRandomClip(AudioType listName, string prefix)
    {
        foreach (KeyValuePair<AudioType, Dictionary<string, AudioClip>> kvp in audioClips)
        {
            if (kvp.Key == listName)
            {
                IEnumerable<string> fullMatchingKeys = kvp.Value.Keys.Where(currentKey => currentKey.Contains(prefix));

                List<AudioClip> returnedValues = new List<AudioClip>();

                foreach (string currentKey in fullMatchingKeys)
                {
                    returnedValues.Add(kvp.Value[currentKey]);
                }

                return returnedValues[Random.Range(0, returnedValues.Count)];
            }
        }

        return null;
    }

    public void SetMasterVol(float volume)
    {
        masterMixer.SetFloat("masterVol", volume);
    }
    public void SetMusicVol(float volume)
    {
        masterMixer.SetFloat("musicVol", volume);
    }
    public void SetSfxVol(float volume)
    {
        masterMixer.SetFloat("fxVol", volume);
    }
    public void SetAmbienceVol(float volume)
    {
        masterMixer.SetFloat("ambientVol", volume);
    }

}
