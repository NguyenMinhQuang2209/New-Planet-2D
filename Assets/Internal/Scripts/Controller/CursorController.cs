using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    private string currentCursor = "";
    private List<GameObject> currents = new();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        ChangeCursor("", null);
    }
    public void ChangeCursor(string newCursor, List<GameObject> news)
    {
        if (currents != null)
        {
            foreach (GameObject current in currents)
            {
                current.SetActive(false);
            }
        }
        if (newCursor == currentCursor || newCursor == "")
        {
            currentCursor = "";
            currents = null;
        }
        else
        {
            currentCursor = newCursor;
            currents = news;
        }

        if (currents != null)
        {
            foreach (GameObject current in currents)
            {
                current.SetActive(true);
            }
        }

        Cursor.lockState = currentCursor == "" ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
