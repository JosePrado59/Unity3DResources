using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager sharedInstance;
    private InputModel inputControls;

    public static InputManager GetSharedInstance()
    {
        return sharedInstance;
    }

    public InputModel GetInput()
    {
        return inputControls;
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
        inputControls = new InputModel();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLocalControlMoving() || !IsRemoteControlMoving())
        {
            inputControls.XAxis = Input.GetAxisRaw("Horizontal");
            inputControls.YAxis = Input.GetAxisRaw("Vertical");
        }
        else
        {
            inputControls = RemoteInputListener.GetSharedInstance().GetInputModel();
        }
    }

    private bool IsLocalControlMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    private bool IsRemoteControlMoving()
    {
        return RemoteInputListener.GetSharedInstance().GetInputModel().XAxis != 0 ||
            RemoteInputListener.GetSharedInstance().GetInputModel().YAxis != 0;
    }
}
