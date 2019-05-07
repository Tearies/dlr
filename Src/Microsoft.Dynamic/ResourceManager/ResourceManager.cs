using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Microsoft.Scripting {
    public class ResourceManager : IResourceManager {
        private CultureInfo cultureInfo;
        private string currentAppName = "DLR";
        private DataContractJsonSerializer defaultSerializer;
        private string excutablePath;
        private LanguagePack languagePack;
        private const string resourceDir = @"\locales\";
        private const string languageExt = ".pak";
        public static readonly IResourceManager Default = new Lazy<IResourceManager>(() => new ResourceManager()).Value;

        private ResourceManager() {
            languagePack = new LanguagePack();
            defaultSerializer = new DataContractJsonSerializer(typeof(LanguagePack),
                new DataContractJsonSerializerSettings() {
                    UseSimpleDictionaryFormat = false
                });

            GetExcutablePath();
            ChangeLanguage(GetDefaultCulture());
        }

        /// <summary>
        /// 切换资源语言
        /// </summary>
        /// <param name="languageid">语言Id同CultureInfo</param>
        public void ChangeLanguage(CultureInfo languageid) {
            cultureInfo = languageid;
            LoadResource();
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key">资源ID</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public string GetResource(string key, string defaultValue = "") {
            var findItem = languagePack.Items.FirstOrDefault(o => o.Key == key);
            if (findItem != null) {
                var sb = new StringBuilder();
                sb.Append(findItem.Value);
                if (!string.IsNullOrEmpty(findItem.Solution)) sb.AppendFormat("Tips:{0}", findItem.Solution);
                return sb.ToString();
            }

            languagePack.Items.Add(new LanguagePackItem() {
                Key = key,
                Value = defaultValue
            });
            return defaultValue;
        }

        /// <summary>
        /// 初始化App的名称
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="languageid"></param>
        public void Initialize(string appName, CultureInfo languageid = null) {
            currentAppName = appName;
            if (languageid == null) languageid = GetDefaultCulture();
            ChangeLanguage(languageid);
        }

        /// <summary>
        /// 另存为资源
        /// </summary>
        /// <param name="cultureInfo"></param>
        public void SaveAs(CultureInfo cultureInfo) {
            var resourcepath = BuildResourcePath(cultureInfo);
            var tmpPack = languagePack.Clone() as LanguagePack;
            tmpPack.CultureInfo = GetCultureInfo(cultureInfo);
            FlushToLocal(resourcepath, tmpPack);
        }

        private string BuildResourcePath(CultureInfo cultureInfo) {
            var culId = GetCultureInfo(cultureInfo);
            return excutablePath + resourceDir + currentAppName + @"." + culId + languageExt;
        }

        private void FlushToLocal(string resource, LanguagePack language) {
            var resourcedir = Path.GetDirectoryName(resource);
            if (!Directory.Exists(resourcedir)) Directory.CreateDirectory(resourcedir);
            using (var fs = File.Open(resource, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                try {
                    defaultSerializer.WriteObject(fs, language);
                } catch (Exception e) {
                    var ss = e;
                }
            }
        }

        private string GetCultureInfo(CultureInfo cultureInfo) {
            return cultureInfo.Parent.NativeName;
        }

        private CultureInfo GetDefaultCulture() {
            return CultureInfo.CurrentCulture;
        }

        private void GetExcutablePath() {
            var currentAss = Assembly.GetEntryAssembly();
            if (currentAss == null)
                currentAss = typeof(IResourceManager).Assembly;
            excutablePath = Path.GetDirectoryName(currentAss.Location);
        }

        private LanguagePack LoadReourceDic(string resource) {
            LanguagePack resourcedic = null;
            using (var fs = File.OpenRead(resource)) {
                try {
                    resourcedic = defaultSerializer.ReadObject(fs) as LanguagePack;
                } catch {
                    resourcedic = null;
                }
            }

            return resourcedic;
        }

        private void LoadResource() {
            var resource = BuildResourcePath(cultureInfo);
            if (!File.Exists(resource)) return;

            var languagetmpPack = LoadReourceDic(resource);
            if (languagetmpPack != null)
                if (languagetmpPack.CultureInfo == GetCultureInfo(cultureInfo))
                    languagetmpPack.Items.ForEach(o => { languagePack.Items.Add(o); });
        }
    }
}
