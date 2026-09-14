using UnityEngine;

public class Conveyor : MonoBehaviour
{
	[SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
	//read only properties ( Arrow function that returns transform)
    public Transform StartPoint => startPoint;
    public Transform EndPoint => endPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
