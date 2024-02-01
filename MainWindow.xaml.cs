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
//using WpfApp1.MVVM.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MainModel mainModel = new MainModel();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
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




        //private async void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        //{
        //    // Проверяем, если прокрутка достигла нижней границы
        //    if (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight)
        //    {
        //        if (scrollViewer.Content is ListView listView && listView.Items.Count > 0)
        //        {
        //            await listView.Dispatcher.InvokeAsync(() =>
        //            {
        //                // Обновляем макет для всех элементов в ListView
        //                listView.UpdateLayout();

        //                foreach (var item in listView.Items)
        //                {
        //                    if (item is MessageModel message && message.AssociatedUIElement != null)
        //                    {
        //                        // Проверяем, если нижняя граница текущего сообщения касается нижней границы Grid
        //                        GeneralTransform transform = message.AssociatedUIElement.TransformToAncestor(grid);
        //                        Rect bounds = transform.TransformBounds(new Rect(0, 0, message.AssociatedUIElement.RenderSize.Width, message.AssociatedUIElement.RenderSize.Height));

        //                        double bottomOfBounds = bounds.Bottom;

        //                        if (bottomOfBounds >= 0 && bottomOfBounds <= grid.ActualHeight)
        //                        {
        //                            MessageBox.Show($"Сообщение с Id: {message.MessageId} касается нижней границы Grid");
        //                        }
        //                    }
        //                }
        //            }, System.Windows.Threading.DispatcherPriority.Background);
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







        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;

            // Calculate the new vertical offset
            double newOffset = scrollViewer.VerticalOffset - (e.Delta > 0 ? 1 : -1) * scrollViewer.ScrollableHeight / 20;
            scrollViewer.ScrollToVerticalOffset(newOffset);
            e.Handled = true;
        }




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

    }
}











