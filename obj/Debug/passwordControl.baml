   M S B A M L   `   `   `  ����>  :4PD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:_4PD_4PD  5          _4PD.passwordControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:_4PD     5      .��5      � 
�������$��0,0,0,5q�6"     � RowDefinitions  5	   
   �� 5
      $
��auto �6   �� 5      $��* �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��auto �6#   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��5q�62   .+�5      �� $��Name: ��6   $
��Bold"�6*   �� 5      -   5         ��Name$ tblName��5      $
��name��6-   �� 5      $�� URL: ��6   $
��Bold"�6*   �� 5      -   5        $ tblURL��5      $	��url��6,   ���	#FF0027FD6N   �� 5      $��
 Comment: ��6   $
��Bold"�6.   �� 5      -   5        $ 
tblComment��5      $��WrapWithOverflow=�60   5      �� 5      -   5         ��Name$ 	btnDelete��5   %   $��1��6   $��20��68   $��X��5      $
��bold"�6!   $��5q�63   6>   5      � 5   
   $��1��6   $��5q�6    5      �� 5       $
��auto �6#   �� 5!      $��* �6#   �� 5"      $
��auto �6#   .+�5$      �� $��
Password: ��6   $
��Bold"�6*   �� 5%      -   5        $ tblPassword��5%      $y�Visible=�6-   $��password��6B   $��1��6R   �� 5&      -   5         ��Name$ tbxPassword��5&      $y�	Collapsed=�6+   $��password��6B   $��1��6R    �� 
IsReadOnly . 6b   �� 5'      $ 
Horizontal=�6   $��2��62   $��5,0,0,0q�6B   .+�5(      �� -   5        $ btnCopy��5(      $y�Visible=�6*   $
��📄��6?   $��20��6L   $��0,0,5,0q�6W   6h   �� 5)      -   5        $ btnShowHide��5)      $y�Visible=�6.   $��20��6C   $��0,0,5,0q�6N   $��👁️��6_   6m   �� 5*      -	   5        $ btnEdit��5*      $y�Visible=�6*   $��50��6?   $
��Edit��6J   {�.  6o   �� 5+      -
   5        $ btnSave��5+      $y�	Collapsed=�6*   $	��100��6A   $
��Save��6M   6\   5,      5-      5/      50      