using UnityEngine;

public class SelectMapManager : MonoBehaviour
{
    public GamemodeSettingsSO gamemodeSettingsSO;
    //This is for SelectVaritaion Manager ceebs changing it to us SO.
    public MapType mapSelected;
    public enum MapType
    {
        SnowMap,
        DesertMap,
        RuinMap
    }

    public void SnowMapSelected()
    {
        gamemodeSettingsSO.mapSelected = MapType.SnowMap;
        mapSelected = MapType.SnowMap;
    }
    
    public void RuinMapSelected()
    {
        gamemodeSettingsSO.mapSelected = MapType.RuinMap;
        mapSelected = MapType.RuinMap;
    }
    
    public void DesertMapSelected()
    {
        gamemodeSettingsSO.mapSelected = MapType.DesertMap;
        mapSelected = MapType.DesertMap;
    }
}
