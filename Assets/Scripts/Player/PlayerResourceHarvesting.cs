using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceHarvesting : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HarvestResource();
        }
    }

    private void HarvestResource()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (!IsValidResource(hit)) return;

        Resource resource = hit.collider.GetComponent<Resource>();
        if (resource.ResourcePool > 0)
        {
            InventoryManager.AddItemAction(resource.Item, 1);
            resource.DepleteResourcePool(1);
        }
    }

    private bool IsValidResource(RaycastHit2D hit)
    {
        return hit.collider && hit.collider.GetComponent<Resource>();
    }
}
