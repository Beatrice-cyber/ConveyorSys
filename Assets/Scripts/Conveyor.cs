using UnityEngine;

public class Conveyor : MonoBehaviour
{
	[SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
	//read only properties ( Arrow function that returns transform)
    public Transform StartPoint => startPoint;
    public Transform EndPoint => endPoint;
	// public Conveyor next;
	//this is to show the product's next conveyor , auto getter setter property
	public Conveyor Next { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
