using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ServiceLoader
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    
    public static void Initialize()
    {
        ServiceLocator.Initiailze();
        
        ServiceLocator.Current.Register<ISoundService>(new SoundManager());
        ServiceLocator.Current.Register<IBulletService>(new BulletManager());
        
        //SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
    }
}
