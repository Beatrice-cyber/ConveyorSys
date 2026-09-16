using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject canPrefab;

    void Update()
    {
        // Hover over conveyor + B = box
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnProduct(boxPrefab);
        }

        // Hover over conveyor + C = can
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnProduct(canPrefab);
        }
    }

    private void SpawnProduct(GameObject productPrefab)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Conveyor conveyor = hit.collider.GetComponent<Conveyor>();

            if (conveyor == null)
            {
                return;
            }

            // 1. Create product
            GameObject productObj = Instantiate(productPrefab);

            // 2. Get its movement script
            ProductMover mover = productObj.GetComponent<ProductMover>();

            // 3. Tell it which conveyor to start on
            mover.SetConveyor(conveyor);
        }
    }
}