   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WpfApp1.MVVM.ViewModelWpfApp1.MVVM.ViewModel  5   	   $    WpfApp1.MVVM.View.RegisterView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:WpfApp1.MVVM.ViewModel     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  6<     � Title$  RegisterView��5   	   $	��450��6   $	��380��6+    � AllowsTransparency$
 True��5	   	   
������  � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      +  %WpfApp1.MVVM.ViewModel.LoginViewModel  5   
   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $��25 �6    �� 5      $��40 �6    �� 5      $	��385 �6    .+�5      �� $��1��6   $��0��6&   ���#252525 K� ZIndex$ 1��5      ._�5      � $��Stretch=�6   .+�5      �� $��NikTalk��6   
�������$��SemiBold"�5      �� 5      $��Right=�6!    �� Orientation$ 
Horizontal=�5      .+�5       �� $��20��6!   $��20��6,   $
��🗕��5!      
������ $��0q�5#      
�������$
��Bold"�5%      $��0,0,0,3q�5&      ��5'      �� *MinimizeCommand+�� 5(      $��20��6!   $��20��6,   $	��⬜��5)      
������ $��0q�5+      
�������$
��Bold"�5-      ��5.      �� *MaximizeRestoreCommand+�� 5/      $��20��6!   $��20��6,   $	��✕��50      
������ $��0q�52      
�������$
��Bold"�54      ��55      �� *CloseCommand+56      57      58      �� 5;      	 �� CornerRadius$	 10��6   $��3��6'   ��5<      �� 5=      .��5>      � 
 � Color$
 #2F3136��6'    � Offset$	 0.0Q�67   � 5?      $
 #36393F��6'   $ 1Q�67   5@      5B      �� 5E      $��Center=�6   $��1��64   .+�5F      �� $��Регистрация��6   $��Light"�5G      $��	helvetica)�5H      $��22(�5I      
�������$��Center=�5K      5L      �� 5R      $��2��6   .+�5S      �� .+�5T      �� 
�������$	��100��64   $	��100��6@   $	 50��6M   ._�5U      A� $	��100��6"   $	��100��6.   ��5V      �� 5W   "    j� Stretch$ UniformToFill=�6U    �� ImageSource 6-   �� *AvatarImagePath+6U   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5Y      +  %Microsoft.Xaml.Behaviors.EventTrigger  5Z   "      	EventName$ MouseLeftButtonDown��61   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5[   &   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6<   �� *ChooseAvatarCommand+5\   #   5^      5_      5b      �� 5c      $ 
Horizontal=�6   .+�5d      �� $��	Helvetica)�6   $��Light"�5e   
   $��20(�5g   
   $��Center=�5h   
   
�������
������ $��0q�5k   
   $	��250��5l   
   $
��Left=�5m   
    J� Opacity$	 0.5Q�5n   
   $��25��5o   
   $��	65,20,0,0q�5p   
   ��5f   
   �� *Email+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors$
 True��.��5p      ��5q      �� 5r       �� 
TargetType" ��6$    �� Triggers 5s   "   P� 5t   &    P� Property �@=�HasError P� Value$
 true��6M   . �5u   *   ��  �� Property �@�ToolTip �� Value 6D   ��  �� RelativeSource �� *Self+ �� Path$) #(Validation.Errors)[0].ErrorContent�5v   '   5x      .��5y   )   5z      5|      �� 5}      $	��250��6   $��2��5~      
�������$	 0.5Q�5�      �� 5�      $ 
Horizontal=�6   $��0,20,0,0q�66   .+�5�      �� $��	Helvetica)�6   $��Light"�5�      $��20(�5�      $��Center=�5�      
�������
������ $��0q�5�      $	��250��5�      $
��Left=�5�      $	 0.5Q�5�      $��25��5�      $��65,0,0,0q�5�      ��5�      �� *Name+$ TwoWay=�$ PropertyChanged=�$
 True��.��5�      ��5�      �� 5�      " ��6$    5�   "   P� 5�   &    �@=�HasError$
 true��6M   . �5�   *   ��  �@�ToolTip 6D   ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�   '   5�      .��5�   )   5�      5�      �� 5�      $	��250��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      $ 
Horizontal=�6   $��0,20,0,0q�66   .+�5�      �� $��	Helvetica)�6   $��Light"�5�      $��20(�5�      $��Center=�5�      
�������
������ $��0q�5�      $	��250��5�      $
��Left=�5�      $	 0.5Q�5�      $��25��5�      $��65,0,0,0q�5�      ��5�      �� *
Password+$ TwoWay=�$ PropertyChanged=�$
 True��.��5�      ��5�      �� 5�      " ��6$    5�   "   P� 5�   &    �@=�HasError$
 true��6M   . �5�   *   ��  �@�ToolTip 6D   ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�   '   5�      .��5�   )   5�      5�      �� 5�      $	��250��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      $��Center=�62   
���  ����6   �� *Error+6O   �� 5�      $ Vertical=�6   $��0,40,0,0q�64   .+�5�      ��    ButtonStyle#��[  6   $	��100��6C   $��40��6O   $��РЕГИСТРАЦИЯ��6[   $��	60,0,60,0q�6q   ��6�   �� *RegisterCommand+�� 5�      
������ $��0,15,0,5q�6G   $	��190��6Y   $��0q�6e   ��6   Z� ��6y   �� *ShowLoginViewCommand+.��5�      �� $-��'Есть аккаунт? Войдите��6    5�      5�      5�      5�      5�      5�      