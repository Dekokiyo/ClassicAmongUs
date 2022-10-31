// using UnityEngine;
// using HarmonyLib;
// using UltimateMods.Modules;
// using UltimateMods.Utilities;

// namespace UltimateMods.Classic
// {
//     [HarmonyPatch]
//     public static class ClassicMeeting
//     {
//         public static GameObject ClassicAnimation;
//         public static GameObject CrewSprite;
//         public static SpriteRenderer AnimationRend;
//         public static SpriteRenderer CrewRend;
//         public static Material CrewShader;
//         public static float Timer;
//         public static bool DisableTimer = false;
//         public static bool EnableTimer = false;
//         public static int AnimNum = 0;

//         [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
//         public static class ClassicMeetingStart
//         {
//             public static void Prefix()
//             {
//                 FastDestroyableSingleton<HudManager>.Instance.KillOverlay.transform.gameObject.SetActive(false);

//                 CrewShader = FastDestroyableSingleton<HatManager>.Instance.PlayerMaterial;

//                 ClassicAnimation = new("ClassicAnimation");
//                 ClassicAnimation.transform.SetParent(FastDestroyableSingleton<HudManager>.Instance.transform);
//                 ClassicAnimation.transform.localPosition = new Vector3(0f, 0f, 0f);
//                 ClassicAnimation.transform.localScale = new Vector3(1.3f, 1.25f, 1f);
//                 ClassicAnimation.SetActive(true);

//                 CrewSprite = new("CrewSprite");
//                 CrewSprite.transform.SetParent(ClassicAnimation.transform);
//                 CrewSprite.transform.localPosition = new Vector3(0f, 0.4f, -0.1f);
//                 CrewSprite.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
//                 CrewSprite.SetActive(true);

//                 AnimationRend = ClassicAnimation.AddComponent<SpriteRenderer>();
//                 AnimationRend.sprite = Helpers.LoadSpriteFromTexture2D(AssetLoader.ClassicMeetingStart0, 115f);
//                 AnimNum = 1;
//             }
//         }

//         public static void DestroyObject()
//         {
//             if (ClassicAnimation != null)
//             {
//                 UnityEngine.Object.Destroy(ClassicAnimation);
//                 AnimNum = 0;
//             }
//         }

//         [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
//         public static void Postfix()
//         {
//             if (!CustomOptionsH.RememberClassic.getBool()) return;

//             if (EnableTimer)
//             {
//                 Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
//                 if (Timer <= 0f)
//                 {
//                     FastDestroyableSingleton<HudManager>.Instance.discussEmblem.transform.gameObject.SetActive(true);
//                     EnableTimer = false;
//                     Timer = 3f;
//                     DisableTimer = true;
//                 }
//             }

//             if (DisableTimer)
//             {
//                 Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
//                 if (Timer <= 0f)
//                 {
//                     FastDestroyableSingleton<HudManager>.Instance.discussEmblem.transform.gameObject.SetActive(false);
//                     DisableTimer = false;
//                 }
//             }
//         }

//         public static void OnMeetingEnd()
//         {
//             Timer = 3f;
//             EnableTimer = false;
//             DisableTimer = false;
//             FastDestroyableSingleton<HudManager>.Instance.discussEmblem.transform.gameObject.SetActive(false);
//         }

//         [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
//         public static class ClassicAnimationPatch
//         {
//             public static float Timer;
//             public static void Prefix()
//             {
//                 switch (AnimNum)
//                 {
//                     case 1:
//                         if (Timer <= 0f && ClassicAnimation != null)
//                         {
//                             if (AnimationRend != null)
//                             {
//                                 AnimationRend.sprite = Helpers.LoadSpriteFromTexture2D(AssetLoader.ClassicMeetingStart1, 115f);
//                                 Timer = 0.1f;
//                                 AnimNum++;
//                             }
//                         }
//                         Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
//                         break;
//                     case 2:
//                         if (Timer <= 0f && ClassicAnimation != null)
//                         {
//                             if (AnimationRend != null)
//                             {
//                                 AnimationRend.sprite = Helpers.LoadSpriteFromTexture2D(AssetLoader.ClassicMeetingStart2, 115f);
//                                 Timer = 0.1f;
//                                 AnimNum++;
//                             }
//                         }
//                         Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
//                         break;
//                     case 3:
//                         if (Timer <= 0f && ClassicAnimation != null)
//                         {
//                             if (AnimationRend != null)
//                             {
//                                 AnimationRend.sprite = Helpers.LoadSpriteFromTexture2D(AssetLoader.ClassicMeetingStart3, 115f);

//                                 CrewRend = CrewSprite.AddComponent<SpriteRenderer>();
//                                 CrewRend.sharedMaterial = CrewShader;
//                                 GameData.PlayerInfo playerInfo = GameData.Instance.GetPlayerById(MeetingRoomManager.Instance.target.PlayerId);
//                                 CrewRend.color = Palette.PlayerColors[playerInfo.Object.CurrentOutfit.ColorId];

//                                 if (FastDestroyableSingleton<MeetingHud>.Instance.amDead == true)
//                                 {
//                                     CrewRend.sprite = Helpers.LoadSpriteFromTexture2D(AssetLoader.ClassicMeetingDeadBodyReport, 115f);
//                                 }
//                                 else
//                                 {
//                                     CrewRend.sprite = Helpers.LoadSpriteFromTexture2D(AssetLoader.ClassicMeetingDeadBodyReport, 115f);
//                                 }
//                                 AnimNum++;
//                                 Timer = 2.2f;
//                             }
//                         }
//                         Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
//                         break;
//                     case 4:
//                         if (Timer <= 0f)
//                         {
//                             if (ClassicAnimation.transform.localScale.y > 0f && ClassicAnimation != null)
//                             {
//                                 if (CrewSprite != null)
//                                 {
//                                     CrewRend.sprite = null;
//                                 }
//                                 ClassicAnimation.transform.localScale -= new Vector3(0f, 0.25f, 0f);
//                             }
//                         }
//                         Timer = Mathf.Max(0f, Timer -= Time.deltaTime);
//                         break;
//                 }
//             }
//         }
//     }
// }