   M S B A M L   `   `   `  ����?  ;LARI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:LARI.ViewModelsLARI.ViewModels  5      5clr-namespace:UW.LARI.DatatypesUW.LARI.Datatypes  5	           LARI.Views.AddSystemWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4
ViewModelsclr-namespace:LARI.ViewModels     5      5	Datatypesclr-namespace:UW.LARI.Datatypes     5	      -   5          �Name$  AddSystemWindowName��5       � Title$ 
Add System��5      $	��500��5   #   $	��315��6/   .��5      �� -   5         ��Name$ 	AddSystem��5       �� Orientation$ Vertical=�6$   .+�5   
   � $��0, 15, 0, 15q�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Name:��6   $	��100��6(   �� 5      -   5        $	��200��5      $��14(�6&   $��26��6�   $��
0,0,0,-0.4q�6�   $��Bottom=�6�   ��64   ��  �� Path$
 Name� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   5      5      � 5   
   $��0, 15, 0, 15q�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Description:��6   $	��100��6/   �� 5      -   5        $	��300��5      $	��100��6&    �� TextWrapping$
 Wrap=�63   ��6G   �� $ Description�$ TwoWay=�6v   5      5      � 5   
   $��0, 15, 0, 15q�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��
Wing Type:��6   $	��100��6-   �� 5      -   5         ��Name$ 	Wing_Type��5      $	��200��6X   	 �� 
IsReadOnly	 . 
�������
�������
 �� SelectedItem
 6.   �� *SelectedWingType+.@�5      !   UW.LARI.Datatypes.WingTypes Unspecified=�67    5      	FixedWing=�65    5       Quad=�60    5!      Octo=�60    5"      None=�60   5#      5$      5%      �� 5&   
   $ 
Horizontal=�6   $��Right=�6.   .+�5'      � $��0, 0, 10, 0q�6   .+�5(      �� $��Apply��6   $��50��6)   $��
0,0,-0.8,0q�6_   $��19��6s   $	��Top=�6   ��64   �� $ ApplySystemWindow�6   5)      � 5*      $��0, 0, 15, 0q�6   .+�5+      �� $��Cancel��6   $��50��6*    �� IsCancel . 65   5,      5-      5.      5/      