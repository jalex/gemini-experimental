namespace Gemini.Modules.Settings
{
    public interface ISettingsEditor
    {
        string SettingsPageName { get; }
        string SettingsPagePath { get; }
        bool IsVisible { get; }

        void Load();
        void ApplyChanges();
    }

    public interface ISettingsEditorOrder
    {
        int Order { get; }
    }
}
