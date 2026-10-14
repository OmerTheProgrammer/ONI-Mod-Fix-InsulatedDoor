using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(Door))]
[HarmonyPatch("SetSimState")]
public static class InsulatingDoor_SetSimState_Patch
{
    [HarmonyPrefix]
    public static bool Prefix(Door __instance, object[] __args)
    {
        //if the door is opening (aka __instance is am opening door) and the door is an insulating door 
        if (__instance != null && __instance.GetComponent<InsulatingDoor>() != null)
        {
            //if __args is fine and is_door_open (a bool) is sent right order and the bool is true (aka the door is open)
            if (__args != null && __args.Length > 0 && __args[0] is bool is_door_open && is_door_open)
            {
                //we prevent a func to set the door's sim state to open block so liquids and gases won't pass
                return false;
            }
        }

        //other doors will continue to work as normal - have their sim state set to open block
        return true;
    }
}