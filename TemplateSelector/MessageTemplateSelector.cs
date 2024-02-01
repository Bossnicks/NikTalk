﻿using System.Windows;
using System.Windows.Controls;
using WpfApp1.MVVM.Model;

namespace WpfApp1.TemplateSelector
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is MessageModel message)
            {
                if (message.TypeOfMessage == "Text" && TextTemplate != null)
                {
                    return TextTemplate;
                }
                else if (message.TypeOfMessage == "Image" && ImageTemplate != null)
                {
                    return ImageTemplate;
                }
            }

            // If no suitable template is found, return a default template or null
            return base.SelectTemplate(item, container);
        }
    }
}