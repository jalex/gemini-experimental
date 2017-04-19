namespace Gemini.Modules.Settings
{
    public interface ISettingsEditor
    {
        string SettingsPageName { get; }
        string SettingsPagePath { get; }

        void ApplyChanges();
    }

    public interface ISettingsEditorOrder {

        int Order { get; }
    }
}