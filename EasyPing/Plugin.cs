using BepInEx;
using Bepinject;

namespace ComputerModExample
{
    [BepInPlugin("org.kaylie.gtag.easyping", "EasyPing", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Zenjector.Install<MainInstaller>().OnProject();
        }
    }
}
