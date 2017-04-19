#region

using System.Collections.Generic;

#endregion

namespace Gemini.Modules.Settings.ViewModels
{
    public class SettingsPageViewModel
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public List<ISettingsEditor> Editors { get; private set; }
        public List<SettingsPageViewModel> Children { get; private set; }

        public SettingsPageViewModel()
        {
            Children = new List<SettingsPageViewModel>();
            Editors = new List<ISettingsEditor>();
        }
    }
}