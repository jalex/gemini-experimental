﻿using System;
using System.Xml.Serialization;
using Caliburn.Micro;

namespace Gemini.Modules.RecentFiles.ViewModels
{
    [Serializable]
    public class RecentFileItemViewModel : PropertyChangedBase
    {
        private string _filePath;
        
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                NotifyOfPropertyChange(() => DisplayName);
                NotifyOfPropertyChange(() => FilePath);
            }
        }

        // TODO: will implement Pinned
        private bool _pinned;
        public bool Pinned
        {
            get { return _pinned; }
            set
            {
                _pinned = value;
                NotifyOfPropertyChange(() => Pinned);
            }
        }

        public string DisplayName { get { return ShortenPath(_filePath); } }

        // http://stackoverflow.com/questions/8360360/function-to-shrink-file-path-to-be-more-human-readable
        private string ShortenPath(string path, int maxLength = 50)
        {
            var splits = path.Split('\\');

            var output = "";

            if (splits.Length > 4)
                output = splits[0] + "\\" + splits[1] + "\\...\\" + splits[splits.Length - 2] + "\\" + splits[splits.Length - 1];
            else
                output = string.Join("\\", splits, 0, splits.Length);

            return output;
        }
    }
}