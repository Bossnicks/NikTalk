using System.Windows;
using System.Windows.Controls;
using WpfApp1.MVVM.Model;

namespace WpfApp1.TemplateSelector
{
    public class DeleteUpdateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextTemplateUP { get; set; }
        public DataTemplate ImageTemplateUP { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is MessageModel message)
            {
                if (message.TypeOfMessage == "Text" && TextTemplateUP != null)
                {
                    return TextTemplateUP;
                }
                else if ((message.TypeOfMessage == "Image" || message.TypeOfMessage == "Audio") && ImageTemplateUP != null)
                {
                    return ImageTemplateUP;
                }
            }

            // If no suitable template is found, return a default template or null
            return base.SelectTemplate(item, container);
        }
    }
}
