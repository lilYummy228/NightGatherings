using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster", order = 51)]
public class MonsterSetup : ScriptableObject
{
    [SerializeField] private float _bideMinTime = 2;
    [SerializeField] private float _bideMaxTime = 6;
    [SerializeField] private float _jumpscareDelay = 0.35f;

    public float JumpscareDelay => _jumpscareDelay;
    public float BideMinTime => _bideMinTime;
    public float BideMaxTime => _bideMaxTime;
}
