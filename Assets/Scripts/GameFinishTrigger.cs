using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishTrigger : MonoBehaviour
{
    private Paper[] _papers;

    private void OnEnable()
    {
        _papers = gameObject.GetComponentsInChildren<Paper>();

        foreach (var paper in _papers) 
        {
            paper.Reached += OnPaperReached;
        }
    }

    private void OnDisable()
    {
        foreach (var paper in _papers)
        {
            paper.Reached -= OnPaperReached;
        }
    }

    private void OnPaperReached()
    {
        foreach (var paper in _papers)
        {
            if(paper.IsReached == false) 
            {
                return;
            }
        }

        Debug.Log("End Game");
    }
}
