   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfApp1.ConvertersWpfApp1.Converters  5      C&clr-namespace:WpfApp1.TemplateSelectorWpfApp1.TemplateSelector  5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089/    )WpfApp1.Converters.StringToBytesConverter   StringToBytesConverter8   2WpfApp1.Converters.ByteArrayToImageSourceConverter # ByteArrayToImageSourceConverter  StickerTemplate  �� 	Converter  ImageTemplate  TextTemplate  AudioTemplate �� Orientation �� RelativeSource �� AncestorType � 	Resources �� 
TargetType �� Property �� Value  ImageTemplateUP  ButtonStyle �� BitmapScalingMode  TextTemplateUP6   0WpfApp1.TemplateSelector.MessageTemplateSelector 	 MessageTemplateSelector	   ImageTemplate
   TextTemplate   AudioTemplate   StickerTemplate;   5WpfApp1.TemplateSelector.DeleteUpdateTemplateSelector  
 DeleteUpdateTemplateSelector   ImageTemplateUP   TextTemplateUP  ChatItem �� 
TargetType �� ImageSource�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      <local2http://schemas.microsoft.com/expression/blend/2008  5      )conv clr-namespace:WpfApp1.Converters  5      /temp&clr-namespace:WpfApp1.TemplateSelector  5      %{  &	        &	      &	 &     7  &	 �     7  &	     7   &	 �    &	 �    7  &	 ,    7  7  &		 �    7  7  7  7  &	
 �    7  7  &	 �    7 
 7 	    5      6"     5      6+   �� 5	      .R�5
   
   �� $
��auto��6   $	��100��6   $��	10,20,0,0q�6,   ��6?   �� *	Message+8    5      �� 5      .R�5   
   �� $
��auto��6   $	��300��6   $��10,0,0,0q�6,   ��6>   �� *	Message+8    5      �� 5      .R�5   
   �� $
��auto��6   $	��200��6   $��35��6,   $��0,-5,0,0q�68   $��15(�6J   $
��Left=�6X   ��6s   �� *	Message+8    5      �� 5      .R�5   
   �� $ 
Horizontal=�6   .+�5      �� $��Century Gothic)�5      
������ 
������ $��0q�5      $��40��5      $��40��5      $��10q�5      ��5      �� *(&DataContext.PlayOrDownloadAudioCommand+ �� " ����6�   ��  5      ���� 5       '��       " ��6   .�5!      ��  �@��CornerRadius$ 20��69   5"      5#      .��5%      �� ��5&      �� 5'      $��../Icons/play.png��6    5)      5*      �� 5-      $ Vertical=�6   .+�5.      �� $
��auto��6   $��30��6%   $��15(�61   $��0,5,0,0q�6?   $
��Left=�6P   $��Center=�6k   $��Audio��6�   �� 5/      $
��auto��6   $��30��6%   $��15(�61   $��	0,-10,0,0q�6?   $
��Left=�6R   $��Center=�6m   ��6�   �� *SentAt+50      51      52      �� 53      .R�54   
   �� 8��  $��Center=�6�   $��Center=�6�   $��27��6�   $��27��6  
������ $��0q�6)  ��66   �� *DataContext.DeleteCommand+ �� " ����6�   �� .��55      �� $��16��6   $��16��6   $��../Icons/cancel.png��6+   $
 Fant=�6H   56      57      �� 58      .R�59   
   �� $ 
Horizontal=�6   .+�5:      �� 8��  $��Center=�6�   $��Center=�6�   $��27��6�   $��27��6  
������ $��0q�6+  ��6:   �� *DataContext.EditCommand+ �� " ����6�   �� .��5;      �� $��24��6   $��24��6#   $��../Icons/edit.png��6/   $
 Fant=�6J   5<      �� 5=      8�� $��Center=�6�   $��Center=�6�   $��27��6�   $��27��6  
������ $��0q�6-  ��6:   �� *DataContext.DeleteCommand+ �� " ����6�   �� .��5>      �� $��16��6   $��16��6#   $��../Icons/cancel.png��6/   $
 Fant=�6L   5?      5@      5A        5C      8	   8
  8  8  5G   (     5H      8   8  5J   (   �� 5O      " ��6   .�5P   
   ��  � �� 5Q      �� 5R      " ��6"   .R�5S      �� $ 
Horizontal=�6!   $��0,10,0,0q�6:   .+�5T      A� $��40��6"   $��40��6-   $��10,7,0,0q�69   $	��Top=�6K   ��5U      �� 5V   "   $
 Fant=�6K    6-   �� *Image+6K   5X      �� 5Y      $ Vertical=�6%   .+�5Z      �� $ 
Horizontal=�6)   .+�5\   "   �� 
���  ��$��SemiBold"�5^   (   $��14(�5_   (   $��Center=�5`   (   $��5,0,0,0q�5a   (   $��0,0,0,0q�5b   (   ��5\   (   �� *
UserName+5b   (   �� 5d   "   ���#AAAAAA$��SemiBold"�5f   (   $��11(�5g   (   $��0,1,0,0q�5h   (   $��Center=�5i   (   ��5d   (   �� *SentAt+5i   (   �� 5j   "   
��� � �$��SemiBold"�5l   (   $��11(�5m   (   $��0,1,0,0q�5n   (   $��Center=�5o   (   ��5j   (   �� *IsRead+5o   (   �� 5p   "   8��  ��61   �� 5q   -   5r      �� 5s      8�� ��6-   �� 5t   -   5u      5y      5z      5|      5}      5~      