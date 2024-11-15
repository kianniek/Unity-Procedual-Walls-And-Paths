using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathwayManager : MonoBehaviour
{
    public GameObject PathwayPrefab;

    //enum input.getbuttondown strings
    public enum ButtonDownStrings
    {
        button0,
        button1,
        button2,
    }

    public ButtonDownStrings buttonTypeDown;

    [SerializeField] List<SplineGenerator> splines;
    // Start is called before the first frame update
    void Start()
    {
        splines = new List<SplineGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CursorHandler.Instance == null) { return; }
        if (CursorHandler.Instance.GetCurrentCursorType() != CursorHandler.CursorType.PlacingPath)
        {
            return;
        }

        Instatiate();
    }

    void Instatiate()
    {
        int button = 0;

        switch (buttonTypeDown)
        {
            case ButtonDownStrings.button0:
                button = 0;
                break;
            case ButtonDownStrings.button1:
                button = 1;
                break;
            case ButtonDownStrings.button2:
                button = 2;
                break;
        }

        if (Input.GetMouseButtonDown(button))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit))
            {
                return;
            }

            print("Drawing");
            GameObject go = Instantiate(PathwayPrefab, transform.position, transform.rotation, transform);
            splines.Add(go.GetComponent<SplineGenerator>());
        }
    }

    public List<SplineGenerator> GetSplines()
    {
        return splines;
    }
}