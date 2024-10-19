using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.MVVM.Model;
using WpfApp1.MVVM.ViewModel;

namespace WpfApp1.TemplateSelector
{
    internal class ReadTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ReadTemplate { get; set; }
        public DataTemplate UnReadTemplate { get; set; }
        public DataTemplate NonReadTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is MessageModel message)
            {
                if (message.IsRead == true && MainViewModel.authenticatedUser.UserId == message.SenderId && ReadTemplate != null)
                {
                    return ReadTemplate;
                }
                else if (message.IsRead == false && MainViewModel.authenticatedUser.UserId == message.SenderId && UnReadTemplate != null)
                {
                    return UnReadTemplate;
                }

                else if (MainViewModel.authenticatedUser.UserId == message.ReceiverId && NonReadTemplate != null)
                {
                    return NonReadTemplate;
                }
            }

            // If no suitable template is found, return a default template or null
            return base.SelectTemplate(item, container);
        }



    }
}