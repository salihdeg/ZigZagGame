using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    private Color _firstColor, _secondColor;
    private int _colorIndex, _secondColorIndex;

    [SerializeField] private Material _groundMat;

    private void Start()
    {
        _colorIndex = Random.Range(0, _colors.Length);
        _secondColorIndex = SelectSecondColor();
        _secondColor = _colors[_secondColorIndex];
        _groundMat.color = _colors[_colorIndex];
        Camera.main.backgroundColor = _secondColor;
    }

    private void Update()
    {
        Color diff = _groundMat.color - _secondColor;

        if (Mathf.Abs(diff.r) + Mathf.Abs(diff.g) + Mathf.Abs(diff.b) < 0.1f)
        {
            _secondColor = _colors[SelectSecondColor()];
        }

        _groundMat.color = Color.Lerp(_groundMat.color, _secondColor, 0.003f);
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, _secondColor, 0.002f);
    }

    private int SelectSecondColor()
    {
        int secondColorIndex = Random.Range(0, _colors.Length);
        while (secondColorIndex == _colorIndex)
        {
            secondColorIndex = Random.Range(0, _colors.Length);
        }

        return secondColorIndex;
    }
}
