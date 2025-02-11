using System;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{      
    public event Action<bool> IsDoorOpened;
    public event Action DoorOpened;

    public void InteractWithDoor(bool isOpening) => 
        IsDoorOpened.Invoke(isOpening);

    public void OpenDoor() => 
        DoorOpened?.Invoke();
}
