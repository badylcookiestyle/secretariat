   M S B A M L   `   `   `  ����D  @secretary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Secretary	Secretary  5      ;"clr-namespace:secretary.viewModelssecretary.viewModels  5      1clr-namespace:secretary.viewssecretary.views  5          Secretary.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      )localclr-namespace:Secretary     5      9
viewmodels"clr-namespace:secretary.viewModels     5      /viewsclr-namespace:secretary.views     5      -   5          � Title$  
MainWindow��5
   	   $	��800��6   $
��1850��6)    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ CenterScreen=�6L    � WindowStyle$
 None=�6q    � 	Resources 5         FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background	 �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 SidebarBtnStyle
 �� 
TargetType �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  RoundMe �� DataType(  "secretary.viewModels.DataViewModel   secretary.views.DataView � DataContext(  "secretary.viewModels.MainViewModel   secretary.views.MainView+  %secretary.viewModels.OptionsViewModel!   secretary.views.OptionsView��%I	  &	        &	 �     &	 �     &	 �     &	     &	 ?    &	 b    &	 �    &	 �    &		 �    &	
 �    7   7  7  7  7  7 	 &	     (�� l    5F   1   *, +)(�� �    5I   1   *, +)(��     5L   4   *, +)�� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   $
 true��6E   $��1��6`   #��� ��6t   5      5      5      �� 5   
   $	 	#FFDDDDDD��6;   �� 5   
   $	 	#FF707070��67   �� 5   
   $	 	#FFBEE6FD��6>   �� 5   
   $	 	#FF3C7FB1��6:   �� 5   
   $	 	#FFC4E5F6��6<   �� 5   
   $	 	#FF2C628B��68   �� 5   
   $	 	#FFF4F4F4��6=   �� 5   
   $	 	#FFADB2B5��69   �� 5   
   $	 	#FF838383��6=   �� 5   
   "
 ��6(   .�5      ��  �@�FocusVisualStyle8   61   �� 5        � ��8  6+   �� 5!       � ��8  6,   �� 5"       � ��# � ��6+   �� 5#       � ��$ 1q�60   �� 5$       � ��$ Center=�6;   �� 5%       � ��$ Center=�69   �� 5&       � ��$ 1q�6(   �� 5'       � �� 5(      �� 5)      " ��6&   .R�5*      �� $ border��6!   #��z��61   #��z��6[   #��z��6�   $
 true��6�   ._�5+      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5,       5-      P� 5.       �@��IsDefaulted$
 true��6=   . �5/   "   �� $ border��6@   # � ��6T    � ��50      P� 51       �@J�IsMouseOver$
 true��6=   . �52   "   �� $ border��6?    � �� �#7B6CF66S   �� 53   "   $ border��6@    � �� �#7B6CF66T   54      P� 55       � ��$
 true��6;   . �56   "   �� $ border��6?    � �� �#7B6CF66S   �� 57   "   $ border��6@    � �� �#7B6CF66T   58      P� 59       � {�$ false��6;   . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   $ contentPresenter��6K   8   � ��6i   5=      5?      5A      5B      �� 5C   
   "
 ��6   .�5D      ��  �@��CornerRadius$ 4��6-   5E      �� 5F   
   DataViewTemplate��"  61   .R�5G         6   �� 5H      �� 5I   
   MainViewTemplate��"  61   .R�5J         6   �� 5K      �� 5L   
   OptionsViewTemplate��"  64   .R�5M         6    �� 5N      5O      .��5P      � ���#393E46.+�5Q   
   � -   5         �Name$ mainGrid��5Q      ���#222831.+�5R      �� ��6   �� 65   �� 5S      $��90��6   $
��Left=�6$   ���#574B90.+�5T      �� -   5         ��Name$ MainViewBtn��5T   ^   #��� 
 64   $��90��6s   
������ 
������ $��0,30,0,0q�6�   
�������.��5U      �� $��/assets/images/main.png��6   $��15,15,15,15q�6=   5V      �� 5W      -   5        $ DataViewBtn��5W   �   #��� 
 6r   $��90��6�   
������ 
������ .��5X      �� $��/assets/images/upload.png��6   $��15,15,15,15q�6?   5Y      �� 5Z      -   5        $ OptionsViewBtn��5Z   �   #��� 
 6s   $��90��6�   
������ 
������ .��5[      �� $#��/assets/images/preference.png��6   $��15,15,15,15q�6C   6\   5\      5]      � 5^      -   5        $��25��5^      $	��Top=�6!   ���#0e171e.+�5_      ]� $��Right=�6   .+�5`      �� -   5        $ MinimalizeBtn��5`       $��25��67   $��25��6C   
������ 
������ .��5a      �� $$��/assets/images/minimalizer.png��6    6J   5b      �� 5c      -   5        $ crossBtn��5c      $��25��60   $��25��6<   
������ 
������ .��5d      �� $��/assets/images/cross.png��6    6D   5e      5f      � 5g      -	   5         �Name$ 
hoverCloud��5g       � AllowsTransparency$
 True��5h       � HorizontalOffset$ 10��5i       � VerticalOffset$ 10��5j      .��5k      � .+�5l         Secretary.HoverCloud  -
   5          Name$ Header��5l   +   ���#E0FFFF6;   5m      5n      5o      5p      5q      5r      