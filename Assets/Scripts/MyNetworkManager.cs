using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    AudioListener _audioListener;

    public override void Awake()
    {
        base.Awake();
        _audioListener = Camera.main.gameObject.GetComponent<AudioListener>();
    }

    public override void OnStartClient()
    {
        _audioListener.enabled = false;
        base.OnStartClient();
    }
    
    public override void OnStopClient()
    {
        base.OnStopClient();
        _audioListener.enabled = true;
    }
    
}
