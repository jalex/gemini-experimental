using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Caliburn.Micro;
using Gemini.Framework.Results;
using Microsoft.Win32;

namespace Gemini.Modules.Inspector.Inspectors
{
    public class FilePath
    {
        public string FullName { get; set; }

        public FilePath()
        {
            FullName = "";
        }

        public FilePath(string fullName)
        {
            FullName = fullName;
        }
    }

    public class FilePathEditorViewModel : EditorBase<FilePath>, ILabelledInspector
    {
        public IEnumerable<IResult> Choose()
        {
            var fileDialog = new OpenFileDialog();
            yield return Show.CommonDialog(fileDialog);
            
            Value = new FilePath(fileDialog.FileName);
        }
    }
}
