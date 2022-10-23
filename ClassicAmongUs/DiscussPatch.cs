using HarmonyLib;
using UnityEngine;

namespace UltimateMods.ClassicAmongUs
{
    [HarmonyPatch]
    public static class MeetingDiscuss
    {
        public static float Timer;
        public static bool DisableTimer = false;
        public static bool EnableTimer = false;

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
        public static void Postfix()
        {
            // Discussのタイマー軌道・終了
            if (EnableTimer)
            {
                Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
                if (Timer <= 0f)
                {
                    HudManager.Instance.discussEmblem.gameObject.SetActive(true);
                    EnableTimer = false;
                    Timer = 3f;
                    DisableTimer = true;
                }
            }

            if (DisableTimer)
            {
                Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
                if (Timer <= 0f)
                {
                    HudManager.Instance.discussEmblem.gameObject.SetActive(false);
                    DisableTimer = false;
                }
            }
        }

        public static void OnMeetingEnd()
        {
            Timer = 3f;
            EnableTimer = false;
            DisableTimer = false;
            if (HudManager.Instance.discussEmblem.gameObject.active)
            {
                HudManager.Instance.discussEmblem.gameObject.SetActive(false);
            }
        }
    }
}