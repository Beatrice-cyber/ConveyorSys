using UnityEngine;
using System.Collections.Generic;

//this is a class for managing conveyors
public class ConveyorPlacer : MonoBehaviour
{
	[SerializeField] private GameObject conveyorPrefab;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private float SnapThreshold = 1.0f;
	private GameObject previewConveyor;
	private GameObject selectedPrefab;
	private List<Conveyor> placedConveyors = new List<Conveyor>();
	// this is to keep track of the what we are snapping so that we can update the chain
	private Conveyor CurrentSnapTarget = null;

	[SerializeField] private GameObject shortConveyorPrefab;
	[SerializeField] private GameObject inclineConveyorPrefab;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		//default is long conveyor but this can be changed to other types of conveyor
		selectedPrefab = conveyorPrefab;
		previewConveyor = Instantiate(selectedPrefab);
	}

	// Update is called once per frame
	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		// starting pt, store result, how long can ray hit, ONLY hit layers in groundlayer
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
		{
			if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectConveyor(conveyorPrefab); }
			if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectConveyor(shortConveyorPrefab); }
			if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectConveyor(inclineConveyorPrefab); }
			previewConveyor.transform.position = hit.point;
			if (placedConveyors.Count > 0)
			{
				SnapConveyors();
			}
			// if (Mouse.current.leftButton.wasPressedThisFrame)
			if (Input.GetMouseButtonDown(0))
			{
				//instantiate a conveyor(of type gameobject) at the place of hit 
				GameObject conveyorObj = Instantiate(selectedPrefab, previewConveyor.transform.position, previewConveyor.transform.rotation);
				Conveyor placedConveyor = conveyorObj.GetComponent<Conveyor>();
				placedConveyors.Add(placedConveyor);
				if(CurrentSnapTarget != null){CurrentSnapTarget.Next = placedConveyor;Debug.Log("Curr Snap target next is "+placedConveyor);}
			}
		}

	}
	private void SelectConveyor(GameObject prefab)
	{
		selectedPrefab = prefab;
		// old preview is the wrong type
		Destroy(previewConveyor);
		// create preview of newly selected type
		previewConveyor = Instantiate(selectedPrefab);
	}

	private void SnapConveyors()
	{
		// takes a current moouse position and go through the list to see if distance is < threshold 
		// to decide whether snap or not 
		//need access to previewconveyor.endpt
		CurrentSnapTarget = null;
		Conveyor preview = previewConveyor.GetComponent<Conveyor>();
		Conveyor nearest = NearestConveyor(preview);
		if (nearest == null) { return; }
		float distance = Vector3.Distance(preview.StartPoint.position, nearest.EndPoint.position);
		//&& distance < SnapThreshold
		if (distance <= SnapThreshold )
		{
			// Debug.Log("SNAPPING! Distance = " + distance);
			// snap 2 conveyors together, move preview by offset 
			Vector3 offset = nearest.EndPoint.position - preview.StartPoint.position;
			// Debug.Log("Offset = " + offset);
			previewConveyor.transform.position += offset;
			/*Debug.Log("After snap distance = " + Vector3.Distance(
			preview.StartPoint.position,
			nearest.EndPoint.position));*/
			Debug.Log("Curr Snap target"+nearest);
			CurrentSnapTarget = nearest;
		}


	}

	// returns a conveyor of minimum distance  
	private Conveyor NearestConveyor(Conveyor preview)
	{
		float minDist = Mathf.Infinity;
		Conveyor minConveyor = null;
		for (int i = 0; i < placedConveyors.Count; i++)
		{
			// this is omitting conveyers that are already connected to another convyor 
			if (placedConveyors[i] == null || placedConveyors[i].Next != null) { Debug.Log("PlacedConveys[i] or next null"); continue; }

			float dist1 = Vector3.Distance(placedConveyors[i].EndPoint.position, preview.StartPoint.position);
			// dist2 = Vector3.Distance(placedConveyors[i].StartPoint.position, preview.EndPoint.position);
			if (minDist >= dist1)
			{
				minConveyor = placedConveyors[i];
				minDist = dist1;
			}
		}
		return minConveyor;
	}
}
