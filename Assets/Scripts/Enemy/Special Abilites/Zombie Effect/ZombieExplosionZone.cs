using UnityEngine;

public class ZombieExplosionZone : BaseExplosionZone
{
    protected override string ZoneTag
    {
        get
        {
            return "NoTag";
        }
    }
    
    //Overrides the Base values
    protected override void Awake()
    {
        duration = 2f;
        fadeDuration = 0.5f;
        defaultStartingAlpha = 139f;

        base.Awake();
    }
    
    protected override void SetInitialAlpha()
    {
        if (spriteRenderer != null)
        {
            Color green = new Color(Color.green.r, Color.green.g, Color.green.b);
            green.a = defaultStartingAlpha / 255f;
            spriteRenderer.color = green;
        }
    }
}
