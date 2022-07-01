using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WaveCounter : MonoBehaviour
{
    private Text _text;
    private string _label = "Wave {0}";

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        EnemySpawner.WaveCountChanged += WaveChanged;
    }

    private void OnDisable()
    {
        EnemySpawner.WaveCountChanged -= WaveChanged;
    }

    private void WaveChanged(int index)
    {
        _text.text = string.Format(_label, index + 1);
    }
}
