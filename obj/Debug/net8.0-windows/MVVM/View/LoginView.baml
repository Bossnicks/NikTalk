   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfApp1.MVVM.ViewWpfApp1.MVVM.View  5      ?$clr-namespace:WpfApp1.MVVM.ViewModelWpfApp1.MVVM.ViewModel  65   !    WpfApp1.MVVM.View.LoginView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:WpfApp1.MVVM.View     5      :	viewmodel$clr-namespace:WpfApp1.MVVM.ViewModel     65     � Title$  	LoginView��5	       � 	Resources 5
      +  %WpfApp1.MVVM.ViewModel.LoginViewModel   log��%   &	          5   
   6#   5      .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $��25 �6    �� 5      $	��120 �6    �� 5      $	��305 �6    .+�5      �� $��1��6   $��0��6&   ���#252525 K� ZIndex$ 1��5      ._�5      � $��Stretch=�6   .+�5      �� $��NikTalk��6   
�������$��SemiBold"�5      �� 5      $��Right=�6!    �� Orientation$ 
Horizontal=�5      .+�5      �� $��20��6!   $��20��6,   $
��🗕��5       
������ $��0q�5"      
�������$
��Bold"�5$      $��0,0,0,3q�5%      ��5&      �� *MinimizeCommand+�� 5'      $��20��6!   $��20��6,   $	��⬜��5(      
������ $��0q�5*      
�������$
��Bold"�5,      ��5-      �� *MaximizeRestoreCommand+�� 5.      $��20��6!   $��20��6,   $	��✕��5/      
������ $��0q�51      
�������$
��Bold"�53      ��54      �� *CloseCommand+55      56      57      �� 5:       �� CornerRadius$ 10��6   $��3��6'   ��5;      �� 5<      .��5=      �  � Color$ #2F3136��6'    � Offset$	 0.0Q�67   � 5>      $ #36393F��6'   $ 1Q�67   5?      5A      �� 5D      $��Center=�6   $��1��64   .+�5E      �� $��Авторизация��6   $��Light"�5F      $��	helvetica)�5G      $��22(�5H      
�������$��Center=�5J      5K      �� 5O      $��2��6   .+�5P      �� $ 
Horizontal=�6   .+�5Q      �� $��	Helvetica)�6   $��Light"�5R   
   $��20(�5T   
   $��Center=�5U   
   
�������
������ $��0q�5X   
   $	��250��5Y   
   $
��Left=�5Z   
    J� Opacity$	 0.5Q�5[   
   $��25��5\   
   $��65,0,0,0q�5]   
   ��5S   
   �� *Email+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=� �� ValidatesOnDataErrors$
 True��.��5]      ��5^      �� 5_       �� 
TargetType" ��6$    �� Triggers 5`   "   P� 5a   &    P� Property �@=�HasError P� Value$
 true��6M   . �5b   *   ��  �� Property �@�ToolTip �� Value 6D   ��  �� RelativeSource �� *Self+ �� Path$) #(Validation.Errors)[0].ErrorContent�5c   '   5e      .��5f   )   5g      5i      �� 5j      $	��250��6   $��2��5k      
�������$	 0.5Q�5m      �� 5q      $ 
Horizontal=�6   $��0,20,0,0q�66   .+�5r      �� $��	Helvetica)�6   $��Light"�5s      $��20(�5u      $��Center=�5v      
�������
������ $��0q�5y      $	��250��5z      $
��Left=�5{      $	 0.5Q�5|      $��25��5}      $��65,0,0,0q�5~      ��5t      �� *
Password+$	 TwoWay=�$
 PropertyChanged=�$
 True��.��5~      ��5      �� 5�      " ��6$    5�   "   P� 5�   &    �@=�HasError$
 true��6M   . �5�   *   ��  �@�ToolTip 6D   ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�   '   5�      .��5�   )   5�      5�      �� 5�      $	��250��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      ��6   �� *Error+�� 5�      $ Vertical=�6   $��0,60,0,0q�64   .+�5�      ��   ButtonStyle#��[ 6   $	��100��6C   $��40��6O   $��ВХОД��6[   $��	60,0,60,0q�6j   ��6}   �� *LoginCommand+�� 5�      
������ $��0,15,0,5q�6G   $	��190��6Y   $��0q�6e   ��6   Z� ��6y   �� *ShowRegisterViewCommand+.��5�      �� $A��;Нет аккаунта? Зарегистрируйтесь��6    5�      5�      5�      5�      5�      5�      