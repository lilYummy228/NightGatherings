using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Player _player;

    private void OnEnable() => 
        _player.IsClicking += SetStatus;

    private void OnDisable() => 
        _player.IsClicking -= SetStatus;

    private void SetStatus(bool isAbleToSpook) =>
        _monster.SetSpookStatus(isAbleToSpook);
}
