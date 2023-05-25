using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NotePad.models
{
    public class FormatModel 
    {
        public delegate void FormatPropertyChangeHandler<T>(T newValue);
        public event FormatPropertyChangeHandler<Brush> ColorChanged;
        public event FormatPropertyChangeHandler<double> FontSizeChanged;
        public event FormatPropertyChangeHandler<FontFamily> FontFamilyChanged;
        private Brush _color;
        public Brush Color
        {
            get { return _color; }
            set 
            {
                _color = value;
                ColorChanged?.Invoke(value); 
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get { return _fontSize; }
            set 
            {
                _fontSize= value;
                FontSizeChanged?.Invoke(value); 
            }
        }

        private FontFamily _fontFamily;
        public FontFamily FontFamily
        {
            get { return _fontFamily; }
            set 
            {
                _fontFamily=value;
                FontFamilyChanged?.Invoke(value); 
            }
        }
    }
}
