using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceHarvesting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryManager _inventory;


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
        if (_inventory.selectedTool.Tier >= resource.Tier)
        {
            StartCoroutine(DamageResource(resource));
        }
    }

    private bool IsValidResource(RaycastHit2D hit)
    {
        return hit.collider && hit.collider.GetComponent<Resource>();
    }

    private IEnumerator DamageResource(Resource resource)
    {
        resource.ResourceBreakParticles.Play();
        Collider2D col = resource.GetComponent<Collider2D>();
        bool isResourceDepleted = resource.CurrentDmgLevel >= resource.DmgThreshold;
        col.enabled = false;
        Cursor.visible = false;
        yield return new WaitForSeconds(_inventory.selectedTool.AttackDelay);
        col.enabled = true;
        Cursor.visible = true;

        if (isResourceDepleted)
        {
            InventoryManager.AddItemAction(resource.Item, resource.ResourcePool);
            resource.DepleteResourcePool();
            resource.CurrentDmgLevel = 0;
        } else
        {
            resource.CurrentDmgLevel += _inventory.selectedTool.Damage;
        }
    }
}
