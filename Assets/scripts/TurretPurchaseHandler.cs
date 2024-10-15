using UnityEngine;

public class TurretPurchaseHandler : MonoBehaviour
{
    private const int WEAPON_PRICE = 100;

    public void BuyTurret(int turretType, GameObject lastSelectedObject, GameManager gameManager, bool blueTeam)
    {
        if (lastSelectedObject == null) return;

        var lastSelectedTile = lastSelectedObject.GetComponent<Tile>();
        var lastSelectedComponent = lastSelectedObject.GetComponent<ObjectStats>();

        if (lastSelectedTile != null && lastSelectedComponent != null && lastSelectedComponent.hover && !lastSelectedTile.activeConstruction)
        {
            if (CanAffordTurret(gameManager, blueTeam))
            {
                lastSelectedTile.activeConstruction = true;
                gameManager.AddCoins(-WEAPON_PRICE, blueTeam);
            }
        }
    }

    private bool CanAffordTurret(GameManager gameManager, bool blueTeam)
    {
        return blueTeam ? gameManager.blueCoins >= WEAPON_PRICE : gameManager.redCoins >= WEAPON_PRICE;
    }
}