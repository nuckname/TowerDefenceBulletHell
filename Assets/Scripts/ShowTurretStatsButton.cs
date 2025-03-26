using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ShowTurretStatsButton : MonoBehaviour
{
    [SerializeField] private GameObject turretStatsPanel;
    private TurretStats turretStats;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private UpgradeUiManager upgradeUiManager;

    private bool isPanelVisible = false;

    private void Awake()
    {
    }

    private void Start()
    {
        turretStatsPanel.SetActive(false); 
    }

    public void ToggleTurretStats()
    {

        isPanelVisible = !isPanelVisible;
        turretStatsPanel.SetActive(isPanelVisible);

        if (isPanelVisible)
        {
            turretStats = upgradeUiManager.targetTurret.GetComponent<TurretStats>();
            //turretStats = upgradeUiManager.GetComponent<UpgradeUiManager>().targetTurret.GetComponent<TurretStats>();
            UpdateStatsUI();
        }
    }

    public void UpdateStatsUI()
    {
        if (!isPanelVisible)
        {
            return;
        }
        StringBuilder stats = new StringBuilder();

        // Check each stat and only add if it's different from the default value
        if (turretStats.totalAmountOfUpgrades > 0) stats.AppendLine($"<b>Total Upgrades:</b> {turretStats.totalAmountOfUpgrades}");
        if (turretStats.modifierFireRate != 0) stats.AppendLine($"<b>Fire Rate:</b> {turretStats.modifierFireRate}");
        if (turretStats.modifierBulletLifeTime != 1) stats.AppendLine($"<b>Bullet LifeTime:</b> {turretStats.modifierBulletLifeTime}");
        if (turretStats.modifierBulletSpeed != 0) stats.AppendLine($"<b>Bullet Speed:</b> {turretStats.modifierBulletSpeed}");
        if (turretStats.modifierSlowerBulletSpeed != 0) stats.AppendLine($"<b>Slower Bullet Speed:</b> {turretStats.modifierSlowerBulletSpeed}");
        if (turretStats.isTurretHoming) stats.AppendLine($"<b>Homing:</b> {turretStats.isTurretHoming}");
        if (turretStats.pierceCount > 1) stats.AppendLine($"<b>Pierce Count:</b> {turretStats.pierceCount - 1}");
        if (turretStats.angleSpread != 30f) stats.AppendLine($"<b>Angle Spread:</b> {turretStats.angleSpread}");
        if (turretStats.extraProjectiles > 1) stats.AppendLine($"<b>Extra Projectiles:</b> {turretStats.extraProjectiles}");
        if (turretStats.Shoot4Projectiles) stats.AppendLine($"<b>Shoot 4 Projectiles:</b> {turretStats.Shoot4Projectiles}");
        if (turretStats.enableBulletSplit) stats.AppendLine($"<b>Bullet Split Enabled:</b> {turretStats.enableBulletSplit}");
        if (turretStats.splitAmount > 0) stats.AppendLine($"<b>Split Amount:</b> {turretStats.splitAmount}");
        if (turretStats.multiShotCount > 1) stats.AppendLine($"<b>Multi Shot Count:</b> {turretStats.multiShotCount}");
        if (turretStats.multiShotDelay != 0.25f) stats.AppendLine($"<b>Multi Shot Delay:</b> {turretStats.multiShotDelay}");
        if (turretStats.allow4ShootPoints) stats.AppendLine($"<b>4 Shoot Points:</b> {turretStats.allow4ShootPoints}");
        if (turretStats.activeDirections > 0) stats.AppendLine($"<b>Active Directions:</b> {turretStats.activeDirections}");
        if (turretStats.allowDiagonalShooting) stats.AppendLine($"<b>Diagonal Shooting:</b> {turretStats.allowDiagonalShooting}");
        if (turretStats.amountOfBounces > 1) stats.AppendLine($"<b>Bounce Amount:</b> {turretStats.amountOfBounces}");
        if (turretStats.AllowBulletsToBounce) stats.AppendLine($"<b>Allow Bounces:</b> {turretStats.AllowBulletsToBounce}");
        if (turretStats.chainRange > 5) stats.AppendLine($"<b>Chain Range:</b> {turretStats.chainRange}");
        if (turretStats.AllowBulletsToBouncesToChain) stats.AppendLine($"<b>Bullets Chain Bounce:</b> {turretStats.AllowBulletsToBouncesToChain}");
        if (turretStats.projectilesReturn) stats.AppendLine($"<b>Return Projectiles:</b> {turretStats.projectilesReturn}");
        if (turretStats.enableOrbit) stats.AppendLine($"<b>Orbit Enabled:</b> {turretStats.enableOrbit}");
        if (turretStats.orbitRadius != 180) stats.AppendLine($"<b>Orbit Radius:</b> {turretStats.orbitRadius}");
        if (turretStats.orbitSpeed != 90) stats.AppendLine($"<b>Orbit Speed:</b> {turretStats.orbitSpeed}");
        if (turretStats.GoldOnHit) stats.AppendLine($"<b>Gold on Hit:</b> {turretStats.GoldOnHit}");
        if (turretStats.spiralBullets) stats.AppendLine($"<b>Spiral Bullets:</b> {turretStats.spiralBullets}");

        statsText.text = stats.Length > 0 ? stats.ToString() : "<b>No active upgrades.</b>";
    }
}
