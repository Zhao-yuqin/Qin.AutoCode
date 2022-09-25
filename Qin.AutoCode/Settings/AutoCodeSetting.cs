using Qin.AutoCode.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qin.AutoCode.Settings
{
    public class AutoCodeSetting : IAutoCodeSetting
    {

        #region Static

        private static List<string> defaultReferences;

        private static string defaultNamespace;

        static AutoCodeSetting()
        {

        }
        #endregion

        private List<string> _references;
        private string _namespace;
        private string _inherit;
        public List<string> References
        {
            get => _references ?? defaultReferences;
            set => _references = value;
        }

        public string Namespace
        {
            get => string.IsNullOrEmpty(_namespace) ? defaultNamespace : _namespace;
            set => _namespace = value;
        }



        public string Inherit
        {
            get => _inherit;
            set => _inherit = value;
        }



        public AutoCodeSetting()
        {
        }
    }
}
