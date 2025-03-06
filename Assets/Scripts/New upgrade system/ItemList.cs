using UnityEngine;

public class ItemList
{
    public Item item;
    public string name;
    public int stacks;
    public GameObject bulletPrefab;

    public ItemList(Item newItem, string newName, int newStacks)
    {
        item = newItem;
        name = newName;
        stacks = newStacks;
    }
}
