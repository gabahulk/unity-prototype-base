using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAnalogStickController : MonoBehaviour
{

    private float horizontal;
    private float vertical;
    private Vector2 initialTouchPosition;
    private float analogStickBackgroundRadius;
    private SpriteRenderer analogStickSpriteRenderer;

    public GameObject analogStickControler;
    public GameObject analogStick;
    public SpriteRenderer analogStickBackgroundSpriteRenderer;
    public bool mouseDebug;

    public float Horizontal { get => horizontal; set => horizontal = value; }
    public float Vertical { get => vertical; set => vertical = value; }


    // Start is called before the first frame update
    void Start()
    {
        analogStickSpriteRenderer = analogStick.GetComponent<SpriteRenderer>();
        analogStickBackgroundRadius = analogStickBackgroundSpriteRenderer.bounds.size.x / 2 - analogStickSpriteRenderer.bounds.size.x/2;
        analogStickControler.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDebug)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
                Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 inputPosition = new Vector3(worldMousePos.x, worldMousePos.y, this.transform.position.z);
                ShowAnalogStickAtPosition(inputPosition);
                initialTouchPosition = inputPosition;
                
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
                Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 inputPosition = new Vector3(worldMousePos.x, worldMousePos.y, this.transform.position.z);
                MoveStick(inputPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                horizontal = 0;
                vertical = 0;
                HideAnalogStick();
            }
        }
        else
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray))
                    {
                        Debug.Log("eita");
                    }
                }
            }
        }
        print(new Vector2(Horizontal,Vertical));
    }

    void ShowAnalogStickAtPosition(Vector3 position)
    {
        analogStickControler.transform.position = position;
        analogStickControler.SetActive(true);
    }

    void MoveStick(Vector3 position)
    {
        Vector3 direction = position - (Vector3)initialTouchPosition;

        if (Vector3.Distance(initialTouchPosition, position) > analogStickBackgroundRadius)
        {
            position = (Vector3)initialTouchPosition + (direction.normalized * analogStickBackgroundRadius);
        }
        horizontal = Mathf.Clamp(direction.x / analogStickBackgroundRadius,-1,1);
        vertical = Mathf.Clamp(direction.y / analogStickBackgroundRadius,-1,1);
        position.z = this.transform.position.z;
        analogStick.transform.position = position;
    }

    void HideAnalogStick()
    {
        analogStickControler.SetActive(false);
    }
}
