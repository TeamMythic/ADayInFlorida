using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    int daysLeft = 3;
    struct bobsInventory
    {
        public int apples;
        public int oranges;
    }
	bobsInventory bob = new bobsInventory();
    private int Main()
    {
        for (int i = daysLeft; i > 0; Harvest(ref i))
        {
        
        }



        showStats();
		return 0;
    }
    void showStats()
    {
        Debug.Log($"Bobs Apples: {bob.apples}");
        Debug.Log($"Bobs Oranges: {bob.oranges}");
    }
    void Harvest(ref int i)
    {
        i--;
        bob.apples += 15;
        bob.oranges += 20;
    }
}
