using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(Door))]
[HarmonyPatch("SetSimState")]
public static class InsulatingDoor_SetSimState_Patch
{
    [HarmonyPrefix]
    public static bool Prefix(Door __instance, object[] __args)
    {
        // אם הדלת הזו מכילה את רכיב ה-InsulatingDoor שלנו
        if (__instance != null && __instance.GetComponent<InsulatingDoor>() != null)
        {
            // בדיקת הערך של הפרמטר הראשון במערך הארגומנטים (מצב פתיחה/סגירה)
            if (__args != null && __args.Length > 0 && __args[0] is bool is_door_open && is_door_open)
            {
                // אנחנו עוצרים את הרצת המתודה המקורית!
                // הדלת תפתח מבחינת הניווט של הדופים והאנימציה,
                // אבל רשת הסימולציה הפיזיקלית תישאר חסומה לגזים ונוזלים לחלוטין.
                return false;
            }
        }

        // עבור דלתות רגילות או בעת סגירה - תן למתודה המקורית לרוץ כרגיל
        return true;
    }
}