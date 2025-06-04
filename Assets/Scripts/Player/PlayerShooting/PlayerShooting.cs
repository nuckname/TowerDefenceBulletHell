using UnityEngine;
using System.Collections;
using Unity.Netcode;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerShooting : NetworkBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;   // Must have a NetworkObject + ProjectileNetworked component
    public Transform firePoint;
    public float projectileSpeed = 10f;

    [Header("Shooting Settings")]
    public float shootCooldown = 0.2f;
    public int magazineSize = 6;
    public float reloadTime = 2f;

    public static bool disableShooting;

    private int bulletsRemaining;
    private bool isReloading = false;
    private float lastShotTime = -999f;
    private float reloadStartTime = -999f;

    [Header("Reload Bar Settings")]
    public Vector2 barSize       = new Vector2(40, 6);
    public Vector2 barOffset     = new Vector2(10, 10);
    public Color  barBackground  = new Color(0, 0, 0, 0.6f);
    public Color  barFill        = new Color(1, 1, 1, 0.9f);

    public TutorialStateSO tutorialStateSO;

    private void Start()
    {
        bulletsRemaining = magazineSize;
        lastShotTime     = -shootCooldown;
    }

    private void Update()
    {
        // 1) If this is not *your* player, bail out immediately
        if (!IsOwner) 
            return;

        // 2) Global disable?
        if (disableShooting) 
            return;

        // 3) If pointer over UI button, don’t shoot
        if (IsPointerOverUIButton()) 
            return;

        // 4) If pointer is clicking on a turret in the world, don’t shoot
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit     = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Turret"))
            return;

        // 5) If currently reloading, skip
        if (isReloading)
            return;

        // 6) If out of bullets, kick off reload
        if (bulletsRemaining <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // 7) If enough time has passed and left‐mouse button is held, shoot
        if (Time.time >= lastShotTime + shootCooldown && Input.GetMouseButton(0))
        {
            // Record local cooldowns/UI first
            lastShotTime = Time.time;
            bulletsRemaining--;

            // Then ask the server to actually spawn a projectile
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector2 dir = (mousePos - firePoint.position).normalized;

            // Send spawn request to server
            ShootServerRpc(firePoint.position, dir);
        }
    }

    /// <summary>
    /// This ServerRpc runs on the server. It instantiates + spawns the projectile,
    /// then tells the new projectile what velocity to use.
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    private void ShootServerRpc(Vector2 spawnPosition, Vector2 direction, ServerRpcParams rpcParams = default)
    {
        // 1) Instantiate on the server/host
        GameObject proj = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // 2) Grab the NetworkObject and call Spawn()
        var netObj = proj.GetComponent<NetworkObject>();
        if (netObj == null)
        {
            Debug.LogError("[PlayerShooting] projectilePrefab is missing a NetworkObject!");
            Destroy(proj);
            return;
        }

        netObj.Spawn(true);

        // 3) Compute the actual velocity vector
        Vector2 velocity = direction * projectileSpeed;

        // 4) Tell the projectile network‐script to initialize its velocity
        var networkedProjectile = proj.GetComponent<ProjectileNetworked>();
        if (networkedProjectile != null)
        {
            networkedProjectile.InitializeOnServer(velocity);
        }
        else
        {
            Debug.LogError("[PlayerShooting] projectilePrefab needs a ProjectileNetworked component!");
        }
    }

    private IEnumerator Reload()
    {
        isReloading     = true;
        reloadStartTime = Time.time;
        yield return new WaitForSeconds(reloadTime);
        bulletsRemaining = magazineSize;
        isReloading      = false;
    }

    public int  GetBulletsRemaining() => bulletsRemaining;
    public bool IsReloading()         => isReloading;

    private void OnGUI()
    {
        // only show the bar while reloading
        if (!isReloading || disableShooting)
            return;

        float elapsed   = Time.time - reloadStartTime;
        float remaining = Mathf.Clamp(reloadTime - elapsed, 0f, reloadTime);
        if (remaining <= 0f) 
            return;

        float fillPct = remaining / reloadTime;

        Vector2 mp     = Input.mousePosition;
        Vector2 guiPos = new Vector2(mp.x + barOffset.x,
                                     Screen.height - mp.y + barOffset.y);

        Rect bg = new Rect(guiPos.x, guiPos.y, barSize.x, barSize.y);
        Rect fg = new Rect(guiPos.x, guiPos.y, barSize.x * fillPct, barSize.y);

        Color prev = GUI.color;
        GUI.color  = barBackground;
        GUI.DrawTexture(bg, Texture2D.whiteTexture);
        GUI.color  = barFill;
        GUI.DrawTexture(fg, Texture2D.whiteTexture);
        GUI.color  = prev;
    }

    //— helper that returns true if the pointer is over any UI Button
    private bool IsPointerOverUIButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) 
            return false;

        var ped     = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);

        foreach (var r in results)
        {
            if (r.gameObject.TryGetComponent<UnityEngine.UI.Button>(out _))
                return true;
        }
        return false;
    }
}
