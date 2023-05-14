using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private float _interactionDistance;

    private Ray _ray;
    private RaycastHit _hit;

    public event UnityAction<int> ScoreChanged;

    public int _score { get; private set; }

    private void Start()
    {
        _score = 0;
    }

    private void Update()
    {
        InteractionRay();
    }

    private void InteractionRay()
    {
        _ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        _interactionUI.SetActive(false);

        if (Physics.Raycast(_ray, out _hit, _interactionDistance))
        {
            if (_hit.collider.TryGetComponent<Paper>(out Paper paper))
            {
                _interactionUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    paper.Take();
                }
            }
        }
    }

    public void IncreasScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);

        if(_score == 8)
        {
            MenuGame.Load();
        }
    }
}
