#region

using System.Linq;
using System.Collections.Generic;

#endregion

namespace Gemini.Modules.Settings.ViewModels
{
    public class SettingsPageViewModel
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public List<ISettingsEditor> Editors { get; }
        public List<SettingsPageViewModel> Children { get; }

        public bool IsVisible => Editors.Any(e => !(e is ISettingsEditorEx ee) || ee.IsVisible) || Children.Any(p => p.IsVisible);

        public SettingsPageViewModel()
        {
            Children = new List<SettingsPageViewModel>();
            Editors = new List<ISettingsEditor>();
        }
    }
}
