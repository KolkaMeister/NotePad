using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePad.models
{
    public class DocumentModel
    {
        public delegate void DocumentPropertyChangeHandler(string newValue);
        public event DocumentPropertyChangeHandler PathChanged;
        public event DocumentPropertyChangeHandler NameChanged;
        public event DocumentPropertyChangeHandler TextChanged;
        private string _path;

        public bool IsEmpty => string.IsNullOrEmpty(_path)||string.IsNullOrEmpty(_name);

        public string Path 
        {
            get { return _path; }
            set 
            {
                _path=value;
                PathChanged?.Invoke(value); 
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NameChanged?.Invoke(value);
            }
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                TextChanged?.Invoke(value);
            }
        }
    }
}
