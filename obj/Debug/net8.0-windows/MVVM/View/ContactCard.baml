   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfApp1.ConvertersWpfApp1.Converters  5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089*    $WpfApp1.Converters.DateTimeConverter   DateTimeConverter  �� 
TargetType  ContactCard �� Triggers P� Property P� Value �� Property  UpdateColor �� Value  ListViewContactBack �� 
TargetType �� CornerRadius �� Orientation	 �� ImageSource  	TextColor  
LabelColor
 �� 	Converter  SelectedContactMessage�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      )conv clr-namespace:WpfApp1.Converters  5      %N  &	        &	      7      5      6   �� 5      "  ��6    5   
   P� 5	       �@��
IsSelected$
 True��6,   . �5
      ��  � ��# �  6/   5      .�5   
   ��  � ��# �  6'   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� #��z��6   $��50��5      $ 4��5      $��8,2,8,2q�5      ._�5      �� $ 
Horizontal=�6%   $��10,0,0,0q�5   %   $	��180��5   %   .+�5      A� $��30��6&   $��30��61   ��5   "   �� 5   &   	 61   �� *Image+5      �� 5!      $ Vertical=�6)   $��5,0,0,0q�6@   .+�5"   "   �� $ 
Horizontal=�6-   $	��130��6F   $��0,0,0,0q�6R   .+�5#   &   �� #���  5$   -   $��45��5%   -   $��45��5&   -   ��5#   ,   �� *
UserName+5&   -   �� 5(   &   #���  5)   -   $��10(�5*   -   $	��100��5+   -   ��5(   ,   �� *LastMessage.SentAt+8
   5+   -   5,   #   �� 5.   "   #���  5/   )   $��10(�50   )   $	��130��51   )   $��0,0,0,0q�52   )   ��5.   (   �� *LastMessageView+52   )   54      56      57      58      5:      5;      5=      