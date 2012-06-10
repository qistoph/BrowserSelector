using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelector
{
    public class EnumComboBox<T> : ComboBox
    {
        private Type EType;

        public EnumComboBox()
        {
            EType = typeof(T);
            string[] names = Enum.GetNames(EType);
            Items.AddRange(names);
        }

        public EnumComboBox(ComboBox placeHolder)
            : this()
        {
            this.Size = placeHolder.Size;
            this.Location = placeHolder.Location;
            this.DropDownStyle = placeHolder.DropDownStyle;
            placeHolder.Parent.Controls.Add(this);
            placeHolder.Parent.Controls.Remove(placeHolder);
        }

        public new T SelectedItem
        {
            get
            {
                return (T)Enum.Parse(EType, (string)base.SelectedItem);
            }
            set
            {
                base.SelectedItem = (string)Enum.GetName(EType, value);
            }
        }

        public static EnumComboBox<T> Replace(ref ComboBox target)
        {
            EnumComboBox<T> newCombo = new EnumComboBox<T>();
            newCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            newCombo.Size = target.Size;
            newCombo.Location = target.Location;
            if (target.Parent != null)
            {
                target.Parent.Controls.Add(newCombo);
                target.Parent.Controls.Remove(target);
            }
            target = newCombo;
            return newCombo;
        }
    }
}
