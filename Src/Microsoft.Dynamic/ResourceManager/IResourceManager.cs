using System;
using System.Globalization;

namespace Microsoft.Scripting
{
    public interface IResourceManager: IDisposable {
        /// <summary>
        /// 切换资源语言
        /// </summary>
        /// <param name="languageid">语言Id同CultureInfo</param>
        void ChangeLanguage(CultureInfo languageid);

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key">资源ID</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        string GetResource(string key,string defaultValue="");

        /// <summary>
        /// 另存为资源
        /// </summary>
        /// <param name="cultureInfo"></param>
        void SaveAs(CultureInfo cultureInfo);

        /// <summary>
        /// 初始化App的名称
        /// </summary>
        /// <param name="appName"></param>
        void Initialize(string appName);
    }
}
