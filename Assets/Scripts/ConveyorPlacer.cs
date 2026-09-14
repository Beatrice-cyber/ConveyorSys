using UnityEngine;
using System.Collections.Generic;

//this is a class for managing conveyors
public class ConveyorPlacer : MonoBehaviour
{
    [SerializeField] private GameObject conveyorPrefab;
	[SerializeField] private LayerMask groundLayer;
    private GameObject previewConveyor;
	private List<Conveyor> placedConveyors = new List<Conveyor>();

    [SerializeField] private GameObject shortConveyorPrefab;
    [SerializeField] private GameObject inclineConveyorPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previewConveyor = Instantiate(conveyorPrefab);

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
		// starting pt, store result, how long can ray hit, ONLY hit layers in groundlayer
        if (Physics.Raycast(ray, out hit , Mathf.Infinity, groundLayer))
        {
            previewConveyor.transform.position = hit.point;
			// if (Mouse.current.leftButton.wasPressedThisFrame)
			if(Input.GetMouseButtonDown(0))
			{
				//instantiate a conveyor(of type gameobject) at the place of hit 
				GameObject conveyorObj=Instantiate(conveyorPrefab, previewConveyor.transform.position, previewConveyor.transform.rotation); 
				Conveyor placedConveyor = conveyorObj.GetComponent<Conveyor>();
				placedConveyors.Add(placedConveyor);
			}
        }

    }
}
