   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WpfApp1.MVVM.ViewModelWpfApp1.MVVM.ViewModel  5   	       WpfApp1.MVVM.View.Profile   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:WpfApp1.MVVM.ViewModel     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  6<     � Title$  Profile��5   	   $	��450��6   $	��380��6&    � AllowsTransparency$
 True��5	   	   
������  � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�6    � DataContext 5      +  %WpfApp1.MVVM.ViewModel.LoginViewModel  5   
   -   5        LoginViewModelInstance��5      .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $��25 �6    �� 5      $��40 �6    �� 5      $	��385 �6    .+�5      �� $��1��6   $��0��6&   ���#252525 K� ZIndex$ 1��5      ._�5      � -   5        $��Stretch=�5      .+�5      �� $��NikTalk��6   
�������$��SemiBold"�5      5      5       �� 5#       �� CornerRadius$ 10��6   $��3��6'   ��5$      �� 5%      .��5&      �  � Color$ #2F3136��6'   	 � Offset$		 0.0Q�67   � 5'      $ #36393F��6'   $	 1Q�67   5(      5*      �� 5-      $��Center=�6   $��1��64   .+�5.      �� $��Профиль��6   $��Light"�5/      $��	helvetica)�50      $��22(�51      
�������$��Center=�53      54      �� 5:      $��2��6   .+�5;      �� .+�5<      �� 
�������$	��100��64   $	��100��6@   $ 50��6M   ._�5=      A� $	��100��6"   $	��100��6.   ��5>      �� 5?   "   
 j� Stretch$
 UniformToFill=�6K    �� ImageSource 6-   �� *Image+6K   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5A      +  %Microsoft.Xaml.Behaviors.EventTrigger  5B   "      	EventName$ MouseLeftButtonDown��61   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5C   &   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6<   �� *ChooseAvatarCommand+5D   #   5F      5G      5J      �� 5K       �� Orientation$ 
Horizontal=�6   .+�5L      �� $��	Helvetica)�6   $��Light"�5M   
   $��20(�5O   
   $��Center=�5P   
   
�������
������ $��0q�5S   
   $	��250��5T   
   $
��Left=�5U   
    J� Opacity$	 0.5Q�5V   
   $��25��5W   
   $��	65,20,0,0q�5X   
   ��5N   
   �� *Email+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors$
 True��.��5X      ��5Y      �� 5Z       �� 
TargetType" ��6$    �� Triggers 5[   "   P� 5\   &    P� Property �@=�HasError P� Value$
 true��6M   . �5]   *   ��  �� Property �@�ToolTip �� Value 6D   ��  �� RelativeSource �� *Self+ �� Path$) #(Validation.Errors)[0].ErrorContent�5^   '   5`      .��5a   )   5b      5d      �� 5e      $	��250��6   $��2��5f      
�������$	 0.5Q�5h      �� 5m      $ 
Horizontal=�6   $��0,20,0,0q�66   .+�5n      �� $��	Helvetica)�6   $��Light"�5o      $��20(�5q      $��Center=�5r      
�������
������ $��0q�5u      $	��250��5v      $
��Left=�5w      $	 0.5Q�5x      $��25��5y      $��65,0,0,0q�5z      ��5p      �� *Name+$ TwoWay=�$ PropertyChanged=�$
 True��.��5z      ��5{      �� 5|      " ��6$    5}   "   P� 5~   &    �@=�HasError$
 true��6M   . �5   *   ��  �@�ToolTip 6D   ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�   '   5�      .��5�   )   5�      5�      �� 5�      $	��250��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      $ 
Horizontal=�6   $��0,20,0,0q�66   .+�5�      �� $��	Helvetica)�6   $��Light"�5�      $��20(�5�      $��Center=�5�      
�������
������ $��0q�5�      $	��250��5�      $
��Left=�5�      $	 0.5Q�5�      $��25��5�      $��65,0,0,0q�5�      ��5�      �� *
Password+$ TwoWay=�$ PropertyChanged=�$
 True��.��5�      ��5�      �� 5�      " ��6$    5�   "   P� 5�   &    �@=�HasError$
 true��6M   . �5�   *   ��  �@�ToolTip 6D   ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�   '   5�      .��5�   )   5�      5�      �� 5�      $	��250��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      $��Center=�62   
���  ����6   �� *Error+6O   �� 5�      $ Vertical=�6   $��0,40,0,0q�64   .+�5�      ��    ButtonStyle#��[  6   $	��100��6C   $��40��6O   $��СОХРАНИТЬ��6[   $��	60,0,60,0q�6o   ��6�   �� *UpdateUserCommand+5�      5�      5�      5�      5�      