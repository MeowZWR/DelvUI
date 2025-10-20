using System.Collections.Generic;

namespace DelvUI.Localization
{
    public enum Language
    {
        English,
        ChineseSimplified
    }

    public class LocalizationManager
    {
        private static LocalizationManager? _instance;
        public static LocalizationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalizationManager();
                }
                return _instance;
            }
        }

        private Language _currentLanguage = Language.ChineseSimplified;
        private Dictionary<string, Dictionary<Language, string>> _translations;
        private Dictionary<string, int> _duplicateDefinitions; // 追踪重复定义

        private LocalizationManager()
        {
            _translations = new Dictionary<string, Dictionary<Language, string>>();
            _duplicateDefinitions = new Dictionary<string, int>();
            InitializeTranslations();
        }

        public void SetLanguage(Language language)
        {
            _currentLanguage = language;
        }

        public string Translate(string key)
        {
            // 如果没有找到翻译，返回原始键值
            if (!_translations.TryGetValue(key, out var translations))
            {
                return key;
            }

            // 如果当前语言没有翻译，返回英文或原始键值
            if (!translations.TryGetValue(_currentLanguage, out var translation))
            {
                if (translations.TryGetValue(Language.English, out var englishTranslation))
                {
                    return englishTranslation;
                }
                return key;
            }

            return translation;
        }

        private void InitializeTranslations()
        {
            // Sections - 主分类
            AddTranslation("Unit Frames", "情报框体", "Unit Frames");
            AddTranslation("Mana Bars", "法力条", "Mana Bars");
            AddTranslation("Castbars", "咏唱栏", "Castbars");
            AddTranslation("Buffs and Debuffs", "增益与减益", "Buffs and Debuffs");
            AddTranslation("Nameplates", "角色名牌", "Nameplates");
            AddTranslation("Party Frames", "小队框体", "Party Frames");
            AddTranslation("Party Cooldowns", "小队冷却", "Party Cooldowns");
            AddTranslation("Enemy List", "敌人列表", "Enemy List");
            AddTranslation("Job Specific Bars", "职业专用量表", "Job Specific Bars");
            AddTranslation("Other Elements", "其他元素", "Other Elements");
            AddTranslation("Colors", "颜色", "Colors");
            AddTranslation("Customization", "自定义", "Customization");
            AddTranslation("Visibility", "可见性", "Visibility");
            AddTranslation("Misc", "杂项", "Misc");
            AddTranslation("Import", "导入", "Import");

            // SubSections - Unit Frames
            AddTranslation("Player", "玩家", "Player");
            AddTranslation("Target", "目标", "Target");
            AddTranslation("Target of Target", "目标的目标", "Target of Target");
            AddTranslation("Focus Target", "焦点目标", "Focus Target");

            // SubSections - Buffs and Debuffs
            AddTranslation("Player Buffs", "玩家增益", "Player Buffs");
            AddTranslation("Player Debuffs", "玩家减益", "Player Debuffs");
            AddTranslation("Target Buffs", "目标增益", "Target Buffs");
            AddTranslation("Target Debuffs", "目标减益", "Target Debuffs");
            AddTranslation("Focus Target Buffs", "焦点目标增益", "Focus Target Buffs");
            AddTranslation("Focus Target Debuffs", "焦点目标减益", "Focus Target Debuffs");
            AddTranslation("Custom Effects", "自定义效果", "Custom Effects");

            // SubSections - Nameplates
            AddTranslation("General", "通用", "General");
            AddTranslation("Enemies", "敌人", "Enemies");
            AddTranslation("Party Members", "小队成员", "Party Members");
            AddTranslation("Alliance Members", "团队成员", "Alliance Members");
            AddTranslation("Friends", "好友", "Friends");
            AddTranslation("Other Players", "其他玩家", "Other Players");
            AddTranslation("Pets", "宠物", "Pets");
            AddTranslation("NPCs", "NPC", "NPCs");
            AddTranslation("Minions", "迷你宠物", "Minions");
            AddTranslation("Objects", "物体", "Objects");

            // SubSections - Party Frames
            AddTranslation("Health Bar", "生命值条", "Health Bar");
            AddTranslation("Health Bars", "生命值条", "Health Bars");
            AddTranslation("Mana Bar", "法力条", "Mana Bar");
            AddTranslation("Castbar", "咏唱栏", "Castbar");
            AddTranslation("Icons", "图标", "Icons");
            AddTranslation("Buffs", "增益", "Buffs");
            AddTranslation("Debuffs", "减益", "Debuffs");
            AddTranslation("Trackers", "追踪器", "Trackers");
            AddTranslation("Cooldowns", "冷却", "Cooldowns");
            AddTranslation("Cooldown List", "冷却列表", "Cooldown List");

            // SubSections - Party Cooldowns
            AddTranslation("Cooldown Bar", "冷却条", "Cooldown Bar");
            AddTranslation("Cooldowns Tracked", "追踪的冷却", "Cooldowns Tracked");

            // SubSections - Enemy List
            AddTranslation("Enmity Icon", "仇恨图标", "Enmity Icon");
            AddTranslation("Sign Icon", "标记图标", "Sign Icon");

            // SubSections - Job Specific Bars (Tanks)
            AddTranslation("Paladin", "骑士", "Paladin");
            AddTranslation("Warrior", "战士", "Warrior");
            AddTranslation("Dark Knight", "暗黑骑士", "Dark Knight");
            AddTranslation("Gunbreaker", "绝枪战士", "Gunbreaker");

            // SubSections - Job Specific Bars (Healers)
            AddTranslation("White Mage", "白魔法师", "White Mage");
            AddTranslation("Scholar", "学者", "Scholar");
            AddTranslation("Astrologian", "占星术士", "Astrologian");
            AddTranslation("Sage", "贤者", "Sage");

            // SubSections - Job Specific Bars (Melee)
            AddTranslation("Monk", "武僧", "Monk");
            AddTranslation("Dragoon", "龙骑士", "Dragoon");
            AddTranslation("Ninja", "忍者", "Ninja");
            AddTranslation("Samurai", "武士", "Samurai");
            AddTranslation("Reaper", "钐镰客", "Reaper");
            AddTranslation("Viper", "蝰蛇剑士", "Viper");

            // SubSections - Job Specific Bars (Ranged)
            AddTranslation("Bard", "吟游诗人", "Bard");
            AddTranslation("Machinist", "机工士", "Machinist");
            AddTranslation("Dancer", "舞者", "Dancer");

            // SubSections - Job Specific Bars (Casters)
            AddTranslation("Black Mage", "黑魔法师", "Black Mage");
            AddTranslation("Summoner", "召唤师", "Summoner");
            AddTranslation("Red Mage", "赤魔法师", "Red Mage");
            AddTranslation("Blue Mage", "青魔法师", "Blue Mage");
            AddTranslation("Pictomancer", "绘灵法师", "Pictomancer");

            // SubSections - Other Elements
            AddTranslation("Experience Bar", "经验条", "Experience Bar");
            AddTranslation("GCD Indicator", "GCD指示器", "GCD Indicator");
            AddTranslation("Pull Timer", "倒计时", "Pull Timer");
            AddTranslation("Limit Break", "极限技", "Limit Break");
            AddTranslation("MP Ticker", "MP跳蓝", "MP Ticker");

            // SubSections - Colors & Job Categories
            AddTranslation("Tank", "防护", "Tank");
            AddTranslation("Tanks", "防护职业", "Tanks");
            AddTranslation("Healer", "治疗", "Healer");
            AddTranslation("Healers", "治疗职业", "Healers");
            AddTranslation("Melee", "近战", "Melee");
            AddTranslation("Ranged", "远程物理", "Ranged");
            AddTranslation("Caster", "远程魔法", "Caster");
            AddTranslation("Casters", "远程魔法职业", "Casters");
            AddTranslation("Roles", "职能", "Roles");

            // SubSections - Customization
            AddTranslation("Fonts", "字体", "Fonts");
            AddTranslation("Bar Textures", "条材质", "Bar Textures");

            // SubSections - Visibility
            AddTranslation("Global", "全局", "Global");
            AddTranslation("Global Visibility", "全局可见性", "Global Visibility");
            AddTranslation("Hotbars", "热键栏", "Hotbars");
            AddTranslation("Hotbars Visibility", "热键栏可见性", "Hotbars Visibility");

            // SubSections - Misc
            AddTranslation("HUD Options", "HUD选项", "HUD Options");
            AddTranslation("Window Clipping", "窗口裁剪", "Window Clipping");
            AddTranslation("Tooltips", "工具提示", "Tooltips");
            AddTranslation("Grid", "网格", "Grid");

            // ==================================================================
            // 配置项标签 - 常用基础项
            // ==================================================================
            
            // 基础开关和可见性
            AddTranslation("Enabled", "启用", "Enabled");
            AddTranslation("Enable", "启用", "Enable");
            AddTranslation("Visible", "可见", "Visible");
            AddTranslation("Show", "显示", "Show");
            AddTranslation("Hide", "隐藏", "Hide");
            AddTranslation("Hide When Inactive", "不活动时隐藏", "Hide When Inactive");
            
            // 通用按钮
            AddTranslation("Export", "导出", "Export");
            AddTranslation("Reset", "重置", "Reset");
            AddTranslation("Save", "保存", "Save");
            AddTranslation("Load", "加载", "Load");
            AddTranslation("Copy", "复制", "Copy");
            AddTranslation("Paste", "粘贴", "Paste");
            AddTranslation("Delete", "删除", "Delete");
            AddTranslation("Rename", "重命名", "Rename");
            
            // 配置文件管理
            AddTranslation("Profile Name", "配置名称", "Profile Name");
            AddTranslation("Create a new profile:", "创建新配置：", "Create a new profile:");
            AddTranslation("Attach HUD Layout to this profile", "将HUD布局附加到此配置", "Attach HUD Layout to this profile");
            AddTranslation("Export to Clipboard", "导出到剪贴板", "Export to Clipboard");
            AddTranslation("Export to File", "导出到文件", "Export to File");
            AddTranslation("Import From Clipboard", "从剪贴板导入", "Import From Clipboard");
            AddTranslation("Import From File", "从文件导入", "Import From File");
            AddTranslation("Profile export string copied to clipboard!", "配置导出字符串已复制到剪贴板！", "Profile export string copied to clipboard!");
            AddTranslation("Please type a name for the new profile!", "请为新配置输入一个名称！", "Please type a name for the new profile!");
            AddTranslation("Are you sure you want to delete the profile:", "确定要删除此配置吗：", "Are you sure you want to delete the profile:");
            AddTranslation("Are you sure you want to reset the profile:", "确定要重置此配置吗：", "Are you sure you want to reset the profile:");
            AddTranslation("Delete?", "删除？", "Delete?");
            AddTranslation("Reset?", "重置？", "Reset?");
            AddTranslation("Type a new name for the profile:", "为配置输入一个新名称：", "Type a new name for the profile:");
            AddTranslation("Auto-Switch For Specific Jobs", "为特定职业自动切换", "Auto-Switch For Specific Jobs");
            
            // 窗口裁剪
            AddTranslation("WARNING!", "警告！", "WARNING!");
            AddTranslation("THIS FEATURE IS KNOWN TO CAUSE RANDOM", "已知此功能可能导致随机", "THIS FEATURE IS KNOWN TO CAUSE RANDOM");
            AddTranslation("CRASHES TO A SMALL PORTION OF USERS!!!", "崩溃（影响少数用户）！！！", "CRASHES TO A SMALL PORTION OF USERS!!!");
            AddTranslation("Are you sure you want to enable it?", "确定要启用吗？", "Are you sure you want to enable it?");
            AddTranslation("Mode: ", "模式：", "Mode: ");
            AddTranslation("Performance", "性能", "Performance");
            AddTranslation("Enable special clipping for Nameplates", "为角色名牌启用特殊裁剪", "Enable special clipping for Nameplates");
            AddTranslation("When enabled, Nameplates will get covered by game UI elements that wouldn't normally cover DelvUI elements.", "启用后，游戏UI元素会遮挡角色名牌（通常不会遮挡DelvUI元素）。", "When enabled, Nameplates will get covered by game UI elements that wouldn't normally cover DelvUI elements.");
            AddTranslation("Default Target Castbar", "默认目标咏唱条", "Default Target Castbar");
            AddTranslation("When enabled, the game's target castbar will not be covered by DelvUI Nameplates.\nFor players that prefer to use the default target cast bar over DelvUI's.", "启用后，游戏的目标咏唱条不会被DelvUI角色名牌遮挡。\n适合喜欢使用默认咏唱条而非DelvUI咏唱条的玩家。", "When enabled, the game's target castbar will not be covered by DelvUI Nameplates.\nFor players that prefer to use the default target cast bar over DelvUI's.");
            AddTranslation("When enabled, active hotbar will not be covered by DelvUI Nameplates.\nNote that the way this is calculated is not perfect and it might not work well for hotbars that have empty slots.", "启用后，激活的热键栏不会被DelvUI角色名牌遮挡。\n注意：此计算方式并不完美，对于有空槽位的热键栏可能无法正常工作。", "When enabled, active hotbar will not be covered by DelvUI Nameplates.\nNote that the way this is calculated is not perfect and it might not work well for hotbars that have empty slots.");
            AddTranslation("NPC Chat Bubbles", "NPC聊天气泡", "NPC Chat Bubbles");
            AddTranslation("Player Chat Bubbles", "玩家聊天气泡", "Player Chat Bubbles");
            AddTranslation("Enable clipping for other plugins", "为其他插件启用裁剪", "Enable clipping for other plugins");
            AddTranslation("When enabled, other plugins' windows can also be clipped so DelvUI elements don't cover them.\nPlease note that this requires the developer of each third party plugin to implement the feature.", "启用后，其他插件的窗口也可以被裁剪，以防止DelvUI元素遮挡它们。\n请注意，这需要每个第三方插件的开发者实现此功能。", "When enabled, other plugins' windows can also be clipped so DelvUI elements don't cover them.\nPlease note that this requires the developer of each third party plugin to implement the feature.");
            AddTranslation("DelvUI will attempt to not cover game windows in this mode by clipping around them.", "DelvUI将尝试通过在游戏窗口周围裁剪来避免遮挡它们。", "DelvUI will attempt to not cover game windows in this mode by clipping around them.");
            AddTranslation("DelvUI will attempt to not cover game windows in this mode by not drawing an element if its touching a game window.", "DelvUI将尝试通过不绘制接触到游戏窗口的元素来避免遮挡它们。", "DelvUI will attempt to not cover game windows in this mode by not drawing an element if its touching a game window.");
            AddTranslation("Window Clipping functionality will be reduced in favor of performance.\nOnly one game window will be clipped at a time. This might yield unexpected / ugly results.\n\nNote: This mode won't work well with Nameplates.", "窗口裁剪功能将降低以提高性能。\n一次只裁剪一个游戏窗口。这可能会产生意外/不美观的结果。\n\n注意：此模式在角色名牌上效果不佳。", "Window Clipping functionality will be reduced in favor of performance.\nOnly one game window will be clipped at a time. This might yield unexpected / ugly results.\n\nNote: This mode won't work well with Nameplates.");
            AddTranslation("If you're experiencing random crashes or bad performance, we recommend you try a different mode\nor disable Window Clipping altogether", "如果您遇到随机崩溃或性能问题，建议您尝试其他模式\n或完全禁用窗口裁剪", "If you're experiencing random crashes or bad performance, we recommend you try a different mode\nor disable Window Clipping altogether");
            
            // 全局可见性
            AddTranslation("Apply to all elements", "应用到所有元素", "Apply to all elements");
            AddTranslation("This will replace the visibility settings", "这将替换可见性设置", "This will replace the visibility settings");
            AddTranslation("for ALL DelvUI elements!", "应用于所有DelvUI元素！", "for ALL DelvUI elements!");
            AddTranslation("Are you sure?", "确定吗？", "Are you sure?");
            AddTranslation("Apply?", "应用？", "Apply?");
            
            // 字体配置
            AddTranslation("Support Chinese", "支持中文", "Support Chinese");
            AddTranslation("Support Korean", "支持韩文", "Support Korean");
            AddTranslation("Support Cyrillic", "支持西里尔文", "Support Cyrillic");
            AddTranslation("Are you sure you want to apply this font", "确定要应用此字体吗", "Are you sure you want to apply this font");
            AddTranslation("to all labels using a font with the same size?", "到所有使用相同大小字体的标签？", "to all labels using a font with the same size?");
            AddTranslation("Apply to all labels?", "应用到所有标签？", "Apply to all labels?");
            
            // 条材质配置
            AddTranslation("Select Bar Textures Folder", "选择条材质文件夹", "Select Bar Textures Folder");
            AddTranslation("Custom Bar Textures path", "自定义条材质路径", "Custom Bar Textures path");
            AddTranslation("Preview", "预览", "Preview");
            AddTranslation("Bar Texture", "条材质", "Bar Texture");
            AddTranslation("Draw Mode", "绘制模式", "Draw Mode");
            AddTranslation("Stretch", "拉伸", "Stretch");
            AddTranslation("Repeat Horizontal", "水平重复", "Repeat Horizontal");
            AddTranslation("Repeat Vertical", "垂直重复", "Repeat Vertical");
            AddTranslation("Repeat", "重复", "Repeat");
            AddTranslation("Apply to all bars", "应用到所有条", "Apply to all bars");
            AddTranslation("This will replace the Bar Texture", "这将替换条材质", "This will replace the Bar Texture");
            AddTranslation("and Draw Mode for ALL bars!", "和所有条的绘制模式！", "and Draw Mode for ALL bars!");
            AddTranslation("THIS CAN'T BE UNDONE!", "此操作无法撤销！", "THIS CAN'T BE UNDONE!");
            AddTranslation("Apply to ALL bars?", "应用到所有条？", "Apply to ALL bars?");
            
            // 位置和尺寸
            AddTranslation("Position", "位置", "Position");
            AddTranslation("Size", "尺寸", "Size");
            AddTranslation("Scale", "缩放", "Scale");
            AddTranslation("Anchor", "锚点", "Anchor");
            AddTranslation("Width", "宽度", "Width");
            AddTranslation("Height", "高度", "Height");
            
            // 颜色相关
            AddTranslation("Color", "颜色", "Color");
            AddTranslation("Background Color", "背景颜色", "Background Color");
            AddTranslation("Border Color", "边框颜色", "Border Color");
            AddTranslation("Fill Color", "填充颜色", "Fill Color");
            AddTranslation("Text Color", "文字颜色", "Text Color");
            
            // 透明度
            AddTranslation("Alpha", "透明度", "Alpha");
            AddTranslation("Background Alpha", "背景透明度", "Background Alpha");
            AddTranslation("Additional Alpha", "额外透明度", "Additional Alpha");
            
            // 边框和背景
            AddTranslation("Show Border", "显示边框", "Show Border");
            AddTranslation("Draw Border", "绘制边框", "Draw Border");
            AddTranslation("Border Thickness", "边框粗细", "Border Thickness");
            AddTranslation("Show Background", "显示背景", "Show Background");
            AddTranslation("Enable Nameplate Backdrop", "启用角色名牌背景", "Enable Nameplate Backdrop");
            
            // 文本和标签
            AddTranslation("Text", "文本", "Text");
            AddTranslation("Label", "标签", "Label");
            AddTranslation("Font", "字体", "Font");
            AddTranslation("Font Size", "字体大小", "Font Size");
            AddTranslation("Font Key", "字体键", "Font Key");
            
            // 对齐
            AddTranslation("Text Alignment", "文字对齐", "Text Alignment");
            AddTranslation("Horizontal Alignment", "水平对齐", "Horizontal Alignment");
            AddTranslation("Vertical Alignment", "垂直对齐", "Vertical Alignment");
            
            // 增长方向
            AddTranslation("Fill Direction", "填充方向", "Fill Direction");
            AddTranslation("Growth Direction", "增长方向", "Growth Direction");
            AddTranslation("Stack Direction", "堆叠方向", "Stack Direction");
            
            // 阈值和限制
            AddTranslation("Threshold", "阈值", "Threshold");
            AddTranslation("Threshold Value", "阈值", "Threshold Value");
            AddTranslation("Limit", "限制", "Limit");
            AddTranslation("Max", "最大", "Max");
            AddTranslation("Min", "最小", "Min");
            
            // 速度和时间
            AddTranslation("Velocity", "速度", "Velocity");
            AddTranslation("Duration", "持续时间", "Duration");
            AddTranslation("Cooldown", "冷却时间", "Cooldown");
            
            // 图标相关
            AddTranslation("Icon Size", "图标大小", "Icon Size");
            AddTranslation("Icon Position", "图标位置", "Icon Position");
            AddTranslation("Show Icon", "显示图标", "Show Icon");
            AddTranslation("Icon Options", "图标选项", "Icon Options");
            
            // 间距和边距
            AddTranslation("Spacing", "间距", "Spacing");
            AddTranslation("Padding", "内边距", "Padding");
            AddTranslation("Gap", "间隙", "Gap");
            AddTranslation("Offset", "偏移", "Offset");
            
            // ==================================================================
            // 复选框选项 (181个) - 第1批：常用可见性选项
            // ==================================================================
            
            AddTranslation("Always Show", "始终显示", "Always Show");
            AddTranslation("Always show nameplate for target", "始终显示目标的角色名牌", "Always show nameplate for target");
            AddTranslation("Always show when crafting", "制作时始终显示", "Always show when crafting");
            AddTranslation("Always show when gathering", "采集时始终显示", "Always show when gathering");
            AddTranslation("Always show when in duty", "在副本中始终显示", "Always show when in duty");
            AddTranslation("Always show when weapon is drawn", "拔刀时始终显示", "Always show when weapon is drawn");
            AddTranslation("Always show while in a party", "组队时始终显示", "Always show while in a party");
            AddTranslation("Always show while in Island Sanctuary", "在无人岛时始终显示", "Always show while in Island Sanctuary");
            
            AddTranslation("Use Job Color", "使用职业颜色", "Use Job Color");
            AddTranslation("Use Role Color", "使用职能颜色", "Use Role Color");
            AddTranslation("Use Class Job Category for coloring", "使用职业分类着色", "Use Class Job Category for coloring");
            
            AddTranslation("Draw Backdrop", "绘制背景", "Draw Backdrop");
            AddTranslation("Enable Backdrop", "启用背景", "Enable Backdrop");
            AddTranslation("Clip", "裁剪", "Clip");
            
            // 数值显示选项
            AddTranslation("Show Health Value", "显示生命值数值", "Show Health Value");
            AddTranslation("Show Mana Value", "显示魔力值数值", "Show Mana Value");
            AddTranslation("Show Shield Value", "显示护盾值", "Show Shield Value");
            AddTranslation("Show Percentage", "显示百分比", "Show Percentage");
            AddTranslation("Show Current and Max Values", "显示当前值和最大值", "Show Current and Max Values");
            
            // 名称和标题
            AddTranslation("Show Name", "显示名称", "Show Name");
            AddTranslation("Show Title", "显示称号", "Show Title");
            AddTranslation("Show Job Name", "显示职业名称", "Show Job Name");
            AddTranslation("Show Level", "显示等级", "Show Level");
            
            // 状态和效果
            AddTranslation("Show Debuffs", "显示减益", "Show Debuffs");
            AddTranslation("Show Buffs", "显示增益", "Show Buffs");
            AddTranslation("Show Status Effects", "显示状态效果", "Show Status Effects");
            AddTranslation("Show Stacks", "显示层数", "Show Stacks");
            AddTranslation("Show Duration", "显示持续时间", "Show Duration");
            AddTranslation("Show Cooldown", "显示冷却", "Show Cooldown");
            
            // 咏唱相关
            AddTranslation("Show Cast Time", "显示咏唱时间", "Show Cast Time");
            AddTranslation("Show Castbar", "显示咏唱条", "Show Castbar");
            AddTranslation("Interruptible Color", "可打断颜色", "Interruptible Color");
            AddTranslation("Show Cast Icon", "显示技能图标", "Show Cast Icon");
            
            // 距离和范围
            AddTranslation("Show Range", "显示距离", "Show Range");
            AddTranslation("Show Distance", "显示距离", "Show Distance");
            AddTranslation("Highlight When in Range", "在范围内时高亮", "Highlight When in Range");
            
            // 目标和焦点
            AddTranslation("Show Target Marker", "显示目标标记", "Show Target Marker");
            AddTranslation("Show Focus Target", "显示焦点目标", "Show Focus Target");
            AddTranslation("Highlight Target", "高亮目标", "Highlight Target");
            
            // 特殊效果
            AddTranslation("Smooth Health", "平滑生命值", "Smooth Health");
            AddTranslation("Use Smooth Transitions", "使用平滑过渡", "Use Smooth Transitions");
            AddTranslation("Enable Glow", "启用发光", "Enable Glow");
            AddTranslation("Enable Pulse", "启用脉冲", "Enable Pulse");
            
            // ==================================================================
            // 更多配置项 - 第2批
            // ==================================================================
            
            // GCD和技能相关
            AddTranslation("GCD Threshold", "GCD阈值", "GCD Threshold");
            AddTranslation("Estimated Fire III Cast Time", "预估爆炎咏唱时间", "Estimated Fire III Cast Time");
            
            // 血量相关
            AddTranslation("Low Health Color Below Health %", "低生命值颜色阈值（低于%）", "Low Health Color Below Health %");
            AddTranslation("Max Health Color Above Health %", "满生命值颜色阈值（高于%）", "Max Health Color Above Health %");
            AddTranslation("Use Health Color", "使用生命值颜色", "Use Health Color");
            AddTranslation("Color By Health Value", "根据生命值着色", "Color By Health Value");
            
            // 遮挡和可见性
            AddTranslation("Occlusion Mode", "遮挡模式", "Occlusion Mode");
            AddTranslation("Occlusion Type", "遮挡类型", "Occlusion Type");
            AddTranslation("Try to keep nameplates on screen", "尝试将角色名牌保持在屏幕内", "Try to keep nameplates on screen");
            
            // 小队框体相关
            AddTranslation("Show Party Pets", "显示小队宠物", "Show Party Pets");
            AddTranslation("Show Chocobos", "显示陆行鸟", "Show Chocobos");
            AddTranslation("Show Role Icon", "显示职能图标", "Show Role Icon");
            AddTranslation("Show Job Icon", "显示职业图标", "Show Job Icon");
            AddTranslation("Show Leader Icon", "显示队长图标", "Show Leader Icon");
            AddTranslation("Show Sign Icon", "显示标记图标", "Show Sign Icon");
            
            // 范围相关
            AddTranslation("In Range Alpha", "范围内透明度", "In Range Alpha");
            AddTranslation("Out of Range Alpha", "范围外透明度", "Out of Range Alpha");
            AddTranslation("Show Out of Range Members", "显示范围外的成员", "Show Out of Range Members");
            
            // 排序相关
            AddTranslation("Order", "顺序", "Order");
            AddTranslation("Sort Order", "排序", "Sort Order");
            AddTranslation("Limit Rows", "限制行数", "Limit Rows");
            AddTranslation("Limit Columns", "限制列数", "Limit Columns");
            
            // 填充相关
            AddTranslation("Fill Row First", "优先填充行", "Fill Row First");
            AddTranslation("Fill Column First", "优先填充列", "Fill Column First");
            
            // 格式化相关
            AddTranslation("Use Abbreviated Values", "使用缩写值", "Use Abbreviated Values");
            AddTranslation("Show Thousands", "显示千位", "Show Thousands");
            AddTranslation("Decimal Precision", "小数精度", "Decimal Precision");
            
            // Dalamud样式
            AddTranslation("Override Dalamud Style", "覆盖Dalamud样式", "Override Dalamud Style");
            AddTranslation("Use Regional Number Formats", "使用区域数字格式", "Use Regional Number Formats");
            
            // 游戏UI相关
            AddTranslation("Hide Default Job Gauges", "隐藏默认职业量谱", "Hide Default Job Gauges");
            AddTranslation("Hide Dalamud Tooltips", "隐藏Dalamud提示", "Hide Dalamud Tooltips");
            
            // 经验条
            AddTranslation("Show Rested EXP", "显示休息经验", "Show Rested EXP");
            AddTranslation("Rested EXP Color", "休息经验颜色", "Rested EXP Color");
            
            // MP跳蓝
            AddTranslation("Show Tick Marker", "显示跳蓝标记", "Show Tick Marker");
            AddTranslation("Show Fire III Threshold", "显示爆炎阈值", "Show Fire III Threshold");
            
            // 咏唱条
            AddTranslation("Show Spell Name", "显示技能名称", "Show Spell Name");
            AddTranslation("Show Interrupt Shield", "显示防打断", "Show Interrupt Shield");
            AddTranslation("Slide Cast Time", "滑步咏唱时间", "Slide Cast Time");
            
            // 状态效果
            AddTranslation("Show Permanent Buffs", "显示永久增益", "Show Permanent Buffs");
            AddTranslation("Show Permanent Debuffs", "显示永久减益", "Show Permanent Debuffs");
            AddTranslation("Show Tooltips", "显示提示", "Show Tooltips");
            AddTranslation("Limit Duration", "限制持续时间", "Limit Duration");
            
            // 冷却追踪
            AddTranslation("Track Only Specific Roles", "仅追踪特定职能", "Track Only Specific Roles");
            AddTranslation("Track Tanks", "追踪防护", "Track Tanks");
            AddTranslation("Track Healers", "追踪治疗", "Track Healers");
            AddTranslation("Track DPS", "追踪输出", "Track DPS");
            
            // 敌人列表
            AddTranslation("Row Count", "行数", "Row Count");
            AddTranslation("Row Size", "行高", "Row Size");
            AddTranslation("Show Enmity Number", "显示仇恨数值", "Show Enmity Number");
            AddTranslation("Preview with Target's Enemies", "使用目标的敌人预览", "Preview with Target's Enemies");
            
            // 条纹理
            AddTranslation("Texture", "材质", "Texture");
            AddTranslation("Chunk Texture", "块材质", "Chunk Texture");
            AddTranslation("Texture Offset", "材质偏移", "Texture Offset");
            
            // 阴影
            AddTranslation("Show Shadow", "显示阴影", "Show Shadow");
            AddTranslation("Shadow Color", "阴影颜色", "Shadow Color");
            AddTranslation("Shadow Offset", "阴影偏移", "Shadow Offset");
            
            // 渐变
            AddTranslation("Use Gradient", "使用渐变", "Use Gradient");
            AddTranslation("Gradient Direction", "渐变方向", "Gradient Direction");
            AddTranslation("Start Color", "起始颜色", "Start Color");
            AddTranslation("End Color", "结束颜色", "End Color");
            
            // 描边
            AddTranslation("Enable Outline", "启用描边", "Enable Outline");
            AddTranslation("Outline Color", "描边颜色", "Outline Color");
            AddTranslation("Outline Thickness", "描边粗细", "Outline Thickness");
            
            // 窗口裁剪
            AddTranslation("Enable Clipping", "启用裁剪", "Enable Clipping");
            AddTranslation("Clip Party Frames", "裁剪小队框体", "Clip Party Frames");
            AddTranslation("Window Title", "窗口标题", "Window Title");
            
            // 目标切换
            AddTranslation("Switch Target on Left Click", "左键切换目标", "Switch Target on Left Click");
            AddTranslation("Switch Target on Right Click", "右键切换目标", "Switch Target on Right Click");
            
            // 数值和比例
            AddTranslation("Value", "数值", "Value");
            AddTranslation("Percentage", "百分比", "Percentage");
            AddTranslation("Current", "当前", "Current");
            AddTranslation("Maximum", "最大", "Maximum");
            
            // ==================================================================
            // 大批量翻译 - 第3批：更多配置项
            // ==================================================================
                        
            // 更多显示相关
            AddTranslation("Show Own Buffs Only", "仅显示自己的增益", "Show Own Buffs Only");
            AddTranslation("Show Own Debuffs Only", "仅显示自己的减益", "Show Own Debuffs Only");
            AddTranslation("Show Only Dispellable Debuffs", "仅显示可驱散的减益", "Show Only Dispellable Debuffs");
            AddTranslation("Show Only Removable Buffs", "仅显示可移除的增益", "Show Only Removable Buffs");
            AddTranslation("Show Party Number", "显示小队编号", "Show Party Number");
            AddTranslation("Show HP", "显示HP", "Show HP");
            AddTranslation("Show MP", "显示MP", "Show MP");
            AddTranslation("Show Only When Active", "仅在激活时显示", "Show Only When Active");
            AddTranslation("Show Only In Combat", "仅在战斗中显示", "Show Only In Combat");
            AddTranslation("Show Only In Duty", "仅在副本中显示", "Show Only In Duty");
            AddTranslation("Show Only When Out of Combat", "仅在非战斗时显示", "Show Only When Out of Combat");
            AddTranslation("Show Progress", "显示进度", "Show Progress");
            AddTranslation("Show Progress Swipe", "显示进度扇形", "Show Progress Swipe");
            AddTranslation("Show Remaining Time", "显示剩余时间", "Show Remaining Time");
            AddTranslation("Show Stack Count", "显示层数", "Show Stack Count");
            AddTranslation("Show Text", "显示文字", "Show Text");
            AddTranslation("Show Value", "显示数值", "Show Value");
            AddTranslation("Show When Empty", "空时显示", "Show When Empty");
            AddTranslation("Show When Full", "满时显示", "Show When Full");
            AddTranslation("Show When Target is Self", "目标是自己时显示", "Show When Target is Self");
            
            // 数值格式化
            AddTranslation("Format", "格式", "Format");
            AddTranslation("Number Format", "数字格式", "Number Format");
            AddTranslation("Show Decimals", "显示小数", "Show Decimals");
            AddTranslation("Use K/M Format", "使用K/M格式", "Use K/M Format");
            AddTranslation("Use Shortened Values", "使用缩写值", "Use Shortened Values");
            
            // 布局相关
            AddTranslation("Column Spacing", "列间距", "Column Spacing");
            AddTranslation("Layout", "布局", "Layout");
            AddTranslation("Orientation", "方向", "Orientation");
            AddTranslation("Row Spacing", "行间距", "Row Spacing");
            
            // 限制相关
            AddTranslation("Duration Limit", "持续时间限制", "Duration Limit");
            AddTranslation("Max Duration", "最大持续时间", "Max Duration");
            AddTranslation("Max Rows", "最大行数", "Max Rows");
            AddTranslation("Max Columns", "最大列数", "Max Columns");
            AddTranslation("Min Duration", "最小持续时间", "Min Duration");
            AddTranslation("Time Limit", "时间限制", "Time Limit");
            
            // 小队相关
            AddTranslation("Include Pets", "包括宠物", "Include Pets");
            AddTranslation("Only Show Removable", "仅显示可移除", "Only Show Removable");
            AddTranslation("Party Sorting", "小队排序", "Party Sorting");
            AddTranslation("Show Offline", "显示离线", "Show Offline");
            AddTranslation("Show Self", "显示自己", "Show Self");
            
            // 目标和选中
            AddTranslation("Focus", "焦点", "Focus");
            AddTranslation("Focused", "已选中", "Focused");
            AddTranslation("Mouseover", "鼠标悬停", "Mouseover");
            AddTranslation("Selected", "已选择", "Selected");
            AddTranslation("Selection", "选择", "Selection");
            
            // 咏唱和技能
            AddTranslation("Cast Bar", "咏唱条", "Cast Bar");
            AddTranslation("Casting", "咏唱中", "Casting");
            AddTranslation("Interruptible", "可打断", "Interruptible");
            AddTranslation("Slide Cast", "滑步咏唱", "Slide Cast");
            AddTranslation("Spell", "技能", "Spell");
            AddTranslation("Uninterruptible", "不可打断", "Uninterruptible");
            
            // 生命值和资源
            AddTranslation("Current HP", "当前HP", "Current HP");
            AddTranslation("Current MP", "当前MP", "Current MP");
            AddTranslation("HP Percentage", "HP百分比", "HP Percentage");
            AddTranslation("Max HP", "最大HP", "Max HP");
            AddTranslation("Max MP", "最大MP", "Max MP");
            AddTranslation("Missing HP", "缺失HP", "Missing HP");
            AddTranslation("MP Percentage", "MP百分比", "MP Percentage");
            AddTranslation("Shield", "护盾", "Shield");
            AddTranslation("Shield Percentage", "护盾百分比", "Shield Percentage");
            
            // 增益和减益
            AddTranslation("Beneficial", "有益", "Beneficial");
            AddTranslation("Blacklist", "黑名单", "Blacklist");
            AddTranslation("Custom Buffs", "自定义增益", "Custom Buffs");
            AddTranslation("Custom Debuffs", "自定义减益", "Custom Debuffs");
            AddTranslation("Effect", "效果", "Effect");
            AddTranslation("Effects", "效果", "Effects");
            AddTranslation("Harmful", "有害", "Harmful");
            AddTranslation("Permanent", "永久", "Permanent");
            AddTranslation("Removable", "可移除", "Removable");
            AddTranslation("Status", "状态", "Status");
            AddTranslation("Whitelist", "白名单", "Whitelist");
            
            // 冷却
            AddTranslation("Cooldown Duration", "冷却时间", "Cooldown Duration");
            AddTranslation("Cooldown Text", "冷却文字", "Cooldown Text");
            AddTranslation("Show Cooldown Animation", "显示冷却动画", "Show Cooldown Animation");
            AddTranslation("Show Cooldown Number", "显示冷却数字", "Show Cooldown Number");
            AddTranslation("Show Cooldown Spiral", "显示冷却螺旋", "Show Cooldown Spiral");
            
            // 锚点和对齐
            AddTranslation("Bottom", "底部", "Bottom");
            AddTranslation("Bottom Left", "左下", "Bottom Left");
            AddTranslation("Bottom Right", "右下", "Bottom Right");
            AddTranslation("Center Left", "左中", "Center Left");
            AddTranslation("Center Right", "右中", "Center Right");
            AddTranslation("Top", "顶部", "Top");
            AddTranslation("Top Left", "左上", "Top Left");
            AddTranslation("Top Right", "右上", "Top Right");
            
            // 增长和填充方向
            AddTranslation("Down to Up", "从下到上", "Down to Up");
            AddTranslation("Left to Right", "从左到右", "Left to Right");
            AddTranslation("Right to Left", "从右到左", "Right to Left");
            AddTranslation("Up to Down", "从上到下", "Up to Down");
            
            // 特定功能
            AddTranslation("Absorb", "吸收", "Absorb");
            AddTranslation("Animation", "动画", "Animation");
            AddTranslation("Bar", "条", "Bar");
            AddTranslation("Chunks", "块", "Chunks");
            AddTranslation("Combo", "连击", "Combo");
            AddTranslation("Counter", "计数器", "Counter");
            AddTranslation("Damage", "伤害", "Damage");
            AddTranslation("Dead", "死亡", "Dead");
            AddTranslation("Distance", "距离", "Distance");
            AddTranslation("Enemy", "敌人", "Enemy");
            AddTranslation("Enmity", "仇恨", "Enmity");
            AddTranslation("Experience", "经验", "Experience");
            AddTranslation("Friendly", "友好", "Friendly");
            AddTranslation("Gauge", "量谱", "Gauge");
            AddTranslation("Gauges", "量谱", "Gauges");
            AddTranslation("Inactive", "不活动", "Inactive");
            AddTranslation("Indicator", "指示器", "Indicator");
            AddTranslation("Job Gauge", "职业量谱", "Job Gauge");
            AddTranslation("Leader", "队长", "Leader");
            AddTranslation("Level", "等级", "Level");
            AddTranslation("Marker", "标记", "Marker");
            AddTranslation("Member", "成员", "Member");
            AddTranslation("Members", "成员", "Members");
            AddTranslation("Nameplate", "角色名牌", "Nameplate");
            AddTranslation("Number", "数字", "Number");
            AddTranslation("Offline", "离线", "Offline");
            AddTranslation("Online", "在线", "Online");
            AddTranslation("Pet", "宠物", "Pet");
            AddTranslation("Pull", "开怪", "Pull");
            AddTranslation("Rested", "休息", "Rested");
            AddTranslation("Role", "职能", "Role");
            AddTranslation("Sign", "标记", "Sign");
            AddTranslation("Stance", "姿态", "Stance");
            AddTranslation("Status Effect", "状态效果", "Status Effect");
            AddTranslation("Status Effects", "状态效果", "Status Effects");
            AddTranslation("Swipe", "扇形", "Swipe");
            AddTranslation("Tick", "跳", "Tick");
            AddTranslation("Title", "称号", "Title");
            AddTranslation("Tooltip", "提示", "Tooltip");
            
            // ==================================================================
            // 第4批：颜色配置项（批量翻译，格式：xxx Color → xxx颜色）
            // ==================================================================
            
            // 基础颜色
            AddTranslation("Absorb Color", "吸收颜色", "Absorb Color");
            AddTranslation("Active Color", "激活颜色", "Active Color");
            AddTranslation("Bar Color", "条颜色", "Bar Color");
            AddTranslation("Base Color", "基础颜色", "Base Color");
            AddTranslation("Buff Color", "增益颜色", "Buff Color");
            AddTranslation("Cast Color", "咏唱颜色", "Cast Color");
            AddTranslation("Chunk Color", "块颜色", "Chunk Color");
            AddTranslation("Cooldown Color", "冷却颜色", "Cooldown Color");
            AddTranslation("Debuff Color", "减益颜色", "Debuff Color");
            AddTranslation("Empty Color", "空颜色", "Empty Color");
            AddTranslation("Full Color", "满颜色", "Full Color");
            AddTranslation("Health Bar Color", "生命值条颜色", "Health Bar Color");
            AddTranslation("Health Color", "生命值颜色", "Health Color");
            AddTranslation("Icon Border Color", "图标边框颜色", "Icon Border Color");
            AddTranslation("Inactive Color", "不活动颜色", "Inactive Color");
            AddTranslation("Label Color", "标签颜色", "Label Color");
            AddTranslation("Low Health Color", "低生命值颜色", "Low Health Color");
            AddTranslation("Mana Bar Color", "魔力条颜色", "Mana Bar Color");
            AddTranslation("Mana Color", "魔力颜色", "Mana Color");
            AddTranslation("Max Health Color", "满生命值颜色", "Max Health Color");
            AddTranslation("Partial Fill Color", "部分填充颜色", "Partial Fill Color");
            AddTranslation("Primary Color", "主要颜色", "Primary Color");
            AddTranslation("Progress Color", "进度颜色", "Progress Color");
            AddTranslation("Secondary Color", "次要颜色", "Secondary Color");
            AddTranslation("Shield Bar Color", "护盾条颜色", "Shield Bar Color");
            AddTranslation("Shield Color", "护盾颜色", "Shield Color");
            AddTranslation("Target Color", "目标颜色", "Target Color");
            AddTranslation("Threshold Color", "阈值颜色", "Threshold Color");
            AddTranslation("Timer Color", "计时器颜色", "Timer Color");
            
            // 职能和职业颜色
            AddTranslation("DPS Color", "输出颜色", "DPS Color");
            AddTranslation("Healer Color", "治疗颜色", "Healer Color");
            AddTranslation("Job Color", "职业颜色", "Job Color");
            AddTranslation("Role Color", "职能颜色", "Role Color");
            AddTranslation("Tank Color", "防护颜色", "Tank Color");
            
            // 状态颜色
            AddTranslation("Dead Color", "死亡颜色", "Dead Color");
            AddTranslation("Disconnected Color", "断线颜色", "Disconnected Color");
            AddTranslation("In Range Color", "范围内颜色", "In Range Color");
            AddTranslation("Not Interruptible Color", "不可打断颜色", "Not Interruptible Color");
            AddTranslation("Offline Color", "离线颜色", "Offline Color");
            AddTranslation("Out of Range Color", "范围外颜色", "Out of Range Color");
            
            // ==================================================================
            // 第5批：更多特定配置项
            // ==================================================================
            
            // 嵌套配置标识（常见的）
            AddTranslation("Config", "配置", "Config");
            AddTranslation("Settings", "设置", "Settings");
            AddTranslation("Appearance", "外观", "Appearance");
            AddTranslation("Behavior", "行为", "Behavior");
            AddTranslation("Advanced", "高级", "Advanced");
            
            // 倒计时相关
            AddTranslation("Countdown", "倒计时", "Countdown");
            AddTranslation("Countdown Audio", "倒计时音频", "Countdown Audio");
            AddTranslation("Countdown Duration", "倒计时时长", "Countdown Duration");
            AddTranslation("Countdown Text", "倒计时文字", "Countdown Text");
            
            // 限制和过滤
            AddTranslation("Duration Filter", "持续时间过滤", "Duration Filter");
            AddTranslation("Icon Limit", "图标限制", "Icon Limit");
            AddTranslation("Max Icons", "最大图标数", "Max Icons");
            AddTranslation("Show All", "显示全部", "Show All");
            AddTranslation("Show Only Mine", "仅显示我的", "Show Only Mine");
            
            // 字体和文本
            AddTranslation("Bold", "粗体", "Bold");
            AddTranslation("Font Family", "字体", "Font Family");
            AddTranslation("Font ID", "字体ID", "Font ID");
            AddTranslation("Font Path", "字体路径", "Font Path");
            AddTranslation("Font Style", "字体样式", "Font Style");
            AddTranslation("Italic", "斜体", "Italic");
            AddTranslation("Text Format", "文字格式", "Text Format");
            AddTranslation("Text Outline", "文字描边", "Text Outline");
            AddTranslation("Text Style", "文字样式", "Text Style");
            
            // 图层和深度
            AddTranslation("Layer", "图层", "Layer");
            AddTranslation("Strata", "层级", "Strata");
            AddTranslation("Strata Level", "层级", "Strata Level");
            AddTranslation("Z-Index", "Z轴", "Z-Index");
            
            // 动画和效果
            AddTranslation("Animate", "动画", "Animate");
            AddTranslation("Blink", "闪烁", "Blink");
            AddTranslation("Effect Duration", "效果持续时间", "Effect Duration");
            AddTranslation("Fade Duration", "淡化时长", "Fade Duration");
            AddTranslation("Flash", "闪光", "Flash");
            AddTranslation("Pulse Duration", "脉冲时长", "Pulse Duration");
            AddTranslation("Transition", "过渡", "Transition");
            AddTranslation("Transition Duration", "过渡时长", "Transition Duration");
            
            // 交互
            AddTranslation("Clickable", "可点击", "Clickable");
            AddTranslation("Draggable", "可拖动", "Draggable");
            AddTranslation("Interactive", "可交互", "Interactive");
            AddTranslation("Mouse Events", "鼠标事件", "Mouse Events");
            AddTranslation("Resizable", "可调整大小", "Resizable");
            
            // 特定游戏功能
            AddTranslation("Chocobo", "陆行鸟", "Chocobo");
            AddTranslation("Crafting", "制作", "Crafting");
            AddTranslation("Duty", "副本", "Duty");
            AddTranslation("Gathering", "采集", "Gathering");
            AddTranslation("Island Sanctuary", "无人岛", "Island Sanctuary");
            AddTranslation("PvP", "PvP", "PvP");
            AddTranslation("Weapon Drawn", "拔刀", "Weapon Drawn");
            AddTranslation("Weapon Sheathed", "收刀", "Weapon Sheathed");
            
            // MP Ticker 特定
            AddTranslation("Fire III", "爆炎", "Fire III");
            AddTranslation("MP Tick", "MP跳", "MP Tick");
            AddTranslation("Next Tick", "下次跳", "Next Tick");
            AddTranslation("Tick Time", "跳时间", "Tick Time");
            
            // GCD Indicator
            AddTranslation("GCD", "GCD", "GCD");
            AddTranslation("GCD Duration", "GCD时长", "GCD Duration");
            AddTranslation("GCD Progress", "GCD进度", "GCD Progress");
            AddTranslation("Global Cooldown", "全局冷却", "Global Cooldown");
            
            // 经验相关
            AddTranslation("Current EXP", "当前经验", "Current EXP");
            AddTranslation("EXP", "经验", "EXP");
            AddTranslation("EXP to Level", "升级所需经验", "EXP to Level");
            AddTranslation("Next Level", "下一级", "Next Level");
            AddTranslation("Rested EXP", "休息经验", "Rested EXP");
            
            // 极限技
            AddTranslation("LB", "极限技", "LB");
            AddTranslation("Limit Break Bar", "极限技条", "Limit Break Bar");
            AddTranslation("Limit Break Level", "极限技等级", "Limit Break Level");
            
            // 角色名牌特定
            AddTranslation("Backdrop", "背景", "Backdrop");
            AddTranslation("Nameplate Background", "角色名牌背景", "Nameplate Background");
            AddTranslation("Nameplate Height", "角色名牌高度", "Nameplate Height");
            AddTranslation("Nameplate Width", "角色名牌宽度", "Nameplate Width");
            AddTranslation("Title Position", "称号位置", "Title Position");
            
            // 小队框体特定
            AddTranslation("Compact", "紧凑", "Compact");
            AddTranslation("Party Frame", "小队框体", "Party Frame");
            AddTranslation("Party Layout", "小队布局", "Party Layout");
            AddTranslation("Pet Bar", "宠物条", "Pet Bar");
            AddTranslation("Raid Frames", "团队框体", "Raid Frames");
            
            // 冷却追踪
            AddTranslation("Ability", "技能", "Ability");
            AddTranslation("All Roles", "所有职能", "All Roles");
            AddTranslation("Cooldown Tracker", "冷却追踪", "Cooldown Tracker");
            AddTranslation("Ready", "就绪", "Ready");
            AddTranslation("Track", "追踪", "Track");
            AddTranslation("Tracked Cooldowns", "追踪的冷却", "Tracked Cooldowns");
            
            // 敌人列表
            AddTranslation("Aggro", "仇恨", "Aggro");
            AddTranslation("Enemy Count", "敌人数量", "Enemy Count");
            AddTranslation("Enmity List", "仇恨列表", "Enmity List");
            AddTranslation("Target's Enemies", "目标的敌人", "Target's Enemies");
            AddTranslation("Threat", "威胁", "Threat");
            
            // 窗口和裁剪
            AddTranslation("Clipping", "裁剪", "Clipping");
            AddTranslation("Window", "窗口", "Window");
            AddTranslation("Window Bounds", "窗口边界", "Window Bounds");
            AddTranslation("Window Name", "窗口名称", "Window Name");
            
            // 网格
            AddTranslation("Cell Size", "单元格大小", "Cell Size");
            AddTranslation("Grid Size", "网格大小", "Grid Size");
            AddTranslation("Show Grid", "显示网格", "Show Grid");
            AddTranslation("Snap to Grid", "对齐网格", "Snap to Grid");
            
            // 导入导出
            AddTranslation("Import from Clipboard", "从剪贴板导入", "Import from Clipboard");
            AddTranslation("Reset to Default", "重置为默认", "Reset to Default");
            AddTranslation("Share", "分享", "Share");
            
            // Profiles
            AddTranslation("Active Profile", "当前方案", "Active Profile");
            AddTranslation("Create Profile", "创建方案", "Create Profile");
            AddTranslation("Delete Profile", "删除方案", "Delete Profile");
            AddTranslation("Switch Profile", "切换方案", "Switch Profile");
            AddTranslation("Profile", "配置方案", "Profile");
            AddTranslation("Profiles", "配置方案", "Profiles");
            
            // ==================================================================
            // 第6批：Combo Attribute 硬编码选项
            // ==================================================================
            
            // BarTextureDrawMode 选项
            
            // StrataLevel 选项
            AddTranslation("Lowest", "最低", "Lowest");
            AddTranslation("Low", "低", "Low");
            AddTranslation("Mid-Low", "中低", "Mid-Low");
            AddTranslation("Mid", "中", "Mid");
            AddTranslation("Mid-High", "中高", "Mid-High");
            AddTranslation("High", "高", "High");
            AddTranslation("Highest", "最高", "Highest");
            
            // ==================================================================
            // 第7批：高频使用的配置项（手动筛选翻译）
            // ==================================================================
            
            // 方位
            AddTranslation("Above", "上方", "Above");
            AddTranslation("Below", "下方", "Below");
            AddTranslation("Left and Down", "左下", "Left and Down");
            AddTranslation("Left and Up", "左上", "Left and Up");
            AddTranslation("Right and Down", "右下", "Right and Down");
            AddTranslation("Right and Up", "右上", "Right and Up");
            AddTranslation("Centered and Down", "居中向下", "Centered and Down");
            AddTranslation("Centered and Left", "居中向左", "Centered and Left");
            AddTranslation("Centered and Right", "居中向右", "Centered and Right");
            AddTranslation("Centered and Up", "居中向上", "Centered and Up");
            AddTranslation("Centered Horizontal", "水平居中", "Centered Horizontal");
            
            // 基础配置项
            AddTranslation("Border", "边框", "Border");
            AddTranslation("Full", "满", "Full");
            AddTranslation("Name", "名称", "Name");
            AddTranslation("DPS", "输出", "DPS");
            AddTranslation("Ceil", "向上取整", "Ceil");
            AddTranslation("Floor", "向下取整", "Floor");
            AddTranslation("Round", "四舍五入", "Round");
            AddTranslation("Truncate", "截断", "Truncate");
            AddTranslation("Rounding Mode", "取整模式", "Rounding Mode");
            
            // 常用开关
            AddTranslation("Crop Icon", "裁剪图标", "Crop Icon");
            AddTranslation("Disable Interaction", "禁用交互", "Disable Interaction");
            AddTranslation("Ignore Mouseover", "忽略鼠标悬停", "Ignore Mouseover");
            AddTranslation("In Combat", "战斗中", "In Combat");
            AddTranslation("Out of Combat", "非战斗", "Out of Combat");
            AddTranslation("Out of Combat (Hostile)", "非战斗（敌对）", "Out of Combat (Hostile)");
            
            // Hide系列
            AddTranslation("Hide in combat", "战斗中隐藏", "Hide in combat");
            AddTranslation("Hide outside of combat", "非战斗时隐藏", "Hide outside of combat");
            AddTranslation("Hide in Gold Saucer", "金碟游乐场中隐藏", "Hide in Gold Saucer");
            AddTranslation("Hide in Island Sanctuary", "无人岛中隐藏", "Hide in Island Sanctuary");
            AddTranslation("Hide in PvP", "PvP中隐藏", "Hide in PvP");
            AddTranslation("Hide when in duty", "副本中隐藏", "Hide when in duty");
            AddTranslation("Hide when Dead", "死亡时隐藏", "Hide when Dead");
            AddTranslation("Hide on Full MP", "MP满时隐藏", "Hide on Full MP");
            AddTranslation("Hide while at full HP", "HP满时隐藏", "Hide while at full HP");
            AddTranslation("Hide Default Castbar", "隐藏默认咏唱条", "Hide Default Castbar");
            AddTranslation("Hide Default Pulltimer", "隐藏默认开怪倒计时", "Hide Default Pulltimer");
            AddTranslation("Hide Health if Possible", "尽可能隐藏生命值", "Hide Health if Possible");
            AddTranslation("Hide Health when fully depleted", "生命值完全耗尽时隐藏", "Hide Health when fully depleted");
            AddTranslation("Hide Primals", "隐藏召唤兽", "Hide Primals");
            AddTranslation("Hide Procs When Active", "激活时隐藏触发效果", "Hide Procs When Active");
            AddTranslation("Hide Second Enmity in Light Parties", "小队中隐藏次仇恨", "Hide Second Enmity in Light Parties");
            AddTranslation("Hide Text When Zero", "为零时隐藏文字", "Hide Text When Zero");
            AddTranslation("Hide When Downsynced", "同步等级时隐藏", "Hide When Downsynced");
            
            // Show系列
            AddTranslation("Show Bar", "显示条", "Show Bar");
            AddTranslation("Show Glow", "显示发光", "Show Glow");
            AddTranslation("Show Chocobo", "显示陆行鸟", "Show Chocobo");
            AddTranslation("Show When Solo", "单人时显示", "Show When Solo");
            AddTranslation("Show Anchor Points", "显示锚点", "Show Anchor Points");
            AddTranslation("Show Ability Icon", "显示技能图标", "Show Ability Icon");
            AddTranslation("Show Center Lines", "显示中心线", "Show Center Lines");
            AddTranslation("Show Effect Duration", "显示效果持续时间", "Show Effect Duration");
            AddTranslation("Show Source Name", "显示来源名称", "Show Source Name");
            AddTranslation("Show Status Effects IDs", "显示状态效果ID", "Show Status Effects IDs");
            AddTranslation("Show Second Enmity", "显示次仇恨", "Show Second Enmity");
            AddTranslation("Show Threshold Marker", "显示阈值标记", "Show Threshold Marker");
            
            // Use系列
            AddTranslation("Use Job Colors", "使用职业颜色", "Use Job Colors");
            AddTranslation("Use Role Colors", "使用职能颜色", "Use Role Colors");
            AddTranslation("Use Role Icons", "使用职能图标", "Use Role Icons");
            AddTranslation("Use Max Health Color", "使用最大生命值颜色", "Use Max Health Color");
            AddTranslation("Use Partial Fill Color", "使用部分填充颜色", "Use Partial Fill Color");
            AddTranslation("Use Regional Number Format", "使用区域数字格式", "Use Regional Number Format");
            AddTranslation("Use State Colors", "使用状态颜色", "Use State Colors");
            AddTranslation("Use Specific DPS Colors", "使用特定输出职业颜色", "Use Specific DPS Colors");
            AddTranslation("Use Specific DPS Role Icons", "使用特定输出职能图标", "Use Specific DPS Role Icons");
            AddTranslation("Use Additional Range Check", "使用额外范围检查", "Use Additional Range Check");
            AddTranslation("Use DelvUI style", "使用DelvUI样式", "Use DelvUI style");
            
            // 标签和文本
            AddTranslation("Name Label", "名称标签", "Name Label");
            AddTranslation("Health Label", "生命值标签", "Health Label");
            AddTranslation("Time Label", "时间标签", "Time Label");
            AddTranslation("Value Label", "数值标签", "Value Label");
            AddTranslation("Order Label", "顺序标签", "Order Label");
            AddTranslation("Title Label", "称号标签", "Title Label");
            AddTranslation("Party Title Label", "小队称号标签", "Party Title Label");
            AddTranslation("Cast Time", "咏唱时间", "Cast Time");
            AddTranslation("Left Text", "左侧文字", "Left Text");
            AddTranslation("Right Text", "右侧文字", "Right Text");
            AddTranslation("Optional Text", "可选文字", "Optional Text");
            AddTranslation("Bar Text", "条文字", "Bar Text");
            AddTranslation("Duration Text", "持续时间文字", "Duration Text");
            AddTranslation("Text Anchor", "文字锚点", "Text Anchor");
            AddTranslation("Text Font and Size", "文字字体和大小", "Text Font and Size");
            AddTranslation("Font and Size", "字体和大小", "Font and Size");
            AddTranslation("Title Font and Size", "称号字体和大小", "Title Font and Size");
            
            // ==================================================================
            // 第8批：边框和激活状态（约100个）
            // ==================================================================
            
            // 边框和厚度
            AddTranslation("Active Border Thickness", "激活边框厚度", "Active Border Thickness");
            AddTranslation("Inactive Border Thickness", "非激活边框厚度", "Inactive Border Thickness");
            AddTranslation("Target Border Color", "目标边框颜色", "Target Border Color");
            AddTranslation("Target Border Thickness", "目标边框厚度", "Target Border Thickness");
            AddTranslation("Targeted Border Color", "被锁定边框颜色", "Targeted Border Color");
            AddTranslation("Targeted Border Thickness", "被锁定边框厚度", "Targeted Border Thickness");
            AddTranslation("Deafened Border Color", "致聋状态边框颜色", "Deafened Border Color");
            AddTranslation("Muted Border Color", "禁言状态边框颜色", "Muted Border Color");
            AddTranslation("Speaking Border Color", "说话状态边框颜色", "Speaking Border Color");
            AddTranslation("Icon Active Border Color", "图标激活边框颜色", "Icon Active Border Color");
            AddTranslation("Icon Active Border Thickness", "图标激活边框厚度", "Icon Active Border Thickness");
            
            // Change系列
            AddTranslation("Change Alpha Based on Range", "根据范围改变透明度", "Change Alpha Based on Range");
            AddTranslation("Change Background Color When Invuln is Up", "无敌时改变背景颜色", "Change Background Color When Invuln is Up");
            AddTranslation("Change Background Color When Raised", "复活时改变背景颜色", "Change Background Color When Raised");
            AddTranslation("Change Border Color", "改变边框颜色", "Change Border Color");
            AddTranslation("Change Border Color When Raised", "复活时改变边框颜色", "Change Border Color When Raised");
            AddTranslation("Change Color", "改变颜色", "Change Color");
            AddTranslation("Change Enemy Alpha Based on Range", "根据敌人范围改变透明度", "Change Enemy Alpha Based on Range");
            AddTranslation("Change Friendly Alpha Based on Range", "根据友军范围改变透明度", "Change Friendly Alpha Based on Range");
            AddTranslation("Change Health Bar Border when active", "激活时改变生命值条边框", "Change Health Bar Border when active");
            AddTranslation("Change Health Bar Color ", "改变生命值条颜色", "Change Health Bar Color ");
            AddTranslation("Change Icon Border When Active", "激活时改变图标边框", "Change Icon Border When Active");
            AddTranslation("Change Label Color When Active", "激活时改变标签颜色", "Change Label Color When Active");
            AddTranslation("Change Labels Color When Active", "激活时改变标签颜色", "Change Labels Color When Active");
            
            // 锚点相关
            AddTranslation("Anchor To Mouse", "锚定到鼠标", "Anchor To Mouse");
            AddTranslation("Anchor to Unit Frame", "锚定到情报框体", "Anchor to Unit Frame");
            AddTranslation("Bars Anchor", "条锚点", "Bars Anchor");
            AddTranslation("Frame Anchor", "框架锚点", "Frame Anchor");
            AddTranslation("Health Bar Anchor", "生命值条锚点", "Health Bar Anchor");
            AddTranslation("Nameplate Label Anchor", "角色名牌标签锚点", "Nameplate Label Anchor");
            AddTranslation("Unit Frame Anchor", "单位框架锚点", "Unit Frame Anchor");
            AddTranslation("Prioritize Health Bar as Anchor when visible", "可见时优先使用生命值条作为锚点", "Prioritize Health Bar as Anchor when visible");
            
            // 范围和距离
            AddTranslation("Additional Range (yalms)", "额外范围（yalms）", "Additional Range (yalms)");
            AddTranslation("Range (yalms)", "范围（yalms）", "Range (yalms)");
            AddTranslation("Fade start range (yalms)", "淡出起始范围（yalms）", "Fade start range (yalms)");
            AddTranslation("Fade end range (yalms)", "淡出结束范围（yalms）", "Fade end range (yalms)");
            
            // 偏移量
            AddTranslation("Bottom Right Offset", "右下偏移", "Bottom Right Offset");
            AddTranslation("Top Left Offset", "左上偏移", "Top Left Offset");
            
            // 图标和布局
            AddTranslation("Icon Padding", "图标内边距", "Icon Padding");
            AddTranslation("Icons Growth Direction", "图标增长方向", "Icons Growth Direction");
            AddTranslation("Sections Growth Direction", "区段增长方向", "Sections Growth Direction");
            AddTranslation("Custom Icon Position", "自定义图标位置", "Custom Icon Position");
            AddTranslation("Custom Icon Size", "自定义图标大小", "Custom Icon Size");
            AddTranslation("Separate Icon", "分离图标", "Separate Icon");
            AddTranslation("Keep Icon After Cast Finishes", "咏唱完成后保留图标", "Keep Icon After Cast Finishes");
            
            // 填充和匹配
            AddTranslation("Fill Health First", "优先填充生命值", "Fill Health First");
            AddTranslation("Fill Rows First", "优先填充行", "Fill Rows First");
            AddTranslation("Match Height with Health Bar", "高度与生命值条匹配", "Match Height with Health Bar");
            AddTranslation("Match Width with Health Bar", "宽度与生命值条匹配", "Match Width with Health Bar");
            AddTranslation("Partially Filled Bar", "部分填充的条", "Partially Filled Bar");
            
            // 数值显示格式
            AddTranslation("No Decimals (i.e. \"12\")", "无小数（如\"12\"）", "No Decimals (i.e. \"12\")");
            AddTranslation("One Decimal (i.e. \"12.3\")", "一位小数（如\"12.3\"）", "One Decimal (i.e. \"12.3\")");
            AddTranslation("Two Decimals (i.e. \"12.34\")", "两位小数（如\"12.34\"）", "Two Decimals (i.e. \"12.34\")");
            AddTranslation("Duration (seconds)", "持续时间（秒）", "Duration (seconds)");
            AddTranslation("Time (milliseconds)", "时间（毫秒）", "Time (milliseconds)");
            AddTranslation("Limit (-1 for no limit)", "限制（-1表示无限制）", "Limit (-1 for no limit)");
            
            // 模式
            AddTranslation("Automatic Mode", "自动模式", "Automatic Mode");
            AddTranslation("Bar Mode", "条模式", "Bar Mode");
            AddTranslation("Circular Mode", "圆形模式", "Circular Mode");
            AddTranslation("Blend Mode", "混合模式", "Blend Mode");
            
            // 背景和颜色
            AddTranslation("Available Background Color", "可用背景颜色", "Available Background Color");
            AddTranslation("Available Color", "可用颜色", "Available Color");
            AddTranslation("Death Indicator Background Color", "死亡指示器背景颜色", "Death Indicator Background Color");
            AddTranslation("Invuln Background Color", "无敌背景颜色", "Invuln Background Color");
            AddTranslation("Out of Reach Background Color", "超出范围背景颜色", "Out of Reach Background Color");
            AddTranslation("Raise Background Color", "复活背景颜色", "Raise Background Color");
            AddTranslation("Raise Border Color", "复活边框颜色", "Raise Border Color");
            AddTranslation("Recharging Background Color", "充能中背景颜色", "Recharging Background Color");
            AddTranslation("Recharging Color", "充能中颜色", "Recharging Color");
            AddTranslation("Reverse Fill Background Color", "反向填充背景颜色", "Reverse Fill Background Color");
            AddTranslation("Walking Dead Background Color", "行尸走肉背景颜色", "Walking Dead Background Color");
            AddTranslation("Walking Dead Color ##TankWalkingDeadCustom", "行尸走肉颜色##坦克行尸走肉自定义", "Walking Dead Color ##TankWalkingDeadCustom");
            AddTranslation("Walking Dead Custom Color", "行尸走肉自定义颜色", "Walking Dead Custom Color");
            
            // 其他配置
            AddTranslation("Activate Above/Below Threshold", "激活高于/低于阈值", "Activate Above/Below Threshold");
            AddTranslation("Always show while in PvP", "PvP中始终显示", "Always show while in PvP");
            AddTranslation("Always show while target exists", "存在目标时始终显示", "Always show while target exists");
            AddTranslation("Automatically disable HUD elements preview", "自动禁用HUD元素预览", "Automatically disable HUD elements preview");
            AddTranslation("Custom Color when being targeted", "被锁定时的自定义颜色", "Custom Color when being targeted");
            AddTranslation("Custom Mouseover Area", "自定义鼠标悬停区域", "Custom Mouseover Area");
            AddTranslation("Dim DelvUI's settings window when not focused", "未聚焦时使DelvUI设置窗口变暗", "Dim DelvUI's settings window when not focused");
            AddTranslation("Filter Status Effects", "过滤状态效果", "Filter Status Effects");
            AddTranslation("Global HUD Position", "全局HUD位置", "Global HUD Position");
            AddTranslation("Highlight Color", "高亮颜色", "Highlight Color");
            AddTranslation("Highlight When Hovering With Cursor Or Soft Targeting", "鼠标悬停或软锁定时高亮", "Highlight When Hovering With Cursor Or Soft Targeting");
            AddTranslation("Size When Targeted", "被锁定时的大小", "Size When Targeted");
            AddTranslation("Use Custom Color when being targeted", "被锁定时使用自定义颜色", "Use Custom Color when being targeted");
            AddTranslation("Use Death Indicator Background Color", "使用死亡指示器背景颜色", "Use Death Indicator Background Color");
            AddTranslation("Use Different Size when targeted", "被锁定时使用不同大小", "Use Different Size when targeted");
            
            // ==================================================================
            // 第9批：职业、NPC、状态和效果（约90个）
            // ==================================================================
            
            // 基础职业（使用官方译名）
            AddTranslation("Arcanist", "秘术师", "Arcanist");
            AddTranslation("Archer", "弓箭手", "Archer");
            AddTranslation("Conjurer", "幻术师", "Conjurer");
            AddTranslation("Gladiator", "剑术师", "Gladiator");
            AddTranslation("Lancer", "枪术师", "Lancer");
            AddTranslation("Marauder", "斧术师", "Marauder");
            AddTranslation("Pugilist", "格斗家", "Pugilist");
            AddTranslation("Rogue", "双剑师", "Rogue");
            AddTranslation("Thaumaturge", "咒术师", "Thaumaturge");
            
            // 职业分类
            AddTranslation("Caster DPS", "远程魔法输出", "Caster DPS");
            AddTranslation("Melee DPS", "近战物理输出", "Melee DPS");
            AddTranslation("Ranged DPS", "远程物理输出", "Ranged DPS");
            AddTranslation("Disciple of the Hand", "能工巧匠", "Disciple of the Hand");
            AddTranslation("Disciple of the Land", "大地使者", "Disciple of the Land");
            
            // 仇恨相关
            AddTranslation("Enmity Close To Leader Color", "接近一仇颜色", "Enmity Close To Leader Color");
            AddTranslation("Enmity Leader Color", "一仇颜色", "Enmity Leader Color");
            AddTranslation("Enmity Second Color", "二仇颜色", "Enmity Second Color");
            
            // NPC类型
            AddTranslation("NPC Friendly", "友好NPC", "NPC Friendly");
            AddTranslation("NPC Hostile", "敌对NPC", "NPC Hostile");
            AddTranslation("NPC Neutral", "中立NPC", "NPC Neutral");
            
            // 状态效果
            AddTranslation("Dispellable Effects Border", "可驱散效果边框", "Dispellable Effects Border");
            AddTranslation("My Effects Border", "我的效果边框", "My Effects Border");
            AddTranslation("My Effects First", "我的效果优先", "My Effects First");
            AddTranslation("Only My Effects", "仅我的效果", "Only My Effects");
            AddTranslation("Permanent Effects", "永久效果", "Permanent Effects");
            AddTranslation("Permanent Effects First", "永久效果优先", "Permanent Effects First");
            AddTranslation("Pet As Own Effect", "宠物视为自己的效果", "Pet As Own Effect");
            AddTranslation("Ignore Buff Duration", "忽略增益持续时间", "Ignore Buff Duration");
            AddTranslation("Sort by Duration", "按持续时间排序", "Sort by Duration");
            
            // 热键栏
            AddTranslation("Cross Hotbar", "交叉热键栏", "Cross Hotbar");
            AddTranslation("Hotbar 1", "热键栏1", "Hotbar 1");
            AddTranslation("Hotbar 2", "热键栏2", "Hotbar 2");
            AddTranslation("Hotbar 3", "热键栏3", "Hotbar 3");
            AddTranslation("Hotbar 4", "热键栏4", "Hotbar 4");
            AddTranslation("Hotbar 5", "热键栏5", "Hotbar 5");
            AddTranslation("Hotbar 6", "热键栏6", "Hotbar 6");
            AddTranslation("Hotbar 7", "热键栏7", "Hotbar 7");
            AddTranslation("Hotbar 8", "热键栏8", "Hotbar 8");
            AddTranslation("Hotbar 9", "热键栏9", "Hotbar 9");
            AddTranslation("Hotbar 10", "热键栏10", "Hotbar 10");
            
            // 颜色配置选项
            AddTranslation("Color ##Outline", "颜色##描边", "Color ##Outline");
            AddTranslation("Color ##Shields", "颜色##护盾", "Color ##Shields");
            AddTranslation("Color ##SlidecastColor", "颜色##滑步咏唱", "Color ##SlidecastColor");
            AddTranslation("Color ##Text", "颜色##文字", "Color ##Text");
            AddTranslation("Color Based On Health Value", "基于生命值的颜色", "Color Based On Health Value");
            AddTranslation("Color##DeathIndicator", "颜色##死亡指示器", "Color##DeathIndicator");
            AddTranslation("Color##MissingHealth", "颜色##缺失生命值", "Color##MissingHealth");
            AddTranslation("Color##ReverseFill", "颜色##反向填充", "Color##ReverseFill");
            AddTranslation("Color##SeraphColor", "颜色##天使", "Color##SeraphColor");
            AddTranslation("Flat Color", "纯色", "Flat Color");
            AddTranslation("GCD Queue Color", "GCD队列颜色", "GCD Queue Color");
            AddTranslation("High Health Color", "高生命值颜色", "High Health Color");
            AddTranslation("Interruptable Color", "可打断颜色", "Interruptable Color");
            AddTranslation("Label Active Color", "标签激活颜色", "Label Active Color");
            AddTranslation("Labels Active Color", "标签激活颜色", "Labels Active Color");
            AddTranslation("Missing Health Color", "缺失生命值颜色", "Missing Health Color");
            AddTranslation("Rested Exp Color", "休息经验颜色", "Rested Exp Color");
            AddTranslation("Subtractive Color", "减色", "Subtractive Color");
            AddTranslation("Subtractive Color ##Outline", "减色##描边", "Subtractive Color ##Outline");
            AddTranslation("Subtractive Fill Color", "减色填充颜色", "Subtractive Fill Color");
            AddTranslation("Threshold Marker Color", "阈值标记颜色", "Threshold Marker Color");
            AddTranslation("Threshold Marker Size", "阈值标记大小", "Threshold Marker Size");
            AddTranslation("Title Color", "称号颜色", "Title Color");
            
            // Job相关颜色
            AddTranslation("Job Color As Background Color", "职业颜色作为背景颜色", "Job Color As Background Color");
            AddTranslation("Job Color as Max Health Color", "职业颜色作为最大生命值颜色", "Job Color as Max Health Color");
            AddTranslation("Job Color As Missing Health Color", "职业颜色作为缺失生命值颜色", "Job Color As Missing Health Color");
            AddTranslation("Job Role as Max Health Color", "职能颜色作为最大生命值颜色", "Job Role as Max Health Color");
            AddTranslation("Role Color As Background Color", "职能颜色作为背景颜色", "Role Color As Background Color");
            AddTranslation("Role Color As Missing Health Color", "职能颜色作为缺失生命值颜色", "Role Color As Missing Health Color");
            
            // 其他配置
            AddTranslation("Damage Type Colors", "伤害类型颜色", "Damage Type Colors");
            AddTranslation("Divisions Distance", "分割距离", "Divisions Distance");
            AddTranslation("Empty Bar", "空条", "Empty Bar");
            AddTranslation("Empty Unit Frame", "空情报框体", "Empty Unit Frame");
            AddTranslation("Gradient Type For Bars", "条的渐变类型", "Gradient Type For Bars");
            AddTranslation("Grid Divisions", "网格分割", "Grid Divisions");
            AddTranslation("Instant GCDs only", "仅瞬发GCD", "Instant GCDs only");
            AddTranslation("Interruptable", "可打断", "Interruptable");
            AddTranslation("Magical", "魔法", "Magical");
            AddTranslation("Physical", "物理", "Physical");
            AddTranslation("Replace Order Label", "替换顺序标签", "Replace Order Label");
            AddTranslation("Replace Role/Job Icon when active", "激活时替换职能/职业图标", "Replace Role/Job Icon when active");
            AddTranslation("RGB", "RGB", "RGB");
            AddTranslation("Role / Job", "职能/职业", "Role / Job");
            AddTranslation("Role/Job Icon", "职能/职业图标", "Role/Job Icon");
            AddTranslation("Rotate CCW", "逆时针旋转", "Rotate CCW");
            AddTranslation("Start Angle", "起始角度", "Start Angle");
            AddTranslation("Style 1", "样式1", "Style 1");
            AddTranslation("Style 2", "样式2", "Style 2");
            AddTranslation("Style 3", "样式3", "Style 3");
            AddTranslation("Subdivision Count", "子分割数", "Subdivision Count");
            AddTranslation("Swap Name and Title labels when needed", "需要时交换名称和称号标签", "Swap Name and Title labels when needed");
            AddTranslation("Thickness in Pixels", "厚度（像素）", "Thickness in Pixels");
            AddTranslation("Vertical Padding", "垂直内边距", "Vertical Padding");
            AddTranslation("Walls", "墙壁", "Walls");
            AddTranslation("Walls and Objects", "墙壁和物体", "Walls and Objects");
            
            // ==================================================================
            // 第10批：名称隐藏和状态追踪（约80个）
            // ==================================================================
            
            // Hide Name系列
            AddTranslation("Hide Name When Casting", "咏唱时隐藏名称", "Hide Name When Casting");
            AddTranslation("Hide Name When Invuln is Up", "无敌时隐藏名称", "Hide Name When Invuln is Up");
            AddTranslation("Hide Name When Raised", "复活时隐藏名称", "Hide Name When Raised");
            AddTranslation("Hide Name When Showing Status", "显示状态时隐藏名称", "Hide Name When Showing Status");
            
            // Show系列（更多）
            AddTranslation("Show Buff Timer On Active Chunk", "在激活块上显示增益计时器", "Show Buff Timer On Active Chunk");
            AddTranslation("Show Current Cast Time + Max Cast Time", "显示当前咏唱时间+最大咏唱时间", "Show Current Cast Time + Max Cast Time");
            AddTranslation("Show Deafened State", "显示致聋状态", "Show Deafened State");
            AddTranslation("Show Empty Drawing for Pom + Wings", "显示绒球+翅膀的空绘制", "Show Empty Drawing for Pom + Wings");
            AddTranslation("Show Enmity Border Colors", "显示仇恨边框颜色", "Show Enmity Border Colors");
            AddTranslation("Show GCD Queue Indicator", "显示GCD队列指示器", "Show GCD Queue Indicator");
            AddTranslation("Show Generic Mana Bar", "显示通用魔力条", "Show Generic Mana Bar");
            AddTranslation("Show Glow##AstralSoul", "显示发光##星灵魂", "Show Glow##AstralSoul");
            AddTranslation("Show Glow##Paradox", "显示发光##悖论", "Show Glow##Paradox");
            AddTranslation("Show Icon Cooldown Animation", "显示图标冷却动画", "Show Icon Cooldown Animation");
            AddTranslation("Show In Chunks", "以块显示", "Show In Chunks");
            AddTranslation("Show Inner Release Cooldown", "显示原初的解放冷却", "Show Inner Release Cooldown");
            AddTranslation("Show mana values up to 10k (will break thresholds)", "显示最高10k魔力值（会破坏阈值）", "Show mana values up to 10k (will break thresholds)");
            AddTranslation("Show Muted State", "显示禁言状态", "Show Muted State");
            AddTranslation("Show Only During Astral Fire", "仅在星极火中显示", "Show Only During Astral Fire");
            AddTranslation("Show Only During Umbral Ice", "仅在灵极冰中显示", "Show Only During Umbral Ice");
            AddTranslation("Show Only in Duties", "仅在副本中显示", "Show Only in Duties");
            AddTranslation("Show only on jobs with cleanses", "仅在有驱散技能的职业上显示", "Show only on jobs with cleanses");
            AddTranslation("Show Remainin Cooldown", "显示剩余冷却", "Show Remainin Cooldown");
            AddTranslation("Show Rested Exp", "显示休息经验", "Show Rested Exp");
            AddTranslation("Show Seraph", "显示天使", "Show Seraph");
            AddTranslation("Show Speaking State", "显示说话状态", "Show Speaking State");
            AddTranslation("Show Text on All Chunks", "在所有块上显示文字", "Show Text on All Chunks");
            AddTranslation("Show Text on Active Chunk", "在激活块上显示文字", "Show Text on Active Chunk");
            AddTranslation("Show For All Jobs With Raise", "所有有复活的职业显示", "Show For All Jobs With Raise");
            AddTranslation("Show Only For Healers", "仅治疗职业显示", "Show Only For Healers");
            AddTranslation("Show For All Jobs", "所有职业显示", "Show For All Jobs");

            // Only Show系列
            AddTranslation("Only show disconnected icon", "仅显示断线图标", "Only show disconnected icon");
            AddTranslation("Only Show when not at full Health", "仅在生命值未满时显示", "Only Show when not at full Health");
            AddTranslation("Only show when targeted", "仅在锁定时显示", "Only show when targeted");
            AddTranslation("Only show when under GCD Threshold", "仅在GCD阈值以下时显示", "Only show when under GCD Threshold");
            
            // 追踪器
            AddTranslation("Cleanse Tracker", "驱散追踪器", "Cleanse Tracker");
            AddTranslation("Invulnerabilities Tracker", "无敌追踪器", "Invulnerabilities Tracker");
            AddTranslation("Raise Tracker", "复活追踪器", "Raise Tracker");
            AddTranslation("Tank Stance Indicator", "坦克姿态指示器", "Tank Stance Indicator");
            
            // 坦克无敌
            AddTranslation("Tank Invulnerability", "坦克无敌", "Tank Invulnerability");
            AddTranslation("Tank Invulnerability Color ##TankInvulnerabilityCustom", "坦克无敌颜色##坦克无敌自定义", "Tank Invulnerability Color ##TankInvulnerabilityCustom");
            AddTranslation("Tank Invulnerability Custom Color", "坦克无敌自定义颜色", "Tank Invulnerability Custom Color");
            
            // 玩家状态
            AddTranslation("Player State Icon", "玩家状态图标", "Player State Icon");
            AddTranslation("Player Status", "玩家状态", "Player Status");
            AddTranslation("Ready Check Status", "确认状态", "Ready Check Status");
            AddTranslation("Sanctuary Icon", "庇护所图标", "Sanctuary Icon");
            AddTranslation("Who's Talking", "谁在说话", "Who's Talking");
            
            // Use Element系列
            AddTranslation("Use Element Color##MP", "使用元素颜色##MP", "Use Element Color##MP");
            AddTranslation("Use Element Color##Paradox", "使用元素颜色##悖论", "Use Element Color##Paradox");
            
            // 其他配置
            AddTranslation("Ascending", "升序", "Ascending");
            AddTranslation("Descending", "降序", "Descending");
            AddTranslation("Combo Start", "连击起手", "Combo Start");
            AddTranslation("Count Swiftcast##Triplecast", "计数即刻咏唱##三连咏唱", "Count Swiftcast##Triplecast");
            AddTranslation("Darkness", "暗黑", "Darkness");
            AddTranslation("Don't Show Drawing", "不显示绘制", "Don't Show Drawing");
            AddTranslation("Enable Awakened Timer", "启用觉醒计时器", "Enable Awakened Timer");
            AddTranslation("Enable Eukrasia Glow", "启用均衡诊断发光", "Enable Eukrasia Glow");
            AddTranslation("Enable Only for BLM", "仅对黑魔法师启用", "Enable Only for BLM");
            AddTranslation("Fire III Threshold (BLM only)", "爆炎阈值（仅黑魔法师）", "Fire III Threshold (BLM only)");
            AddTranslation("Glow on Flourishing Fan Dance", "扇舞·终效果发光", "Glow on Flourishing Fan Dance");
            AddTranslation("No Mercy", "无情", "No Mercy");
            AddTranslation("XYZ", "XYZ", "XYZ");
            
            // 颜色空间
            AddTranslation("Jzazbz", "Jzazbz", "Jzazbz");
            AddTranslation("JzCzhz", "JzCzhz", "JzCzhz");
            AddTranslation("LAB", "LAB", "LAB");
            AddTranslation("LChab", "LChab", "LChab");
            AddTranslation("LChuv", "LChuv", "LChuv");
            AddTranslation("Luv", "Luv", "Luv");
            
            // 更多配置项
            AddTranslation("Glow Color (when Eukrasia active)", "发光颜色（均衡诊断激活时）", "Glow Color (when Eukrasia active)");
            AddTranslation("Glow Color (when Misery ready)", "发光颜色（苦难之心就绪时）", "Glow Color (when Misery ready)");
            AddTranslation("Glow Color (when Primal Rend is ready)", "发光颜色（蛮荒崩裂就绪时）", "Glow Color (when Primal Rend is ready)");
            AddTranslation("Full Stacks Color", "满层数颜色", "Full Stacks Color");
            
            // ==================================================================
            // 第11批：职业量谱Bar（通用格式翻译，约100个）
            // ==================================================================
            
            AddTranslation("Addersgall Bar", "蛇胆", "Addersgall Bar");
            AddTranslation("Addersting Bar", "蛇刺", "Addersting Bar");
            AddTranslation("Aetherflow Bar", "以太超流", "Aetherflow Bar");
            AddTranslation("Anguine Tribute Bar", "祖灵力", "Anguine Tribute Bar");
            AddTranslation("Astral Soul Bar", "星极魂", "Astral Soul Bar");
            AddTranslation("Asylum Bar", "庇护所", "Asylum Bar");
            AddTranslation("Attunement Stacks Bar", "调和层数", "Attunement Stacks Bar");
            AddTranslation("Automaton Queen Bar", "后式自动人偶", "Automaton Queen Bar");
            AddTranslation("Balance Bar", "平衡量谱", "Balance Bar");
            AddTranslation("Battery Gauge", "电能量谱", "Battery Gauge");
            AddTranslation("Beast Gauge", "兽魂量谱", "Beast Gauge");
            AddTranslation("Beast Gauge Color", "兽魂量谱颜色", "Beast Gauge Color");
            AddTranslation("Bio Bar", "毒菌", "Bio Bar");
            AddTranslation("Black Mana Bar", "黑魔元", "Black Mana Bar");
            AddTranslation("Bleed Bar", "流血", "Bleed Bar");
            AddTranslation("Blood Gauge", "暗黑量谱", "Blood Gauge");
            AddTranslation("Blood Lily Bar", "血百合", "Blood Lily Bar");
            AddTranslation("Blood Weapon Bar", "嗜血", "Blood Weapon Bar");
            AddTranslation("Cards Bar", "卡片", "Cards Bar");
            AddTranslation("Caustic Bite Bar", "烈毒咬箭", "Caustic Bite Bar");
            AddTranslation("Chakra Bar", "兽脉", "Chakra Bar");
            AddTranslation("Chakra 1", "兽脉1", "Chakra 1");
            AddTranslation("Chakra 2", "兽脉2", "Chakra 2");
            AddTranslation("Chakra 3", "兽脉3", "Chakra 3");
            AddTranslation("Chakra Order", "兽脉顺序", "Chakra Order");
            AddTranslation("Chaos Thrust", "樱花怒放", "Chaos Thrust");
            AddTranslation("Coda Bar", "终章", "Coda Bar");
            AddTranslation("Creature Canvas Bar", "动物彩绘", "Creature Canvas Bar");
            AddTranslation("Darkside Bar", "灾变", "Darkside Bar");
            AddTranslation("Death Gauge", "死亡量谱", "Death Gauge");
            AddTranslation("Death's Design Bar", "死亡烙印", "Death's Design Bar");
            AddTranslation("Delirium Bar", "血乱", "Delirium Bar");
            AddTranslation("Devilment Bar", "进攻之探戈", "Devilment Bar");
            AddTranslation("Dia Bar", "天辉", "Dia Bar");
            AddTranslation("Dot Bar", "持续伤害", "Dot Bar");
            AddTranslation("Dualcast Bar", "连续咏唱", "Dualcast Bar");
            AddTranslation("Enochian Bar", "天语", "Enochian Bar");
            AddTranslation("Enshroud Duration Text", "夜游魂衣持续时间文字", "Enshroud Duration Text");
            AddTranslation("Esprit Gauge", "幻扇量谱", "Esprit Gauge");
            AddTranslation("Eukrasian Dosis Bar", "均衡注药", "Eukrasian Dosis Bar");
            AddTranslation("Fairy Gauge", "异想量谱", "Fairy Gauge");
            AddTranslation("Feathers Gauge", "幻扇量谱", "Feathers Gauge");
            AddTranslation("Fight or Flight Bar", "战逃反应", "Fight or Flight Bar");
            AddTranslation("Flourishing Flow Bar", "非对称投掷", "Flourishing Flow Bar");
            AddTranslation("Flourishing Symmetry Bar", "对称投掷", "Flourishing Symmetry Bar");
            AddTranslation("Form Number Text", "象形拳文字", "Form Number Text");
            AddTranslation("Formless Fist Duration Text", "无相身形持续时间文字", "Formless Fist Duration Text");
            AddTranslation("Forms Bar", "象形拳", "Forms Bar");
            AddTranslation("Fugetsu Bar", "风月", "Fugetsu Bar");
            AddTranslation("Fuka Bar", "风花", "Fuka Bar");
            AddTranslation("Fury Stacks Bar", "斗气层数", "Fury Stacks Bar");
            AddTranslation("Garuda Bar", "迦楼罗", "Garuda Bar");
            AddTranslation("Garuda Stacks", "迦楼罗层数", "Garuda Stacks");
            AddTranslation("Hammer Time Bar", "重锤次数", "Hammer Time Bar");
            AddTranslation("Heat Gauge", "热量量谱", "Heat Gauge");
            AddTranslation("Higanbana Bar", "彼岸花", "Higanbana Bar");
            AddTranslation("Hyperphantasia Bar", "幻灵绘景", "Hyperphantasia Bar");
            AddTranslation("Ifrit Bar", "伊弗利特", "Ifrit Bar");
            AddTranslation("Ifrit Stacks", "伊弗利特层数", "Ifrit Stacks");
            AddTranslation("Inner Release Bar", "原初的解放", "Inner Release Bar");
            AddTranslation("Kazematoi Bar", "风缠", "Kazematoi Bar");
            AddTranslation("Kenki Bar", "剑气", "Kenki Bar");
            AddTranslation("Kerachole / Holos Bar", "坚角清汁/整体论", "Kerachole / Holos Bar");
            AddTranslation("Landscape Canvas Bar", "风景彩绘", "Landscape Canvas Bar");
            AddTranslation("Life of the Dragon", "红莲龙血", "Life of the Dragon");
            AddTranslation("Lightspeed Bar", "光速", "Lightspeed Bar");
            AddTranslation("Lily Bar", "百合", "Lily Bar");
            AddTranslation("Living Shadow Bar", "掠影示现", "Living Shadow Bar");
            AddTranslation("Mana Stacks Bar", "魔元层数", "Mana Stacks Bar");
            AddTranslation("Masterful Blitz Bar", "必杀技", "Masterful Blitz Bar");
            AddTranslation("Meditation Bar", "默想", "Meditation Bar");
            AddTranslation("Moon Flute Bar", "月之笛", "Moon Flute Bar");
            AddTranslation("MP Ticker Bar", "MP跳蓝计时器", "MP Ticker Bar");
            AddTranslation("Mudra Bar", "结印", "Mudra Bar");
            AddTranslation("Ninki Bar", "忍气", "Ninki Bar");
            AddTranslation("Oath Gauge", "忠义量谱", "Oath Gauge");
            AddTranslation("Off-Guard Bar", "破防", "Off-Guard Bar");
            AddTranslation("Overheat Gauge", "过热量谱", "Overheat Gauge");
            AddTranslation("Paint Bar", "颜料", "Paint Bar");
            AddTranslation("Palette Bar", "调色板", "Palette Bar");
            AddTranslation("Paradox Bar", "悖论", "Paradox Bar");
            AddTranslation("Perfect Balance Bar", "震脚", "Perfect Balance Bar");
            AddTranslation("Perfect Balance Duration Text", "震脚持续时间文字", "Perfect Balance Duration Text");
            AddTranslation("Physis Bar", "自生", "Physis Bar");
            AddTranslation("Plenary Bar", "全大赦", "Plenary Bar");
            AddTranslation("Polyglot Bar", "通晓", "Polyglot Bar");
            AddTranslation("Powder Gauge", "晶壤量谱", "Powder Gauge");
            AddTranslation("Power Surge", "猛枪", "Power Surge");
            AddTranslation("Presence of Mind Bar", "神速咏唱", "Presence of Mind Bar");
            AddTranslation("Rattling Coil Bar", "飞蛇之魂", "Rattling Coil Bar");
            AddTranslation("Requiescat Bar", "安魂祈祷", "Requiescat Bar");
            AddTranslation("Sacred Soil Bar", "野战治疗阵", "Sacred Soil Bar");
            AddTranslation("Sen Bar", "三闪", "Sen Bar");
            AddTranslation("Serpent Offerings Bar", "灵力量谱", "Serpent Offerings Bar");
            AddTranslation("Shadow Walker Bar", "忍隐", "Shadow Walker Bar");
            AddTranslation("Shroud Bar", "夜游魂衣", "Shroud Bar");
            AddTranslation("Song Gauge Bar", "战歌量谱", "Song Gauge Bar");
            AddTranslation("Soul Bar", "灵魂量谱", "Soul Bar");
            AddTranslation("Soul Voice Bar", "诗心", "Soul Voice Bar");
            AddTranslation("Spell Amp Bar", "法术增幅", "Spell Amp Bar");
            AddTranslation("Stacks Bar", "层数", "Stacks Bar");
            AddTranslation("Standard Finish Bar", "标准舞步结束", "Standard Finish Bar");
            AddTranslation("Star Bar", "地星", "Star Bar");
            AddTranslation("Steps Bar", "舞步", "Steps Bar");
            AddTranslation("Stormbite Bar", "狂风蚀箭", "Stormbite Bar");
            AddTranslation("Surging Tempest Bar", "战场风暴", "Surging Tempest Bar");
            AddTranslation("Surpanakha Bar", "穿甲散弹", "Surpanakha Bar");
            AddTranslation("Technical Finish Bar", "技巧舞步结束", "Technical Finish Bar");
            AddTranslation("Temperance Bar", "节制", "Temperance Bar");
            AddTranslation("Thunder DoT Bar", "雷DoT", "Thunder DoT Bar");
            AddTranslation("Tingle Bar", "哔哩哔哩", "Tingle Bar");
            AddTranslation("Titan Bar", "泰坦", "Titan Bar");
            AddTranslation("Titan Stacks", "泰坦层数", "Titan Stacks");
            AddTranslation("Trance Bar", "龙神附体", "Trance Bar");
            AddTranslation("Trick Attack Bar", "攻其不备", "Trick Attack Bar");
            AddTranslation("Triplecast Bar", "三连咏唱", "Triplecast Bar");
            AddTranslation("Umbral Heart Bar", "灵极心", "Umbral Heart Bar");
            AddTranslation("Umbral Ice / Astral Fire Bar", "灵极冰/星极火", "Umbral Ice / Astral Fire Bar");
            AddTranslation("Verfire Ready Bar", "赤火炎预备", "Verfire Ready Bar");
            AddTranslation("Verstone Ready Bar", "赤飞石预备", "Verstone Ready Bar");
            AddTranslation("Vipersight Bar", "蝰蛇量谱", "Vipersight Bar");
            AddTranslation("Weapon Canvas Bar", "武器彩绘", "Weapon Canvas Bar");
            AddTranslation("White Mana Bar", "白魔元", "White Mana Bar");
            AddTranslation("Wildfire Bar", "野火", "Wildfire Bar");
            AddTranslation("Windburn Bar", "风灼", "Windburn Bar");
            
            // ==================================================================
            // 第12批：职业技能颜色和配置（约100个）
            // ==================================================================
            
            // 诗人歌相关
            AddTranslation("Army's Paeon", "军神的赞美歌", "Army's Paeon");
            AddTranslation("Army's Paeon Stack##Stacks", "军神的赞美歌层数##层数", "Army's Paeon Stack##Stacks");
            AddTranslation("Army's Paeon Stacks##Stacks", "军神的赞美歌层数##层数", "Army's Paeon Stacks##Stacks");
            AddTranslation("Army's Paeon Threshold", "军神的赞美歌阈值", "Army's Paeon Threshold");
            AddTranslation("Army's Paeon##Coda", "军神的赞美歌##Coda", "Army's Paeon##Coda");
            AddTranslation("Army's Paeon##Song", "军神的赞美歌##歌", "Army's Paeon##Song");
            AddTranslation("Mage's Ballad", "贤者的叙事谣", "Mage's Ballad");
            AddTranslation("Mage's Ballad Proc Glow", "贤者的叙事谣Proc发光", "Mage's Ballad Proc Glow");
            AddTranslation("Mage's Ballad Proc##Stacks", "贤者的叙事谣Proc##层数", "Mage's Ballad Proc##Stacks");
            AddTranslation("Mage's Ballad Threshold", "贤者的叙事谣阈值", "Mage's Ballad Threshold");
            AddTranslation("Mage's Ballad##Coda", "贤者的叙事谣##Coda", "Mage's Ballad##Coda");
            AddTranslation("Mage's Ballad##Song", "贤者的叙事谣##歌", "Mage's Ballad##Song");
            AddTranslation("Wanderer's Minuet", "放浪神的小步舞曲", "Wanderer's Minuet");
            AddTranslation("Wanderer's Minuet Stack##Stacks", "放浪神的小步舞曲层数##层数", "Wanderer's Minuet Stack##Stacks");
            AddTranslation("Wanderer's Minuet Stacks", "放浪神的小步舞曲层数", "Wanderer's Minuet Stacks");
            AddTranslation("Wanderer's Minuet Stacks Glow", "放浪神的小步舞曲层数发光", "Wanderer's Minuet Stacks Glow");
            AddTranslation("Wanderer's Minuet Threshold", "放浪神的小步舞曲阈值", "Wanderer's Minuet Threshold");
            AddTranslation("Wanderer's Minuet##Coda", "放浪神的小步舞曲##Coda", "Wanderer's Minuet##Coda");
            AddTranslation("Wanderer's Minuet##Song", "放浪神的小步舞曲##歌", "Wanderer's Minuet##Song");
            
            // 召唤师召唤兽颜色
            AddTranslation("Bahamut Color", "巴哈姆特颜色", "Bahamut Color");
            AddTranslation("Garuda Color", "迦楼罗颜色", "Garuda Color");
            AddTranslation("Garuda Stacks Color", "迦楼罗层数颜色", "Garuda Stacks Color");
            AddTranslation("Ifrit Color", "伊弗利特颜色", "Ifrit Color");
            AddTranslation("Ifrit Stacks Color", "伊弗利特层数颜色", "Ifrit Stacks Color");
            AddTranslation("Phoenix Color", "不死鸟颜色", "Phoenix Color");
            AddTranslation("Solar Bahamut Color", "烈日龙神颜色", "Solar Bahamut Color");
            AddTranslation("Titan Color", "泰坦颜色", "Titan Color");
            AddTranslation("Titan Stacks Color", "泰坦层数颜色", "Titan Stacks Color");
            
            // 召唤师其他
            AddTranslation("Show Primals", "显示召唤兽", "Show Primals");
            
            // 舞者舞步
            AddTranslation("Emboite", "蔷薇曲脚步", "Emboite");
            AddTranslation("Entrechat", "小鸟交叠跳", "Entrechat");
            AddTranslation("Jete", "绿叶小踢腿", "Jete");
            AddTranslation("Pirouette", "金冠趾尖转", "Pirouette");
            
            // 武僧相关
            AddTranslation("Blitz Timer Text", "必杀技计时器文字", "Blitz Timer Text");
            AddTranslation("Coeurl Color", "猛豹功力颜色", "Coeurl Color");
            AddTranslation("Formless Fist Color", "无相身形颜色", "Formless Fist Color");
            AddTranslation("Opo-opo Color", "魔猿功力颜色", "Opo-opo Color");
            AddTranslation("Raptor Color", "盗龙功力颜色", "Raptor Color");
            
            // 龙骑相关
            AddTranslation("Flank Ender", "侧翼终结", "Flank Ender");
            AddTranslation("Grim/Default Ender", "无方向/默认终结", "Grim/Default Ender");
            AddTranslation("Hind Ender", "背后终结", "Hind Ender");
            AddTranslation("Firstminds' Focus", "天龙眼", "Firstminds' Focus");
            
            // 占星相关
            AddTranslation("The Arrow Color", "放浪神之箭颜色", "The Arrow Color");
            AddTranslation("The Balance Color", "太阳神之衡颜色", "The Balance Color");
            AddTranslation("The Bole Color", "世界树之干颜色", "The Bole Color");
            AddTranslation("The Ewer Color", "河流神之瓶颜色", "The Ewer Color");
            AddTranslation("The Lady of Crowns Color", "王冠之贵妇颜色", "The Lady of Crowns Color");
            AddTranslation("The Lord of Crowns Color", "王冠之领主颜色", "The Lord of Crowns Color");
            AddTranslation("The Spear Color", "战争神之枪颜色", "The Spear Color");
            AddTranslation("The Spire Color", "建筑神之塔颜色", "The Spire Color");
            AddTranslation("Giant Dominance Glow##Star", "巨星主宰发光##Star", "Giant Dominance Glow##Star");
            AddTranslation("Earthly##Star", "地星##Star", "Earthly##Star");
            AddTranslation("Giant##Star", "巨星##Star", "Giant##Star");
            
            // 忍者相关
            AddTranslation("Kassatsu Color", "生杀予夺颜色", "Kassatsu Color");
            AddTranslation("Ten Chi Jin Color", "天地人颜色", "Ten Chi Jin Color");
            
            // 武士相关
            AddTranslation("Getsu", "月", "Getsu");
            AddTranslation("Setsu", "雪", "Setsu");
            
            // 战士相关
            AddTranslation("Inner Release On Cooldown Color", "原初的解放CD中颜色", "Inner Release On Cooldown Color");
            AddTranslation("Inner Release Ready Color", "原初的解放就绪颜色", "Inner Release Ready Color");
            AddTranslation("Nascent Chaos Color", "Nascent Chaos颜色", "Nascent Chaos Color");
            
            // 黑魔法师相关
            AddTranslation("Fire Background Color##MP", "火背景颜色##MP", "Fire Background Color##MP");
            AddTranslation("Fire Color##MP", "火颜色##MP", "Fire Color##MP");
            AddTranslation("Fire Color##Paradox", "火颜色##悖论", "Fire Color##Paradox");
            AddTranslation("Ice Background Color##MP", "冰背景颜色##MP", "Ice Background Color##MP");
            AddTranslation("Ice Color##MP", "冰颜色##MP", "Ice Color##MP");
            AddTranslation("Ice Color##Paradox", "冰颜色##悖论", "Ice Color##Paradox");
            
            // 暗黑骑士相关
            AddTranslation("Dark Arts Color##MP", "暗技颜色##MP", "Dark Arts Color##MP");
            
            // 镰刀相关
            AddTranslation("Lemure Shroud Color", "夜游魂颜色", "Lemure Shroud Color");
            AddTranslation("Void Shroud Color", "虚无魂颜色", "Void Shroud Color");
            
            // 绘灵相关
            AddTranslation("Black Paint Color", "黑色颜料颜色", "Black Paint Color");
            AddTranslation("Moogle Color", "莫古力颜色", "Moogle Color");
            AddTranslation("Pom Color", "绒球颜色", "Pom Color");
            AddTranslation("White Paint Color", "白色颜料颜色", "White Paint Color");
            AddTranslation("Wings Color", "翅膀颜色", "Wings Color");
            AddTranslation("Madeen Color", "马蒂恩颜色", "Madeen Color");
            AddTranslation("Claw Color", "兽爪颜色", "Claw Color");
            AddTranslation("Fangs Color", "尖牙颜色", "Fangs Color");
            
            // 武僧相关
            AddTranslation("Lunar Nadi", "太阴斗气", "Lunar Nadi");
            AddTranslation("Lunar Nadi Color", "太阴斗气颜色", "Lunar Nadi Color");
            AddTranslation("Solar Nadi", "太阳斗气", "Solar Nadi");
            AddTranslation("Solar Nadi Color", "太阳斗气颜色", "Solar Nadi Color");
            
            // 其他相关
            AddTranslation("Battery Color", "电能颜色", "Battery Color");
            AddTranslation("Waning Crescent Color", "狂战士化的副作用颜色", "Waning Crescent Color");
            AddTranslation("Waxing Crescent Color", "狂战士化颜色", "Waxing Crescent Color");
            AddTranslation("Ready to Reawaken Color", "祖灵降临颜色", "Ready to Reawaken Color");
            
            // 方向相关
            AddTranslation("Corner", "方位锚点", "Corner");
            
            // 其他配置
            AddTranslation("Cast Name", "咏唱名称", "Cast Name");
            AddTranslation("Fake Name", "预览名称", "Fake Name");
            AddTranslation("Shields", "护盾", "Shields");
            
            // 最后一批帮助文本（长文本保持简洁）
            
            // ==================================================================
            // 第13批：长帮助文本和说明（最后一批）
            // ==================================================================
            
            AddTranslation("Default means the bar will be drawn using the global gradient configuration for bars found in Colors > Misc.", 
                "默认表示该条将使用\"颜色 > 杂项\"中的全局渐变配置进行绘制。", 
                "Default means the bar will be drawn using the global gradient configuration for bars found in Colors > Misc.");
            
            AddTranslation("Determines for how long the icons will show after a ready check is finished.", 
                "确定确认完成后图标显示多长时间。", 
                "Determines for how long the icons will show after a ready check is finished.");
            
            AddTranslation("Disclaimer: DelvUI relies heavily on the the game's default nameplates so this setting won't be a huge improvement.\nThis setting tries to prevent nameplates from being cutoff in the border of the screen, but it won't keep showing nameplates that the game wouldn't.", 
                "免责声明：DelvUI很大程度上依赖于游戏默认角色名牌，因此此设置不会有巨大改善。\n此设置尝试防止角色名牌在屏幕边缘被截断，但不会显示游戏本身不会显示的角色名牌。", 
                "Disclaimer: DelvUI relies heavily on the the game's default nameplates so this setting won't be a huge improvement.\nThis setting tries to prevent nameplates from being cutoff in the border of the screen, but it won't keep showing nameplates that the game wouldn't.");
            
            AddTranslation("Enabling this will disable right clicking buffs off, or the shortcut to blacklist/whitelist a status effect.", 
                "启用后将禁用右键点击移除增益，或将状态效果加入黑/白名单的快捷方式。", 
                "Enabling this will disable right clicking buffs off, or the shortcut to blacklist/whitelist a status effect.");
            
            AddTranslation("Enabling this will make it so this element is ignored by mouseover completely.\nThe area can still be defined for left and right clicks.", 
                "启用后将使鼠标悬停完全忽略此元素。\n该区域仍可定义左键和右键点击。", 
                "Enabling this will make it so this element is ignored by mouseover completely.\nThe area can still be defined for left and right clicks.");
            
            AddTranslation("Enabling this will override other border settings!", 
                "启用后将覆盖其他边框设置！", 
                "Enabling this will override other border settings!");
            
            AddTranslation("If enabled, \"Permanent Effects First\" and \"My Effects First\" will be ignored!", 
                "如果启用，\"永久效果优先\"和\"我的效果优先\"将被忽略！", 
                "If enabled, \"Permanent Effects First\" and \"My Effects First\" will be ignored!");
            
            AddTranslation("If enabled, all HUD elements preview modes are disabled when DelvUI's setting window is closed.", 
                "如果启用，关闭DelvUI设置窗口时将禁用所有HUD元素预览模式。", 
                "If enabled, all HUD elements preview modes are disabled when DelvUI's setting window is closed.");
            
            AddTranslation("If enabled, DelvUI will use its own style for the setting window instead of the general Dalamud style.", 
                "如果启用，DelvUI将对设置窗口使用自己的样式，而不是通用的Dalamud样式。", 
                "If enabled, DelvUI will use its own style for the setting window instead of the general Dalamud style.");
            
            AddTranslation("This background color will be used when the player's data couldn't be retreived (i.e. player is disconnected)", 
                "当无法检索到玩家数据时（即玩家断线），将使用此背景颜色。", 
                "This background color will be used when the player's data couldn't be retreived (i.e. player is disconnected)");
            
            AddTranslation("This controls wheter you'll see nameplates through walls and objects.\n\nDisabled: Nameplates will always be seen for units in range.\nSimple: Uses simple calculations to check if a nameplate is being covered by walls or objects. Use this for better performance.\nFull: Uses more complex calculations to check if a nameplate is being covered by walls or objects. Use this for better results.", 
                "此选项控制是否能透过墙壁和物体看到角色名牌。\n\n禁用：始终显示范围内单位的角色名牌。\n简单：使用简单计算检查角色名牌是否被墙壁或物体遮挡。性能更好。\n完全：使用更复杂计算检查角色名牌是否被墙壁或物体遮挡。效果更好。", 
                "This controls wheter you'll see nameplates through walls and objects.\n\nDisabled: Nameplates will always be seen for units in range.\nSimple: Uses simple calculations to check if a nameplate is being covered by walls or objects. Use this for better performance.\nFull: Uses more complex calculations to check if a nameplate is being covered by walls or objects. Use this for better results.");
            
            AddTranslation("This controls which kind of objects will cover nameplates.\n\n\nWalls: Default setting. Only walls will cover nameplates.\n\nWalls and Objects: Some objects like columns and trees will also cover nameplates.\nThis Occlusion Type can yield some unexpected results like nameplates for NPCs behind counters not being visible.", 
                "此选项控制哪些对象会遮挡角色名牌。\n\n\n墙壁：默认设置。仅墙壁会遮挡角色名牌。\n\n墙壁和物体：柱子和树木等物体也会遮挡角色名牌。\n此遮挡类型可能产生一些意外结果，如柜台后NPC的角色名牌不可见。", 
                "This controls which kind of objects will cover nameplates.\n\n\nWalls: Default setting. Only walls will cover nameplates.\n\nWalls and Objects: Some objects like columns and trees will also cover nameplates.\nThis Occlusion Type can yield some unexpected results like nameplates for NPCs behind counters not being visible.");
            
            AddTranslation("This is the border thickness that will be used when the border active (aka targetted, showing enmity, etc).", 
                "这是边框激活时（即被锁定、显示仇恨等）使用的边框厚度。", 
                "This is the border thickness that will be used when the border active (aka targetted, showing enmity, etc).");
            
            AddTranslation("This is the border thickness that will be used when the border is in the default state (aka not targetted, not showing enmity, etc).", 
                "这是边框处于默认状态时（即未被锁定、未显示仇恨等）使用的边框厚度。", 
                "This is the border thickness that will be used when the border is in the default state (aka not targetted, not showing enmity, etc).");
            
            AddTranslation("This will change the color of the bar when the enemy is targeting the player.", 
                "当敌人锁定玩家时，这将改变条的颜色。", 
                "This will change the color of the bar when the enemy is targeting the player.");
            
            AddTranslation("This will hide any label that has a health tag if the character doesn't have health (ie minions, friendly npcs, etc)", 
                "如果角色没有生命值（如宠物、友好NPC等），这将隐藏任何带有生命值标签的标签。", 
                "This will hide any label that has a health tag if the character doesn't have health (ie minions, friendly npcs, etc)");
            
            AddTranslation("This will hide the healthbar when the characters HP has been brought to zero", 
                "当角色生命值降为零时，这将隐藏生命值条。", 
                "This will hide the healthbar when the characters HP has been brought to zero");
            
            AddTranslation("This will swap the contents of these labels depending on if the title goes before or after the name of a player.", 
                "这将根据称号在玩家名称之前还是之后来交换这些标签的内容。", 
                "This will swap the contents of these labels depending on if the title goes before or after the name of a player.");
            
            AddTranslation("When enabled and if the enemy has a sign assigned, the sign icon will be drawn instead of the order label.", 
                "启用后，如果敌人分配了标记，将绘制标记图标而不是顺序标签。", 
                "When enabled and if the enemy has a sign assigned, the sign icon will be drawn instead of the order label.");
            
            AddTranslation("When enabled, DelvUI will use your system's regional format settings when showing numbers.\nWhen disabled, DelvUI will use English number formatting instead.", 
                "启用后，DelvUI将使用您系统的区域格式设置来显示数字。\n禁用时，DelvUI将使用英语数字格式。", 
                "When enabled, DelvUI will use your system's regional format settings when showing numbers.\nWhen disabled, DelvUI will use English number formatting instead.");
            
            AddTranslation("When enabled, the icon will anchor to the Health Bar if it's visible.\nIf the Health Bar disappears, it will anchor back to the desired label.", 
                "启用后，如果生命值条可见，图标将锚定到生命值条。\n如果生命值条消失，它将锚定回所需标签。", 
                "When enabled, the icon will anchor to the Health Bar if it's visible.\nIf the Health Bar disappears, it will anchor back to the desired label.");
            
            AddTranslation("When enabled, this hides the middle chunk that shows which creature parts were already drawn.", 
                "启用后，这将隐藏显示已绘制生物部分的中间块。", 
                "When enabled, this hides the middle chunk that shows which creature parts were already drawn.");
            
            AddTranslation("When enabled: All your actions will automatically assume mouseover when your cursor is on top of a unit frame.\nMouseover macros or other mouseover plugins are not necessary and WON'T WORK in this mode!\n\nWhen disabled: DelvUI unit frames will behave like the game's ones.\nYou'll need to use mouseover macros or other mouseover related plugins in this mode.", 
                "启用时：当光标位于情报框体上方时，所有动作将自动假定为鼠标悬停。\n此模式下不需要鼠标悬停宏或其他鼠标悬停插件，它们也不会工作！\n\n禁用时：DelvUI情报框体将表现得像游戏原生框体。\n此模式下需要使用鼠标悬停宏或其他鼠标悬停相关插件。", 
                "When enabled: All your actions will automatically assume mouseover when your cursor is on top of a unit frame.\nMouseover macros or other mouseover plugins are not necessary and WON'T WORK in this mode!\n\nWhen disabled: DelvUI unit frames will behave like the game's ones.\nYou'll need to use mouseover macros or other mouseover related plugins in this mode.");

                
            // 短词补充
            AddTranslation("Auto Hide", "自动隐藏", "Auto Hide");
            AddTranslation("Auto Hide After Combat", "战斗后自动隐藏", "Auto Hide After Combat");
            AddTranslation("Auto Hide Delay", "自动隐藏延迟", "Auto Hide Delay");
            AddTranslation("Center", "居中", "Center");
            AddTranslation("Centered", "居中", "Centered");
            AddTranslation("Clamp To Screen", "限制在屏幕内", "Clamp To Screen");
            AddTranslation("Color By Job", "按职业着色", "Color By Job");
            AddTranslation("Color By Role", "按职能着色", "Color By Role");
            AddTranslation("Color By Health", "按生命值着色", "Color By Health");
            AddTranslation("Columns", "列数", "Columns");
            AddTranslation("Consolidate", "整合", "Consolidate");
            AddTranslation("Count", "数量", "Count");
            AddTranslation("Custom", "自定义", "Custom");
            AddTranslation("Desaturate", "降低饱和度", "Desaturate");
            AddTranslation("Direction", "方向", "Direction");
            AddTranslation("Disable", "禁用", "Disable");
            AddTranslation("Disabled", "已禁用", "Disabled");
            AddTranslation("Dispellable", "可驱散", "Dispellable");
            AddTranslation("Down", "向下", "Down");
            AddTranslation("Draw", "绘制", "Draw");
            AddTranslation("Enabled (when activated manually)", "启用（手动激活时）", "Enabled (when activated manually)");
            AddTranslation("Fade", "淡化", "Fade");
            AddTranslation("Fade Out", "淡出", "Fade Out");
            AddTranslation("Fill", "填充", "Fill");
            AddTranslation("Filter", "过滤", "Filter");
            AddTranslation("Glow", "发光", "Glow");
            AddTranslation("Health", "生命值", "Health");
            AddTranslation("Hide When Full", "满时隐藏", "Hide When Full");
            AddTranslation("Horizontal", "水平", "Horizontal");
            AddTranslation("Icon", "图标", "Icon");
            AddTranslation("Ignore", "忽略", "Ignore");
            AddTranslation("Invert", "反转", "Invert");
            AddTranslation("Inverted", "已反转", "Inverted");
            AddTranslation("Keep Aspect Ratio", "保持宽高比", "Keep Aspect Ratio");
            AddTranslation("Left", "左", "Left");
            AddTranslation("Lock", "锁定", "Lock");
            AddTranslation("Locked", "已锁定", "Locked");
            AddTranslation("Mana", "魔力", "Mana");
            AddTranslation("Mirror", "镜像", "Mirror");
            AddTranslation("Mode", "模式", "Mode");
            AddTranslation("None", "无", "None");
            AddTranslation("Only", "仅", "Only");
            AddTranslation("Opacity", "不透明度", "Opacity");
            AddTranslation("Options", "选项", "Options");
            AddTranslation("Outline", "描边", "Outline");
            AddTranslation("Radius", "半径", "Radius");
            AddTranslation("Range", "范围", "Range");
            AddTranslation("Reverse", "反向", "Reverse");
            AddTranslation("Right", "右", "Right");
            AddTranslation("Rotation", "旋转", "Rotation");
            AddTranslation("Rows", "行数", "Rows");
            AddTranslation("Shadow", "阴影", "Shadow");
            AddTranslation("Simple", "简单", "Simple");
            AddTranslation("Smooth", "平滑", "Smooth");
            AddTranslation("Sorting", "排序", "Sorting");
            AddTranslation("Stacks", "层数", "Stacks");
            AddTranslation("Style", "样式", "Style");
            AddTranslation("Thickness", "粗细", "Thickness");
            AddTranslation("Time", "时间", "Time");
            AddTranslation("Timer", "计时器", "Timer");
            AddTranslation("Toggle", "切换", "Toggle");
            AddTranslation("Transparency", "透明度", "Transparency");
            AddTranslation("Type", "类型", "Type");
            AddTranslation("Up", "向上", "Up");
            AddTranslation("Use", "使用", "Use");
            AddTranslation("Vertical", "垂直", "Vertical");
            AddTranslation("Visible When", "可见条件", "Visible When");
            AddTranslation("X Offset", "X偏移", "X Offset");
            AddTranslation("Y Offset", "Y偏移", "Y Offset");
            AddTranslation("Ka", "Ka", "Ka");
            
            // Tooltip strings
            AddTranslation("Hide HUD", "隐藏HUD", "Hide HUD");
            AddTranslation("Show HUD", "显示HUD", "Show HUD");
            AddTranslation("Changelog", "更新日志", "Changelog");
            AddTranslation("Close", "关闭", "Close");
            AddTranslation("Unlock HUD", "解锁HUD", "Unlock HUD");
            AddTranslation("Tip the developer at ko-fi.com", "在ko-fi.com上给开发者打赏", "Tip the developer at ko-fi.com");
            AddTranslation("DelvUI Discord", "DelvUI Discord", "DelvUI Discord");
            // FontsConfig strings
            AddTranslation("Default font not found in \"%appdata%/Roaming/XIVLauncher/InstalledPlugins/DelvUI/Media/Fonts/Expressway.ttf\"", "在\"%appdata%/Roaming/XIVLauncher/InstalledPlugins/DelvUI/Media/Fonts/Expressway.ttf\"中未找到默认字体", "Default font not found in \"%appdata%/Roaming/XIVLauncher/InstalledPlugins/DelvUI/Media/Fonts/Expressway.ttf\"");
            AddTranslation("Path", "路径", "Path");
            AddTranslation("Actions", "操作", "Actions");
            
        }

        /// <summary>
        /// 获取所有已翻译的键
        /// </summary>
        public HashSet<string> GetAllTranslationKeys()
        {
            return new HashSet<string>(_translations.Keys);
        }

        /// <summary>
        /// 获取重复定义的键
        /// </summary>
        public Dictionary<string, int> GetDuplicateDefinitions()
        {
            return new Dictionary<string, int>(_duplicateDefinitions);
        }

        private void AddTranslation(string key, string chinese, string english)
        {
            if (_translations.ContainsKey(key))
            {
                // 记录重复定义
                if (_duplicateDefinitions.ContainsKey(key))
                {
                    _duplicateDefinitions[key]++;
                }
                else
                {
                    _duplicateDefinitions[key] = 2; // 第一次检测到重复，说明出现了第2次
                }
            }
            else
            {
                _translations[key] = new Dictionary<Language, string>();
            }

            _translations[key][Language.ChineseSimplified] = chinese;
            _translations[key][Language.English] = english;
        }
    }
}

