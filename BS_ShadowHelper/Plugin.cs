using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering;
using BeatmapEditor3D;
using BeatSaberMarkupLanguage.Settings;
using BS_ShadowHelper.Settings;
using System.Configuration;

namespace BS_ShadowHelper
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin : MonoBehaviour
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }



        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>

        public void Init(IPALogger logger,IPA.Config.Config conf)
        {
            Instance = this;
            Log = logger;
            Log.Info("BSLight initialized.");
            Settings.Settings.Instance = conf.Generated<Settings.Settings>();
            BSMLSettings.instance.AddSettingsMenu("BS_ShadowHelper", "BS_ShadowHelper.Configuration.settings.bsml", SettingsHandler.instance);
            if (Settings.Settings.Instance.enable == false) return;

            BS_Utils.Utilities.BSEvents.gameSceneActive += onGameScene;
            BS_Utils.Utilities.BSEvents.menuSceneActive += onMenuScene;
            //BS_Utils.Utilities.BSEvents.songPaused += onSongPaused;
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
            QualitySettings.shadowCascades = 4;

            AssetBundle assetBundle = AssetBundle.LoadFromFile("sunao.shaders");
            cutoutShader = assetBundle.LoadAsset<Shader>("Sunao_Shader_Cutout.shader");
            standardShader = assetBundle.LoadAsset<Shader>("Sunao_Shader_Cutout.shader");
            //*avatar root
            //  NalulunaAvatarsController
        }
        public static GameObject menuLightObj = null;
        public static GameObject gameLightObj = null;
        public GameObject avatarRoot = null;
        public GameObject platformRoot = null;
        public Shader cutoutShader = null, standardShader = null;

        public void refreshLight()
        {
            if (Settings.Settings.Instance.enable == false) return;
            createLight(ref menuLightObj);
            createLight(ref gameLightObj);

        }
        public void createLight(ref GameObject lightObj)
        {
            if (lightObj == null)
            {
                lightObj = new GameObject("BS_ShadowHelperLight");
                lightObj.AddComponent<Light>();
            }

            //light settings
            Light directionalLight = lightObj.GetComponent<Light>();

            lightObj.transform.SetParent(null);

            //ライトの強度
            directionalLight.intensity = Settings.Settings.Instance.intensity;

            // ライトの方向
            lightObj.transform.localEulerAngles = new Vector3(Settings.Settings.Instance.direLightRotationX, Settings.Settings.Instance.direLightRotationY, Settings.Settings.Instance.direLightRotationZ); // オプションで方向を設定

            // ライトのディレクショナルタイプ
            directionalLight.type = UnityEngine.LightType.Directional;

            // シャドウを有効化
            directionalLight.shadows = ((Settings.Settings.Instance.lightShadows=="Hard")?LightShadows.Hard:LightShadows.Soft);
            directionalLight.shadowCustomResolution = Settings.Settings.Instance.shadowResolution;
        }
        public void searchAvatarsPlatforms()
        {
            if (avatarRoot == null)
            {
                avatarRoot = GameObject.Find("NalulunaAvatarsController/PlayerRoot");
            }
            if (platformRoot == null)
            {
                platformRoot = GameObject.Find("CustomPlatforms");
            }

        }
        public void onMenuScene()
        {
            createLight(ref menuLightObj);
            searchAvatarsPlatforms();
            if (avatarRoot != null)
            {
                updateAllLayer(avatarRoot.transform, 10);
                if (Settings.Settings.Instance.overrideCustomAvatarMaterials) modifyAvatarShader(avatarRoot.transform);
            }
        }
        public void onSongPaused()
        {
            modifyPlatformShader();
        }
        public void onGameScene()
        {
            createLight(ref gameLightObj);

            searchAvatarsPlatforms();
            if (avatarRoot != null)
            {
                updateAllLayer(avatarRoot.transform, 10);
                if (Settings.Settings.Instance.overrideCustomAvatarMaterials) modifyAvatarShader(avatarRoot.transform);
            }
        }
        private void updateAllLayer(Transform parentTransform, int layer)
        {
            // 子オブジェクトの数を取得
            int childCount = parentTransform.childCount;

            // 子オブジェクトをループで処理
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parentTransform.GetChild(i);
                childTransform.gameObject.layer = layer;
                // 子オブジェクトの中にさらに子オブジェクトがある場合、再帰的に処理を続行
                updateAllLayer(childTransform, layer);
            }
        }

        private void modifyPlatformShader()
        {

            Transform activePlatform = null;
            for (int i = 0; i < platformRoot.transform.childCount; i++)
            {


                activePlatform = platformRoot.transform.GetChild(i);



                if (standardShader != null)
                {
                    var objs = activePlatform.GetComponentsInChildren<MeshRenderer>();
                    foreach (var obj in objs)
                    {
                        var objMats = obj.materials;
                        foreach (var mat in objMats)
                        {
                        }
                    }
                }
            }
        }
        private void modifyAvatarShader(Transform root)
        {
            if (standardShader != null)
            {
                var objs = root.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (var obj in objs)
                {
                    var objMats = obj.materials;
                    foreach (var mat in objMats)
                    {
                        //for customAvatar avatars.translate shader
                        if (mat.shader.name == "BeatSaber/Unlit Glow")
                        {
                            var tex = mat.GetTexture("_Tex");
                            mat.shader = standardShader;
                            mat.SetTexture("_MainTex", tex);
                        }
                    }
                }
            }
        }
        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            new GameObject("BS_ShadowHelperController").AddComponent<BS_ShadowHelperController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
