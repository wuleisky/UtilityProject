   M S B A M L   `   `   `  ����  UtilityResolution"    System.Windows.InkMessageBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  inkHintLayer��5   	    � WindowStyle$
 None=�5   	   $��Microsoft YaHei)�5   	    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . |�.   � WindowStartupLocation$ CenterScreen=�5   	    � SizeToContent$ Width=�5   	   $��60��5   	   ��5      Z�  � Triggers 5      :� 5   
    :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� -   5        	 g�Name$	 	delayShow��5      .��5      W� 
 �� 
TargetName$
 inkHintLayer��6&    �� TargetProperty$ Opacity�5   &    W� From$	 0.3X�5   &   	 W� To$	 0.8X�61    g� Duration$ 0:0:0.2D�6:   5      5      5      .��5      � .+�5   
   �� -   5         ��Name$ 
borderMain��5   (   ���	#F03A3A3A �� CornerRadius$ 20��6<   ._�5      �� -   5         ��Name$ lblMsg��5   P   $��Center=�6   $��Center=�65   
�������$��	20,0,20,0q�6s   $��16(�6�   $
��Wrap=�6�   $
��Text��6�   5      5      5       