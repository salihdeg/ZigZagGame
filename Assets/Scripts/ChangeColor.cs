using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    private Color _firstColor, _secondColor;
    private int _colorIndex;

    [SerializeField] private Material _groundMat;

    private void Start()
    {
        _colorIndex = Random.Range(0, _colors.Length);
        _groundMat.color = _colors[_colorIndex];
        Camera.main.backgroundColor = _colors[_colorIndex];
    }
}
