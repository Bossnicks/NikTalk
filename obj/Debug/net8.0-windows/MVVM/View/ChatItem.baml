   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfApp1.ConvertersWpfApp1.Converters  5      C&clr-namespace:WpfApp1.TemplateSelectorWpfApp1.TemplateSelector  5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089/    )WpfApp1.Converters.StringToBytesConverter   StringToBytesConverter8   2WpfApp1.Converters.ByteArrayToImageSourceConverter # ByteArrayToImageSourceConverter  StickerTemplate  �� 	Converter  ImageTemplate  TextTemplate  AudioTemplate �� Orientation �� RelativeSource �� AncestorType � 	Resources �� 
TargetType �� Property �� Value  ImageTemplateUP  ButtonStyle �� BitmapScalingMode  TextTemplateUP 	 ReadTemplate 
 UnReadTemplate  NonReadTemplate6   0WpfApp1.TemplateSelector.MessageTemplateSelector  MessageTemplateSelector	   ImageTemplate
   TextTemplate   AudioTemplate   StickerTemplate;   5WpfApp1.TemplateSelector.DeleteUpdateTemplateSelector   DeleteUpdateTemplateSelector   ImageTemplateUP   TextTemplateUP3  -WpfApp1.TemplateSelector.ReadTemplateSelector  ReadTemplateSelector   ReadTemplate   UnReadTemplate   NonReadTemplate  ChatItem �� 
TargetType �� ImageSource  ChatNameColor  ChatDateColor�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      <local2http://schemas.microsoft.com/expression/blend/2008  5      )conv clr-namespace:WpfApp1.Converters  5      /temp&clr-namespace:WpfApp1.TemplateSelector  5      %b  &	        &	      &	 &     7  &	 �     7  &	     7   &	 �    &	 �    7  &	 (    7  7  &		 �    &	
 		    &	 z	    &	 �	    7  7  7  7  &	 �	    7  7  &	 �	    7 	 7 
 7  &	 
    7  7  7     5      6"     5      6+   �� 5	      .R�5
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
������ $��0q�5      $��40��5      $��40��5      $��10q�5      ��5      �� *(&DataContext.PlayOrDownloadAudioCommand+ �� " ����6�   ��  5      ���� 5      '��       " ��6   .�5       ��  �@��CornerRadius$ 20��69   5!      5"      .��5#      �� ��5$      �� 5%      $��/Icons/play.png��6    5'      5(      �� 5)      $ Vertical=�6   .+�5*      �� $
��auto��6   $��30��6%   $��15(�61   $��0,5,0,0q�6?   $
��Left=�6P   $��Center=�6k   $��Audio��6�   �� 5+      $
��auto��6   $��30��6%   $��15(�61   $��	0,-10,0,0q�6?   $
��Left=�6R   $��Center=�6m   ��6�   �� *SentAt+5,      5-      5.      �� 5/      .R�50   
   �� 8��  $��Center=�6�   $��Center=�6�   $��27��6�   $��27��6  
������ $��0q�6)  ��66   �� *DataContext.DeleteCommand+ �� " ����6�   �� .��51      �� $��16��6   $��16��6   $��/Icons/cancel.png��6+   $
 Fant=�6F   52      53      �� 54      .R�55   
   �� $ 
Horizontal=�6   .+�56      �� 8��  $��Center=�6�   $��Center=�6�   $��27��6�   $��27��6  
������ $��0q�6+  ��6:   �� *DataContext.EditCommand+ �� " ����6�   �� .��57      �� $��24��6   $��24��6#   $��/Icons/edit.png��6/   $
 Fant=�6H   58      �� 59      8�� $��Center=�6�   $��Center=�6�   $��27��6�   $��27��6  
������ $��0q�6-  ��6:   �� *DataContext.DeleteCommand+ �� " ����6�   �� .��5:      �� $��16��6   $��16��6#   $��/Icons/cancel.png��6/   $
 Fant=�6J   5;      5<      5=      �� 5>      .R�5?   
   �� $��/Icons/read.png��6   $��24��6)   $��24��64   $
 Fant=�6@   5@      �� 5A      .R�5B   
   �� $��/Icons/unread.png��6   $��24��6+   $��24��66   $
 Fant=�6B   5C      �� 5D      5E        5G      8	   8
  8  8  5K   (     5L      8   8  5N   (     5O      8   8  8  5R   (   �� 5W      " ��6   .�5X   
   ��  � �� 5Y      �� 5Z      " ��6"   .R�5[      �� $ 
Horizontal=�6!   $��0,10,0,0q�6:   .+�5\      A� $��40��6"   $��40��6-   $��10,7,0,0q�69   $	��Top=�6K   ��5]      �� 5^   "   $
 Fant=�6K    6-   �� *Image+6K   5`      �� 5a      $ Vertical=�6%   .+�5b      �� $ 
Horizontal=�6)   .+�5d   "   �� #���  5e   (   $��SemiBold"�5f   (   $��14(�5g   (   $��Center=�5h   (   $��5,0,0,0q�5i   (   $��0,0,0,0q�5j   (   ��5d   (   �� *
UserName+5j   (   �� 5l   "   #���  5m   (   $��SemiBold"�5n   (   $��11(�5o   (   $��0,1,0,0q�5p   (   $��Center=�5q   (   ��5l   (   �� *SentAt+5q   (   �� 5r   "   8��  ��61   �� 5s   -   �� 5t   "   8�� ��61   �� 5u   -   5v      �� 5w      8�� ��6-   �� 5x   -   5y      5z      5{      5}      5~      5      