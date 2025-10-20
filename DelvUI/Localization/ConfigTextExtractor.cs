using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DelvUI.Config;
using DelvUI.Config.Attributes;

namespace DelvUI.Localization
{
    /// <summary>
    /// 翻译分析结果
    /// </summary>
    public class TranslationAnalysisResult
    {
        public HashSet<string> RequiredKeys { get; set; } = new HashSet<string>();
        public HashSet<string> TranslatedKeys { get; set; } = new HashSet<string>();
        public HashSet<string> UntranslatedKeys { get; set; } = new HashSet<string>();
        public HashSet<string> UnusedKeys { get; set; } = new HashSet<string>();
        public Dictionary<string, int> DuplicateKeys { get; set; } = new Dictionary<string, int>();
        
        public double CoveragePercentage => RequiredKeys.Count > 0 
            ? (double)(RequiredKeys.Count - UntranslatedKeys.Count) / RequiredKeys.Count * 100.0 
            : 100.0;
    }

    /// <summary>
    /// 配置文本提取器 - 分析翻译状态
    /// </summary>
    public class ConfigTextExtractor
    {
        private readonly HashSet<string> _requiredTexts = new HashSet<string>();

        /// <summary>
        /// 分析翻译状态
        /// </summary>
        public TranslationAnalysisResult AnalyzeTranslations()
        {
            _requiredTexts.Clear();

            // 1. 提取所有需要翻译的文本
            ExtractAllRequiredTexts();

            // 2. 获取已翻译的键
            var translatedKeys = LocalizationManager.Instance.GetAllTranslationKeys();

            // 3. 从 LocalizationManager 获取重复定义的键
            var duplicates = LocalizationManager.Instance.GetDuplicateDefinitions();

            // 4. 对比分析
            var result = new TranslationAnalysisResult
            {
                RequiredKeys = new HashSet<string>(_requiredTexts),
                TranslatedKeys = translatedKeys,
                UntranslatedKeys = new HashSet<string>(_requiredTexts.Where(k => !translatedKeys.Contains(k))),
                UnusedKeys = new HashSet<string>(translatedKeys.Where(k => !_requiredTexts.Contains(k))),
                DuplicateKeys = duplicates
            };

            return result;
        }

