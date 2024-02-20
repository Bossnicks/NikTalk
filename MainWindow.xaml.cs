using WpfApp1.MVVM.Model;
using WpfApp1.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Media3D;
using WpfApp1.MVVM.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceDictionary darkTheme;
        private ResourceDictionary lightTheme;
        public MainWindow()
        {
            InitializeComponent();
            //MainViewModel mainViewModel = new MainViewModel();
            darkTheme = new ResourceDictionary { Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative) };
            lightTheme = new ResourceDictionary { Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative) };
            Resources.MergedDictionaries.Add(darkTheme);


        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Button_Click_Theme(object sender, RoutedEventArgs e)
        {
            // Загрузите словари ресурсов


            // Проверьте, какая тема сейчас активна, и переключите на другую
            if (Resources.MergedDictionaries.Contains(darkTheme))
            {
                Resources.MergedDictionaries.Remove(darkTheme);
                Resources.MergedDictionaries.Add(lightTheme);
            }
            else
            {
                Resources.MergedDictionaries.Remove(lightTheme);
                Resources.MergedDictionaries.Add(darkTheme);
            }
        }




        //private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        //{
        //    WindowState = WindowState.Minimized;
        //}

        //private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if(WindowState == WindowState.Normal)
        //    {
        //        WindowState = WindowState.Maximized;
        //    }
        //    else
        //    {
        //        WindowState = WindowState.Normal;
        //    }
        //}

        //private void CloseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}
        //private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        //{
        //    if (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight)
        //    {
        //        if (scrollViewer.Content is ListView listView && listView.Items.Count > 0)
        //        {
        //            var lastMessage = listView.Items[listView.Items.Count - 1] as MessageModel;
        //            if (lastMessage != null)
        //            {
        //                MessageBox.Show($"Самое нижнее сообщение с Id: {lastMessage.MessageId}");
        //            }
        //        }
        //    }
        //}


        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;

            // Calculate the new vertical offset
            double newOffset = scrollViewer.VerticalOffset - (e.Delta > 0 ? 1 : -1) * scrollViewer.ScrollableHeight / 400;
            scrollViewer.ScrollToVerticalOffset(newOffset);
            e.Handled = true;
        }

        private bool isScrollingHandled = false;
        private DispatcherTimer scrollTimer;

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (scrollViewer.Content is ListView listView && listView.Items.Count > 0)
            {
                if (scrollTimer != null)
                {
                    scrollTimer.Stop();
                }

                scrollTimer = new DispatcherTimer();
                scrollTimer.Interval = TimeSpan.FromSeconds(2);

                scrollTimer.Tick += (s, args) =>
                {
                    double maxBottom = 0;
                    MessageModel lastVisibleMessage = null;

                    // Перебираем все элементы в ListView
                    for (int i = 0; i < listView.Items.Count; i++)
                    {
                        // Получаем контейнер для текущего элемента
                        var itemContainer = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                        if (itemContainer != null)
                        {
                            // Получаем координаты нижней границы текущего элемента относительно ScrollViewer
                            GeneralTransform transform = itemContainer.TransformToVisual(scrollViewer);
                            Point bottomPoint = transform.Transform(new Point(0, itemContainer.ActualHeight));

                            // Проверяем, если текущий элемент касается нижней границы ListView
                            if (bottomPoint.Y >= 0 && bottomPoint.Y <= scrollViewer.ActualHeight)
                            {
                                if (bottomPoint.Y > maxBottom)
                                {
                                    maxBottom = bottomPoint.Y;
                                    lastVisibleMessage = listView.Items[i] as MessageModel;
                                }
                            }
                        }
                    }

                    if (lastVisibleMessage != null)
                    {
                        AppDbContextFactory factory = new AppDbContextFactory();
                        AppDbContext context = factory.CreateDbContext(null);
                        // Выводим MessageBox
                        var lastVisibleMessageSentAt = context.dbMessages
                                .Where(m => m.MessageId == lastVisibleMessage.MessageId)
                                .Select(m => m.SentAt)
                                .FirstOrDefault();
                        if (lastVisibleMessageSentAt != null)
                        {
                            // Находим все сообщения, отправленные раньше времени последнего видимого сообщения
                            var messagesToUpdate = context.dbMessages
                                .Where(m => m.SentAt < lastVisibleMessageSentAt && m.ReceiverId == MainViewModel.authenticatedUser.UserId)
                                .ToList();

                            // Обновляем статус isRead для найденных сообщений
                            foreach (var message in messagesToUpdate)
                            {
                                message.IsRead = true;
                            }

                            // Сохраняем изменения в базе данных
                            context.SaveChanges();
                        }





                    }

                    // Сбрасываем флаг в false
                    isScrollingHandled = false;

                    // Останавливаем таймер
                    scrollTimer.Stop();
                };

                // Запускаем таймер
                scrollTimer.Start();
            }
        }


    }
}










