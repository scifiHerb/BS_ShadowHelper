using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using UnityEngine;

namespace BS_ShadowHelper.Settings
{
    class SettingsHandler : PersistentSingleton<SettingsHandler>
    {
        [UIValue("enable")]
        public bool showVotes
        {
            get => Settings.Instance.enable;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.enable = value;
            }
        }
        [UIValue("shadowResolution")]
        public int shadowResolution
        {
            get => Settings.Instance.shadowResolution;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.shadowResolution = value;
            }
        }

        [UIValue("shadowType-options")]
        private List<object> shadowTypeOptions = new object[] { "Hard","Soft"}.ToList();

        [UIValue("shadowType-choice")]
        private string shadowTypeChoice
        {
            get => Settings.Instance.lightShadows;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.lightShadows = value;
            }
        }
        [UIValue("direLightRotationX")]
        private float direLightRotationX
        {
            get => Settings.Instance.direLightRotationX;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.direLightRotationX = value;
            }
        }
        [UIValue("direLightRotationY")]
        private float direLightRotationY
        {
            get => Settings.Instance.direLightRotationY;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.direLightRotationY = value;
            }
        }
        [UIValue("direLightRotationZ")]
        private float direLightRotationZ
        {
            get => Settings.Instance.direLightRotationZ;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.direLightRotationZ = value;
            }
        }
        [UIValue("usingPlatLight")]
        private bool usingPlatLight
        {
            get => Settings.Instance.usingPlatLight;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.usingPlatLight = value;
            }
        }
        [UIValue("overrideCustomAvatarMaterials")]
        private bool overrideCustomAvatarMaterials
        {
            get => Settings.Instance.overrideCustomAvatarMaterials;
            set
            {
                Plugin.Instance.refreshLight();
                Settings.Instance.overrideCustomAvatarMaterials = value;
            }
        }

    }
}
