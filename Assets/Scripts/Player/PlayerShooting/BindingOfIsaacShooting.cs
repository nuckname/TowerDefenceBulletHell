using UnityEngine;
using System.Collections;

public class BindingOfIsaacShooting : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    [Header("Shooting Settings")]
    public float shootCooldown = 0.2f;
    public int magazineSize = 6;
    public float reloadTime = 2f;

    public static bool disableShooting = false;

    private int bulletsRemaining;
    private bool isReloading = false;
    private float lastShotTime = -999f;
    private float reloadStartTime = -999f;

    [Header("Reload Bar Settings")]
    public Vector2 barSize       = new Vector2(40, 6);
    public Vector2 barOffset     = new Vector2(-70, 10);
    public Color  barBackground  = new Color(0, 0, 0, 0.6f);
    public Color  barFill        = new Color(1, 1, 1, 0.9f);

    private void Start()
    {
        bulletsRemaining = magazineSize;
        lastShotTime     = -shootCooldown;
    }

    private void Update()
    {
        if (disableShooting) 
            return;

        if (isReloading)
            return;

        if (bulletsRemaining <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Time.time >= lastShotTime + shootCooldown && Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        lastShotTime     = Time.time;
        bulletsRemaining--;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 dir = (mousePos - firePoint.position).normalized;

        var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().linearVelocity = dir * projectileSpeed;
    }

    private IEnumerator Reload()
    {
        isReloading     = true;
        reloadStartTime = Time.time;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        bulletsRemaining = magazineSize;
        isReloading      = false;
        Debug.Log("Reload complete");
    }

    public int  GetBulletsRemaining() => bulletsRemaining;
    public bool IsReloading()         => isReloading;

    private void OnGUI()
    {
        // only show during reload
        if (!isReloading || disableShooting)
            return;

        // calculate remaining reload time
        float elapsed   = Time.time - reloadStartTime;
        float remaining = Mathf.Clamp(reloadTime - elapsed, 0f, reloadTime);
        if (remaining <= 0f) 
            return;

        float fillPct = remaining / reloadTime;

        // convert mouse pos to GUI coords
        Vector2 mp     = Input.mousePosition;
        Vector2 guiPos = new Vector2(mp.x + barOffset.x,
                                     Screen.height - mp.y + barOffset.y);

        // background and fill rects
        Rect bg = new Rect(guiPos.x, guiPos.y, barSize.x, barSize.y);
        Rect fg = new Rect(guiPos.x, guiPos.y, barSize.x * fillPct, barSize.y);

        Color prev = GUI.color;
        GUI.color  = barBackground;
        GUI.DrawTexture(bg, Texture2D.whiteTexture);
        GUI.color  = barFill;
        GUI.DrawTexture(fg, Texture2D.whiteTexture);
        GUI.color  = prev;
    }
}