//private ListViewItem GetListViewItem(DependencyObject target)
//{
//    while (target != null && !(target is ListViewItem) && target != scrollViewer)
//    {
//        target = VisualTreeHelper.GetParent(target);
//    }

//    return target as ListViewItem;
//}
//private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
//{
//    if (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight)
//    {
//        if (scrollViewer.Content is ListView listView && listView.Items.Count > 0)
//        {
//            var lastMessage = listView.Items[listView.Items.Count - 1] as MessageModel;
//            if (lastMessage != null)
//            {
//                MessageBox.Show($"Самое нижнее сообщение с Id: {lastMessage.MessageId}");
//            }
//        }
//    }
//}





//private ListViewItem GetListViewItem(DependencyObject target)
//{
//    while (target != null && !(target is ListViewItem) && target != scrollViewer)
//    {
//        target = VisualTreeHelper.GetParent(target);
//    }

//    return target as ListViewItem;
//}

//private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
//{
//    // Проверяем, если прокрутка достигла нижней границы
//    if (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight)
//    {
//        if (scrollViewer.Content is ListView listView && listView.Items.Count > 0)
//        {
//            var lastMessage = listView.Items[listView.Items.Count - 1] as MessageModel;
//            if (lastMessage != null)
//            {
//                // Проверяем, если нижняя граница последнего сообщения касается нижней границы Grid
//                GeneralTransform transform = lastMessage.UIElement.TransformToAncestor(grid);
//                Rect bounds = transform.TransformBounds(new Rect(0, 0, lastMessage.UIElement.RenderSize.Width, lastMessage.UIElement.RenderSize.Height));

//                double bottomOfBounds = bounds.Bottom;

//                if (bottomOfBounds >= 0 && bottomOfBounds <= grid.ActualHeight)
//                {
//                    MessageBox.Show($"Самое нижнее сообщение с Id: {lastMessage.MessageId}");
//                }
//            }
//        }
//    }
//}







//private void OnMouseWheel(object sender, MouseWheelEventArgs e)
//{
//    ScrollViewer scrollViewer = sender as ScrollViewer;

//    // Calculate the new vertical offset
//    double newOffset = scrollViewer.VerticalOffset - (e.Delta > 0 ? 1 : -1) * scrollViewer.ScrollableHeight / 20;
//    scrollViewer.ScrollToVerticalOffset(newOffset);
//    e.Handled = true;
//}




//private bool isScrollingHandled = false;
//private DispatcherTimer scrollTimer;

//private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
//{
//    if (scrollViewer.Content is ListView listView && listView.Items.Count > 0)
//    {
//        if (scrollTimer != null)
//        {
//            scrollTimer.Stop();
//        }

//        scrollTimer = new DispatcherTimer();
//        scrollTimer.Interval = TimeSpan.FromSeconds(2);

//        scrollTimer.Tick += (s, args) =>
//        {
//            double maxBottom = 0;
//            MessageModel lastVisibleMessage = null;

//            // Перебираем все элементы в ListView
//            for (int i = 0; i < listView.Items.Count; i++)
//            {
//                // Получаем контейнер для текущего элемента
//                var itemContainer = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

//                if (itemContainer != null)
//                {
//                    // Получаем координаты нижней границы текущего элемента относительно ScrollViewer
//                    GeneralTransform transform = itemContainer.TransformToVisual(scrollViewer);
//                    Point bottomPoint = transform.Transform(new Point(0, itemContainer.ActualHeight));

//                    // Проверяем, если текущий элемент касается нижней границы ListView
//                    if (bottomPoint.Y >= 0 && bottomPoint.Y <= scrollViewer.ActualHeight)
//                    {
//                        if (bottomPoint.Y > maxBottom)
//                        {
//                            maxBottom = bottomPoint.Y;
//                            lastVisibleMessage = listView.Items[i] as MessageModel;
//                        }
//                    }
//                }
//            }

//            if (lastVisibleMessage != null)
//            {
//                // Выводим MessageBox
//                MessageBox.Show($"Сообщение с Id: {lastVisibleMessage.MessageId}\nТекст: {lastVisibleMessage.MessageText}\nВремя: {lastVisibleMessage.SentAt}");
//            }

//            // Сбрасываем флаг в false
//            isScrollingHandled = false;

//            // Останавливаем таймер
//            scrollTimer.Stop();
//        };

//        // Запускаем таймер
//        scrollTimer.Start();
//    }
//}