        /// <summary>
        /// 提取所有需要翻译的文本
        /// </summary>
        private void ExtractAllRequiredTexts()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var configTypes = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(PluginConfigObject)));

                foreach (var type in configTypes)
                {
                    ExtractFromType(type);
                }
            }
            catch (Exception ex)
            {
                Plugin.Logger.Error($"提取文本失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 从类型中提取文本
        /// </summary>
        private void ExtractFromType(Type type)
        {
            // Section
            var sectionAttr = type.GetCustomAttribute<SectionAttribute>();
            if (sectionAttr != null)
            {
                Add(sectionAttr.SectionName);
            }

            // SubSection
            var subSectionAttrs = type.GetCustomAttributes<SubSectionAttribute>();
            foreach (var attr in subSectionAttrs)
            {
                Add(attr.SubSectionName);
            }

            // Fields
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                ExtractFromMember(field.GetCustomAttributes());
            }
        }

        /// <summary>
        /// 从成员属性中提取文本
        /// </summary>
        private void ExtractFromMember(IEnumerable<Attribute> attributes)
        {
            foreach (var attr in attributes)
            {
                // RadioSelector 特殊处理：提取 _options 而不是 friendlyName
                if (attr.GetType().Name == "RadioSelector")
                {
                    var optionsField = attr.GetType().GetField("_options", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (optionsField != null)
                    {
                        var options = optionsField.GetValue(attr) as string[];
                        if (options != null)
                        {
                            foreach (var option in options)
                            {
                                Add(option);
                            }
                        }
                    }
                    // RadioSelector 的 friendlyName 是连接后的字符串，不需要翻译
                    continue;
                }

                // ConfigAttribute 的 friendlyName 和 help
                if (attr is ConfigAttribute configAttr)
                {
                    Add(configAttr.friendlyName);
                    Add(configAttr.help);
                }

                // ComboAttribute 的选项
                if (attr is ComboAttribute comboAttr)
                {
                    foreach (var option in comboAttr.options)
                    {
                        Add(option);
                    }
                }

                // DragDropHorizontalAttribute 的名称
                if (attr is DragDropHorizontalAttribute dragDropAttr)
                {
                    foreach (var name in dragDropAttr.names)
                    {
                        Add(name);
                    }
                }

                // NestedConfigAttribute 的友好名称
                if (attr is NestedConfigAttribute nestedAttr)
                {
                    Add(nestedAttr.friendlyName);
                }
            }
        }

        /// <summary>
        /// 添加文本到集合
        /// </summary>
        private void Add(string? text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _requiredTexts.Add(text);
            }
        }

        /// <summary>
        /// 导出分析结果（精简格式）
        /// </summary>
        public void ExportAnalysis(TranslationAnalysisResult result, string outputPath)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# DelvUI 翻译状态分析");
            sb.AppendLine();
            sb.AppendLine($"分析时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine();

            // 统计
            sb.AppendLine("## 📊 统计");
            sb.AppendLine();
            sb.AppendLine($"- 需要翻译: {result.RequiredKeys.Count} 个");
            sb.AppendLine($"- 已翻译: {result.TranslatedKeys.Count} 个");
            sb.AppendLine($"- 未翻译: {result.UntranslatedKeys.Count} 个");
            sb.AppendLine($"- 未使用: {result.UnusedKeys.Count} 个");
            sb.AppendLine($"- 重复键: {result.DuplicateKeys.Count} 个");
            sb.AppendLine($"- 覆盖率: {result.CoveragePercentage:F1}%");
            sb.AppendLine();

            // 未翻译
            if (result.UntranslatedKeys.Count > 0)
            {
                sb.AppendLine("## ⚠️ 未翻译（需要添加）");
                sb.AppendLine();
                sb.AppendLine("```csharp");
                foreach (var key in result.UntranslatedKeys.OrderBy(k => k))
                {
                    string escaped = Escape(key);
                    sb.AppendLine($"AddTranslation(\"{escaped}\", \"TODO\", \"{escaped}\");");
                }
                sb.AppendLine("```");
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("## ✅ 全部已翻译");
                sb.AppendLine();
            }

            // 重复键（可选信息，折叠显示）
            if (result.DuplicateKeys.Count > 0)
            {
                sb.AppendLine("<details>");
                sb.AppendLine($"<summary>⚠️ 重复定义的键（{result.DuplicateKeys.Count} 个，在 LocalizationManager 中被多次 AddTranslation）</summary>");
                sb.AppendLine();
                sb.AppendLine("| 键 | 定义次数 |");
                sb.AppendLine("|---|---|");
                foreach (var kvp in result.DuplicateKeys.OrderByDescending(k => k.Value).ThenBy(k => k.Key))
                {
                    sb.AppendLine($"| `{kvp.Key}` | {kvp.Value} |");
                }
                sb.AppendLine();
                sb.AppendLine("</details>");
                sb.AppendLine();
            }

            // 未使用（可选信息，折叠显示）
            if (result.UnusedKeys.Count > 0)
            {
                sb.AppendLine("<details>");
                sb.AppendLine($"<summary>🔍 未使用的翻译（{result.UnusedKeys.Count} 个，可能用于运行时或其他地方）</summary>");
                sb.AppendLine();
                foreach (var key in result.UnusedKeys.OrderBy(k => k))
                {
                    sb.AppendLine($"- `{key}`");
                }
                sb.AppendLine();
                sb.AppendLine("</details>");
                sb.AppendLine();
            }

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        private string Escape(string text)
        {
            return text.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        public int GetTotalCount() => _requiredTexts.Count;
    }
}
