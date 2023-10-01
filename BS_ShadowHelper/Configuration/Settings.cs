using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BS_ShadowHelper.Settings
{
    public class Settings
    {
        public static Settings Instance = new Settings();
        public bool enable = true;
        public float direLightRotationX = 30F;
        public float direLightRotationY = 0F;
        public float direLightRotationZ = 0F;
        public float intensity = 1F;

        public int shadowResolution = 10000;
        public string lightShadows = "Hard";
        public bool overrideCustomAvatarMaterials = true;
    }
}
