public class HardMonster : Monster
{
    private void OnEnable() =>
    _wardrobe.DoorOpened += OnDoorOpened;

    private void OnDisable() =>
        _wardrobe.DoorOpened -= OnDoorOpened;

    protected virtual void OnDoorOpened() => 
        GetOut();
}