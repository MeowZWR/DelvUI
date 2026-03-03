using Dalamud.Game;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using Dalamud.Interface;
using Dalamud.Interface.Textures;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using DelvUI.Config;
using DelvUI.Config.Profiles;
using DelvUI.Helpers;
using DelvUI.Interface;
using DelvUI.Interface.GeneralElements;
using DelvUI.Interface.Nameplates;
using DelvUI.Interface.Party;
using DelvUI.Interface.PartyCooldowns;
using DelvUI.Localization;
using Dalamud.Bindings.ImGui;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DelvUI
{
    public class Plugin : IDalamudPlugin
    {
        public static IBuddyList BuddyList { get; private set; } = null!;
        public static IClientState ClientState { get; private set; } = null!;
        public static ICommandManager CommandManager { get; private set; } = null!;
        public static ICondition Condition { get; private set; } = null!;
        public static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
        public static IDataManager DataManager { get; private set; } = null!;
        public static IDutyState DutyState { get; private set; } = null!;
        public static IFramework Framework { get; private set; } = null!;
        public static IGameGui GameGui { get; private set; } = null!;
        public static IJobGauges JobGauges { get; private set; } = null!;
        public static IObjectTable ObjectTable { get; private set; } = null!;
        public static ISigScanner SigScanner { get; private set; } = null!;
        public static IGameInteropProvider GameInteropProvider { get; private set; } = null!;
        public static ITargetManager TargetManager { get; private set; } = null!;
        public static IUiBuilder UiBuilder { get; private set; } = null!;
        public static IPartyList PartyList { get; private set; } = null!;
        public static IPluginLog Logger { get; private set; } = null!;
        public static ITextureProvider TextureProvider { get; private set; } = null!;
        public static IAddonLifecycle AddonLifecycle { get; private set; } = null!;
        public static IChatGui Chat { get; private set; } = null!;

        public static ISharedImmediateTexture? BannerTexture;

        public static string AssemblyLocation { get; private set; } = "";
        public string Name => "DelvUI";

        public static string Version { get; private set; } = "";

        private HudManager _hudManager = null!;

        public delegate void JobChangedEventHandler(uint jobId);
        public static event JobChangedEventHandler? JobChangedEvent;
        private uint _jobId = 0;

        public static double LoadTime = -1;

        public Plugin(
            IBuddyList buddyList,
            IClientState clientState,
            ICommandManager commandManager,
            ICondition condition,
            IDalamudPluginInterface pluginInterface,
            IDataManager dataManager,
            IFramework framework,
            IGameGui gameGui,
            IJobGauges jobGauges,
            IObjectTable objectTable,
            IPartyList partyList,
            ISigScanner sigScanner,
            IGameInteropProvider gameInteropProvider,
            ITargetManager targetManager,
            IPluginLog logger,
            ITextureProvider textureProvider,
            IAddonLifecycle addonLifecycle,
            IChatGui chat,
            IDutyState dutyState)
        {
            BuddyList = buddyList;
            ClientState = clientState;
            CommandManager = commandManager;
            Condition = condition;
            PluginInterface = pluginInterface;
            DataManager = dataManager;
            Framework = framework;
            GameGui = gameGui;
            JobGauges = jobGauges;
            ObjectTable = objectTable;
            PartyList = partyList;
            SigScanner = sigScanner;
            GameInteropProvider = gameInteropProvider;
            TargetManager = targetManager;
            UiBuilder = PluginInterface.UiBuilder;
            Logger = logger;
            TextureProvider = textureProvider;
            AddonLifecycle = addonLifecycle;
            Chat = chat;
            DutyState = dutyState;

            if (pluginInterface.AssemblyLocation.DirectoryName != null)
            {
                AssemblyLocation = pluginInterface.AssemblyLocation.DirectoryName + "\\";
            }
            else
            {
                AssemblyLocation = Assembly.GetExecutingAssembly().Location;
            }

            Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "2.6.2.1";

            FontsManager.Initialize(AssemblyLocation);
            BarTexturesManager.Initialize(AssemblyLocation);
            LoadBanner();

            // initialize a not-necessarily-defaults configuration
            ConfigurationManager.Initialize();
            ProfilesManager.Initialize();
            ConfigurationManager.Instance.LoadOrInitializeFiles();

            FontsManager.Instance.LoadConfig();
            BarTexturesManager.Instance.LoadConfig();

            ClipRectsHelper.Initialize();
            GlobalColors.Initialize();
            InputsHelper.Initialize();
            NameplatesManager.Initialize();
            PartyManager.Initialize();
            PartyCooldownsManager.Initialize();
            PullTimerHelper.Initialize();
            TextTagsHelper.Initialize();
            TooltipsHelper.Initialize();
            PetRenamerHelper.Initialize();
            HonorificHelper.Initialize();
            WotsitHelper.Initialize();
            WhosTalkingHelper.Initialize();

            _hudManager = new HudManager();

            UiBuilder.Draw += Draw;
            UiBuilder.OpenConfigUi += OpenConfigUi;

            FontsManager.Instance.BuildFonts();

            CommandManager.AddHandler(
                "/delvui",
                new CommandInfo(PluginCommand)
                {
                    HelpMessage = "打开 DelvUI 设置窗口。\n"
                                + "/delvui toggle → 切换 HUD 显示/隐藏。\n"
                                + "/delvui show → 显示 HUD。\n"
                                + "/delvui hide → 隐藏 HUD。\n"
                                + "/delvui toggledefaulthud → 切换游戏内职业量谱显示/隐藏。\n"
                                + "/delvui forcejob <JOB> → 强制以指定职业短名显示 DelvUI HUD。\n"
                                + "/delvui profile <PROFILE> → 切换到指定配置文件。\n"
                                + "/delvui mouse <on/off> → 切换 DelvUI 元素上悬停时对多按键鼠标的支持。\n"
                                + "/delvui extract → 输出翻译分析报告。",

                    ShowInHelp = true
                }
            ); ;

            CommandManager.AddHandler(
                "/dui",
                new CommandInfo(PluginCommand)
                {
                    HelpMessage = "打开 DelvUI 设置窗口。\n"
                                + "/dui toggle → 切换 HUD 显示/隐藏。\n"
                                + "/dui show → 显示 HUD。\n"
                                + "/dui hide → 隐藏 HUD。\n"
                                + "/dui toggledefaulthud → 切换游戏内职业量谱显示/隐藏。\n"
                                + "/dui forcejob <JOB> → 强制以指定职业短名显示 DelvUI HUD。\n"
                                + "/dui profile <PROFILE> → 切换到指定配置文件。\n"
                                + "/dui mouse <on/off> → 切换 DelvUI 元素上悬停时对多按键鼠标的支持。\n"
                                + "/dui extract → 输出翻译分析报告。",

                    ShowInHelp = true
                }
            );

            WotsitHelper.Instance?.Update();

            if (ConfigurationManager.Instance?.IsChangelogWindowOpened == false)
            {
                LoadTime = ImGui.GetTime();
            }
        }

        public void Dispose()
        {
            Logger.Info("Starting DelvUI Dispose v" + Version);
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void LoadBanner()
        {
            string bannerImage = Path.Combine(Path.GetDirectoryName(AssemblyLocation) ?? "", "Media", "Images", "banner_short_x150.png");

            if (File.Exists(bannerImage))
            {
                try
                {
                    BannerTexture = TextureProvider.GetFromFile(bannerImage);
                }
                catch (Exception ex)
                {
                    Logger.Error($"Image failed to load. {bannerImage}\n\n{ex}");
                }
            }
            else
            {
                Logger.Debug($"Image doesn't exist. {bannerImage}");
            }
        }

        private void PluginCommand(string command, string arguments)
        {
            var configManager = ConfigurationManager.Instance;

            if (configManager.IsConfigWindowOpened && !configManager.LockHUD)
            {
                configManager.LockHUD = true;
            }
            else
            {
                bool printHUDStatus = false;

                switch (arguments)
                {
                    case "toggle":
                        ConfigurationManager.Instance.ShowHUD = !ConfigurationManager.Instance.ShowHUD;
                        printHUDStatus = true;
                        break;

                    case "toggledefaulthud":
                        HUDOptionsConfig config = ConfigurationManager.Instance.GetConfigObject<HUDOptionsConfig>();
                        config.HideDefaultJobGauges = !config.HideDefaultJobGauges;

                        string defaultJobGaugeStr = config.HideDefaultJobGauges ? "hidden" : "visible";
                        Chat.Print($"Default Job Gauges are {defaultJobGaugeStr}.");
                        break;

                    case "show":
                        ConfigurationManager.Instance.ShowHUD = true;
                        printHUDStatus = true;
                        break;

                    case "hide":
                        ConfigurationManager.Instance.ShowHUD = false;
                        printHUDStatus = true;
                        break;

                    case { } argument when argument.StartsWith("mouse"):
                        string[] mouseArgs = argument.Split(" ");

                        if (mouseArgs.Length > 1)
                        {
                            if (mouseArgs[1] == "on")
                            {
                                InputsHelper.Instance?.ToggleProxy(true);
                            }
                            else if (mouseArgs[1] == "off")
                            {
                                InputsHelper.Instance?.ToggleProxy(false);
                            }
                        }

                        string mouseStr = InputsHelper.Instance?.IsProxyEnabled == true ? "enabled" : "disabled";
                        Chat.Print($"DelvUI special mouse handling is currently {mouseStr}.");
                        break;

                    case { } argument when argument.StartsWith("forcejob"):
                        string[] args = argument.Split(" ");

                        if (args.Length > 0)
                        {
                            if (args[1] == "off")
                            {
                                ForcedJob.Enabled = false;
                                return;
                            }

                            var job = typeof(JobIDs).GetField(args[1].ToUpper());

                            if (job != null)
                            {
                                ForcedJob.Enabled = true;
                                ForcedJob.ForcedJobId = (uint)(job.GetValue(null) ?? JobIDs.ACN);
                            }
                        }
                        break;

                    case { } argument when argument.StartsWith("profile"):
                        string[] profile = argument.Split(" ", 2);

                        if (profile.Length > 0)
                        {
                            ProfilesManager.Instance?.CheckUpdateSwitchCurrentProfile(profile[1]);
                        }

                        break;

                    case "extract":
                    case "extracttext":
                        ExtractConfigTexts();
                        break;

                    default:
                        configManager.ToggleConfigWindow();

                        break;
                }

                if (printHUDStatus)
                {
                    string hudStr = ConfigurationManager.Instance.ShowHUD ? "visible" : "hidden";
                    Chat.Print($"DelvUI HUD is {hudStr}.");
                }
            }
        }

        private void UpdateJob()
        {
            var player = ObjectTable.LocalPlayer;
            if (player is null) { return; }

            var newJobId = player.ClassJob.RowId;
            if (ForcedJob.Enabled)
            {
                newJobId = ForcedJob.ForcedJobId;
            }

            if (_jobId != newJobId)
            {
                _jobId = newJobId;
                JobChangedEvent?.Invoke(_jobId);
            }
        }

        private void Draw()
        {
            UpdateJob();

            UiBuilder.OverrideGameCursor = false;

            ConfigurationManager.Instance.Draw();
            NameplatesManager.Instance?.Update();
            PartyManager.Instance?.Update();

            try
            {
                using (FontsManager.Instance.PushDefaultFont())
                {
                    _hudManager?.Draw(_jobId);
                }
            }
            catch (Exception e)
            {
                Logger.Error("Something went wrong!:\n" + e.StackTrace);
            }

            InputsHelper.Instance.OnFrameEnd();
        }

        private void OpenConfigUi()
        {
            ConfigurationManager.Instance.ToggleConfigWindow();
        }

        private void ExtractConfigTexts()
        {
            try
            {
                Chat.Print("[DelvUI] 正在分析翻译状态...");
                Logger.Info("开始分析翻译状态");

                var extractor = new ConfigTextExtractor();
                var result = extractor.AnalyzeTranslations();

                // 导出到配置目录
                string configDir = PluginInterface.GetPluginConfigDirectory();
                string outputPath = Path.Combine(configDir, "translation_analysis.md");
                extractor.ExportAnalysis(result, outputPath);

                // 输出精简统计
                Chat.Print($"[DelvUI] 翻译分析完成：");
                Chat.Print($"[DelvUI]   需要: {result.RequiredKeys.Count}  已翻译: {result.TranslatedKeys.Count}");
                Chat.Print($"[DelvUI]   未翻译: {result.UntranslatedKeys.Count}  未使用: {result.UnusedKeys.Count}");
                Chat.Print($"[DelvUI]   重复键: {result.DuplicateKeys.Count}  覆盖率: {result.CoveragePercentage:F1}%");
                Chat.Print($"[DelvUI] 详细报告: {outputPath}");

                Logger.Info($"翻译分析完成: 未翻译 {result.UntranslatedKeys.Count} 个, 重复键 {result.DuplicateKeys.Count} 个");
            }
            catch (Exception e)
            {
                Chat.Print($"[DelvUI] 分析失败: {e.Message}");
                Logger.Error($"分析翻译状态失败: {e.Message}\n{e.StackTrace}");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            try
            {
                InputsHelper.Instance?.Dispose();
            }
            catch (Exception e)
            {
                Logger.Error("Error disposing InputsHelper:\n" + e.StackTrace);
            }

            Logger.Info("\tSaving configurations...");
            ConfigurationManager.Instance?.SaveConfigurations(true);
            ConfigurationManager.Instance?.CloseConfigWindow();

            Logger.Info("\tDisposing HudManager...");
            _hudManager?.Dispose();

            Logger.Info("\tDisposing BarTexturesManager...");
            BarTexturesManager.Instance?.Dispose();

            Logger.Info("\tDisposing ClipRectsHelper...");
            ClipRectsHelper.Instance?.Dispose();

            Logger.Info("\tDisposing ExperienceHelper...");
            ExperienceHelper.Instance?.Dispose();

            Logger.Info("\tDisposing FontsManager...");
            FontsManager.Instance?.Dispose();

            Logger.Info("\tDisposing GlobalColors...");
            GlobalColors.Instance?.Dispose();

            Logger.Info("\tDisposing NameplatesManager...");
            NameplatesManager.Instance?.Dispose();

            Logger.Info("\tDisposing PartyCooldownsManager...");
            PartyCooldownsManager.Instance?.Dispose();

            Logger.Info("\tDisposing PartyManager...");
            PartyManager.Instance?.Dispose();

            Logger.Info("\tDisposing PullTimerHelper...");
            PullTimerHelper.Instance?.Dispose();

            Logger.Info("\tDisposing ProfilesManager...");
            ProfilesManager.Instance?.Dispose();

            Logger.Info("\tDisposing SpellHelper...");
            SpellHelper.Instance?.Dispose();

            Logger.Info("\tDisposing TooltipsHelper...");
            TooltipsHelper.Instance?.Dispose();

            Logger.Info("\tDisposing HonorificHelper...");
            HonorificHelper.Instance?.Dispose();

            Logger.Info("\tDisposing PetRenamerHelper...");
            PetRenamerHelper.Instance?.Dispose();

            Logger.Info("\tDisposing WotsitHelper...");
            WotsitHelper.Instance?.Dispose();

            Logger.Info("\tDisposing WhosTalkingHelper...");
            WhosTalkingHelper.Instance?.Dispose();

            Logger.Info("\tRemoving commands...");
            CommandManager.RemoveHandler("/delvui");
            CommandManager.RemoveHandler("/dui");

            Logger.Info("\tUnsubscribing from UIBuilder events...");
            UiBuilder.Draw -= Draw;
            UiBuilder.OpenConfigUi -= OpenConfigUi;

            Logger.Info("\tRebuilding fonts...");
            UiBuilder.FontAtlas.BuildFontsAsync();

            // This needs to remain last to avoid race conditions
            Logger.Info("\tDisposing ConfigurationManager...");
            ConfigurationManager.Instance?.Dispose();
        }
    }
}
