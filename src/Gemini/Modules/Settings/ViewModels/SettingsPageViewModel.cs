#region

using System.Linq;
using System.Collections.Generic;
using Caliburn.Micro;

#endregion

namespace Gemini.Modules.Settings.ViewModels
{
    public class SettingsPageViewModel: PropertyChangedBase
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<ISettingsEditor> Editors { get; }
        public List<SettingsPageViewModel> Children { get; }

        public bool IsVisible => Editors.Any(e => e.IsVisible) || Children.Any(p => p.IsVisible);

        public SettingsPageViewModel()
        {
            Children = new List<SettingsPageViewModel>();
            Editors = new List<ISettingsEditor>();
        }

        public void Load() {
            foreach(var e in Editors.Where(e => e.IsVisible)) e.Load();
            foreach(var p in Children.Where(e => e.IsVisible)) p.Load();
        }
    }
}
