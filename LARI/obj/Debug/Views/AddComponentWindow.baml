   M S B A M L   `   `   `  ����?  ;LARI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:LARI.ViewModelsLARI.ViewModels  5      #    LARI.Views.AddComponentWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4
ViewModelsclr-namespace:LARI.ViewModels     5        � Title$  Add New Component��5      $	��500��5	   #   $	��450��6/    � DataContext 5
      +   %LARI.ViewModels.AddComponentViewModel  5   
   .��5      �� -   5         ��Name$ AddComponent��5       �� Orientation$ Vertical=�6'   .+�5   
   � $��20, 15, 0, 15q�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Description:��6   $	��100��6/   �� 5      $	��300��6n   $��25��6z    �� 	IsEnabled . $��14(�6�   ��6   ��  �� Path$ Description� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   5      5      � 5   
   $��20, 5, 0, 15q�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Date:��6   $��50��6(   (  "System.Windows.Controls.DatePicker  5      $	��105��6   $��25��6)   $	��0,6q�65   �� 5      $��           Location:��6   $��95��67   �� 5      $	��150��6k   $��25��6w   $��14(�6�   ��6   �� $ Location�$ TwoWay=�$ PropertyChanged=�6�   5      5      � 5   
   $��20, 5, 0, 15q�6   .+�5      �� $ 
Horizontal=�6   .+�5       �� $�� Flight Time:��6   $��75��60   �� 5!      $	��150��6m   $��25��6y   $��14(�6�   ��6   �� $ 
FlightTime�$ TwoWay=�$ PropertyChanged=�6�   �� 5"      $�� Active?��6   $��54��6+   � 5#      $��0, 7, 0, 15q�6   .+�5$      �� 5%      5&      5'      � 5(   
   $��20, 5, 0, 15q�6   .+�5)      �� $ 
Horizontal=�6   .+�5*      �� $��Previous Airframes:��6   $	��115��66   �� 5+      -   5        $	��200��5+      $��50��6&    . $"��Airframe (Removed  mm/dd/yy)��6N    �� TextWrapping$
 Wrap=�6�   5,      5-      � 5.   
   $��20, 5, 0, 15q�6   .+�5/      �� $ 
Horizontal=�6   .+�50      �� $��Crashes:��6   $	��115��6+   �� 51      -   5        $	��200��51      $��50��6&    . $ ��Severe - Airframe mm/dd/yy��6N   $
 Wrap=�6�   52      53      � 54   
   $��20, 5, 0, 15q�6   .+�55      �� $ 
Horizontal=�6   .+�56      �� $��Notes:��6   $	��115��6)   �� 57      $	��200��6h   $��50��6t    . $
 Wrap=�6�   ��6   �� $ Notes�$ TwoWay=�$ PropertyChanged=�6�   58      59      �� 5:   
   $ 
Horizontal=�6   $��Right=�6.   .+�5;      � $��0, 0, 10, 0q�6   .+�5<      �� $��Apply��6   $��50��6)   $��20��6]   $��Bottom=�6i   ��64   �� *ApplyComponentWindow+6i   5=      � 5>      $��0, 0, 10, 0q�6   .+�5?      �� $��Cancel��6   $��50��6*   ��65   �� *CancelComponentWindow+5@      5A      5B      5C      