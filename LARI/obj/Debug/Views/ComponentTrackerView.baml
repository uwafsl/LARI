   M S B A M L   `   `   `  ����?  ;LARI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:LARI.ViewModelsLARI.ViewModels  5      %    LARI.Views.ComponentTrackerView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4
ViewModelsclr-namespace:LARI.ViewModels     5      .��5	      � $��10q�6     � RowDefinitions  5
   
   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��25��6   $��0, 10, 0, 5q�6(    � ColumnDefinitions 5      �� 5      $
��0.5* �6#   �� 5      $
��0.5* �6#   .+�5      � $��0��6    5      �� 5      $	��110 �6'   �� 5      $��* �6'   .+�5      �� $��0��6   $��Database File Path:��6(   $��Center=�6F   $��0,0,0,0q�6a   $��0,0,0,0q�6r   �� 5      $��1��6    �� 
IsReadOnly . $��2,4,2,0q�5      $
��auto��6   $��25��6$   ��5   <   ��  �� Path$ EquipageFilePath� �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�5   $   5      �� 5      $��1��6    �� Orientation$ 
Horizontal=�6)   $��20, 0, 0, 0q�6B    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%�   '��       �� 5       " ��6   .�5!      �� 	 � ��$
 0, 0, 10, 0q�63   �� 5"      	 �@�Cursor$

 Hand��63   5#      5$      .+�5%      �� $��	Browse...��6   $��50��6X   ��6-   �� *BrowseEquipageFilePath+6X   �� 5&      $
��Load��6   $��50��6C   ��6(   �� *LoadDb+6C   �� 5'      $
��Save��6   $��50��6C   ��6(   �� *SaveDb+6C   �� 5(      $��Save As��6   $��50��6+   5)      5*      � 5,      -   5         �Name$ SystemComponentGrid��5,      $��1��6     5-      �� 5.      $��46��6    �� 5/      $��5 �6    �� 50      $��46��6    .+�52      � -   5        $ SystemsHeader��52       $��0��6   $��0,10,0,0q�67   $	��Top=�6I   $��21��6a   .+�53      �� $��0��6   $��Systems��6,   $
��Left=�6;   $��Bottom=�6V   $��16(�54      �� 55      -   5         ��Name$ SystemsButtonsContainer��55      $��1��6>   $ 
Horizontal=�6N   $��Right=�6g   .+�56      �� -   5        $	��Add��56      $��50��6+   ��69   �� *AddSystemCommand+6^   �� 57      $
��Edit��6   $��50��6,   ��6:   �� *EditSystemCommand+�� 58      $��Delete��6   $��50��6.   ���#ff7070��6Q   �� *DeleteSystemCommand+$ PropertyChanged=�59      5:      � 5;      $��0��6   $��0,41,0,5q�6    $��Stretch=�62   .+�5<      &   System.Windows.Controls.DataGrid  -   5        $��SystemsTable��5<         
IsReadOnly .    SelectionMode$ Single=�5>         SelectionUnit$ FullRow=�5?   E      AlternatingRowBackground
 ����� �� AlternationCount$ 2��5@   D      AutoGenerateColumns .  $��Stretch=�5A   9   ��5=      �� *	Systems+$ PropertyChanged=� �� SelectedItem 5?      �� *SelectedSystem+   Columns 5B      0  *System.Windows.Controls.DataGridTextColumn  5C      ,  &System.Windows.Controls.DataGridColumn   Header$ System��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *Name+  5D      $ Description��6-    6D   �� *Description+  5E      $ 	Wing Type��6-    6B   �� *WingTypeName+5G      5H      �� 5I      $��1��6   $��Stretch=�6(   $	��0,1q�6F   ���	#FF908F9C J� Opacity$	 0.5Q�6j   � 5J      -   5        $ ComponentsHeader��5J       $��2��6   $��0,5,0,-1q�6:   $	��Top=�6L   $��21��6d   .+�5K      �� $��0��6   $��
Components��6,   $
��Left=�6>   $��Bottom=�6Y   $��16(�5L      �� 5M      -   5        $ ComponentsButtons��5M   -   $��1��6   $��Right=�6H   $ 
Horizontal=�6d   .+�5N      �� $	��Add��6   $��50��6+   ��6:   �� *AddComponentCommand+�� 5O      $
��Edit��6   $��50��6,   ��6:   �� *EditComponentCommand+�� 5P      $
��Copy��6   $��50��6,   �� 5Q      $��Delete��6   $��50��6.   ���#ff7070��6Q   �� *DeleteComponentCommand+$ PropertyChanged=�5R      5S      � 5T      $��2��6   $��0,31,0,5q�6    $��Stretch=�62   .+�5U        -   5        $��ComponentsTable��5U      $ Extended=�5X      $ FullRow=�5Y      
 �����$ 2��5[       . $��0,0,0,0q�5]      $��Stretch=�6d   ��5V      �� *
Components+$ PropertyChanged=� 5W      �� *SelectedComponents+ 5^        5_      $ ID��6-    6;   �� *Id+  5`      $ 	Component��6-    6B   �� *Name+  5a      $ Description��6-    6C   �� *Description+  5b      $ 
Start Date��6-    6C   �� *	StartDate+  5c      $ Flight Time��6-    6D   �� *
FlightTime+  5d      $ Location��6-    6?   �� *
Location+  5e      $ Serial Number��6-    6D   �� *SerialNumber+  5f      $ History��6-    6>   �� *	History+4  .System.Windows.Controls.DataGridCheckBoxColumn  5g      $ Active��61    6A   �� *Active+  5h      $ Damaged��61    6B   �� *	Damaged+5j      5k      5l      5m      5n      