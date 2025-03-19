public class HardMonster : Monster
{
    protected override void OnDoorOpened() => 
        GetOut();
}