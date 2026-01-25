using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseOnScreen : MonoBehaviour
{
    [SerializeField] private float offsetFromMiddle;
    [SerializeField] private float offsetIncrease;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float steps;
    [SerializeField] private int poolSize;
    [SerializeField] private GameObject miniMouse;
    [SerializeField] private GameObject LookAt;
    [SerializeField] private Building cursorBuildingRef;
    private List<GameObject> objectPool = new List<GameObject>();

    private void Awake()
    {
        /* Makes a preset of disabled positions for mini cursors, b/c all at once will crash programs
         * poolSize = 520 since past that point, player can kinda see, but no point.
         */
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(miniMouse, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(transform, false);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetMouseObject(int index)
    {
        return objectPool[index];
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, this.rotSpeed * Time.deltaTime);

        float offset = offsetFromMiddle;
        float angleOffset = 0;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject mini = objectPool[i];

            if (i < BuildingManager.instance.NumberOfCursorBuildings(cursorBuildingRef))
            {
                mini.SetActive(true);
                float degreeToRad = ((i * steps) % 360) * Mathf.Deg2Rad + angleOffset;
                Vector3 pos = new Vector3(Mathf.Sin(degreeToRad), Mathf.Cos(degreeToRad), 0) * offset;
                mini.transform.localPosition = pos;
                
                var dir = LookAt.transform.position - mini.transform.position;
                var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                mini.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
                
                // if next one has an angle of 0, increase from base offset
                if (((i + 1) * steps) % 360 == 0)
                {
                    offset += offsetIncrease;
                    angleOffset += .5f;
                }
            }
            else
            {
                mini.SetActive(false);
            }
        
        }
    }
}
