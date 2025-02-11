using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score;

    public event Action<int> ScoreChanged;

    public void Add() => 
        ScoreChanged?.Invoke(++_score);
}
