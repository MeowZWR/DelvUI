using DelvUI.Config;
using DelvUI.Config.Attributes;
using DelvUI.Helpers;
using DelvUI.Localization;
using Dalamud.Bindings.ImGui;
using Newtonsoft.Json;
using System.Numerics;

namespace DelvUI.Interface.GeneralElements
{
    [Disableable(false)]
    [Exportable(false)]
    [Section("Visibility")]
    [SubSection("Global", 0)]
    public class GlobalVisibilityConfig : PluginConfigObject
    {
        public new static GlobalVisibilityConfig DefaultConfig() { return new GlobalVisibilityConfig(); }

        [NestedConfig("Visibility", 50, collapsingHeader = false)]
        public VisibilityConfig VisibilityConfig = new VisibilityConfig();

        [JsonIgnore]
        private bool _applying = false;

        [ManualDraw]
        public bool Draw(ref bool changed)
        {
            ImGui.NewLine();

            if (ImGui.Button(LocalizationManager.Instance.Translate("Apply to all elements"), new Vector2(200, 30)))
            {
                _applying = true;
            }

            if (_applying)
            {
                string[] lines = new string[] { 
                    LocalizationManager.Instance.Translate("This will replace the visibility settings"), 
                    LocalizationManager.Instance.Translate("for ALL DelvUI elements!"), 
                    LocalizationManager.Instance.Translate("Are you sure?") 
                };
                var (didConfirm, didClose) = ImGuiHelper.DrawConfirmationModal(LocalizationManager.Instance.Translate("Apply?"), lines);

                if (didConfirm)
                {
                    ConfigurationManager.Instance.OnGlobalVisibilityChanged(VisibilityConfig);
                    changed = true;
                }

                if (didConfirm || didClose)
                {
                    _applying = false;
                }
            }

            return false;
        }
    }
}
