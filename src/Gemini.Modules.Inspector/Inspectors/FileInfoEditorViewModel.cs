using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Gemini.Framework.Results;
using Microsoft.Win32;

namespace Gemini.Modules.Inspector.Inspectors
{
    public class FileInfoEditorViewModel : EditorBase<FileInfo>, ILabelledInspector
    {
        public IEnumerable<IResult> Choose()
        {
            var fileDialog = new OpenFileDialog();
            yield return Show.CommonDialog(fileDialog);

            Value = new FileInfo(fileDialog.FileName);
            ValueString = Value.FullName;
        }

        private string _valueString;
        public string ValueString
        {
            get { return _valueString; }
            set
            {
                _valueString = value;
                NotifyOfPropertyChange(() => ValueString);
            }
        }
    }
}
