   M S B A M L   `   `   `  ����K  GInventoryManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:InventoryManagerInventoryManager  5   	       InventoryManager.AddItem   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:InventoryManager     5   	     � Title$  Add Item��5   	   $��518.4��6   $	��250��6)   .��5	      � .+�5
   
   �� $
��Left=�6   $��
105,32,0,0q�6/   $
��Wrap=�6C   $
��Name��6W   $	��Top=�6c   �� 5   
   $
��Left=�6   $��
95,102,0,0q�6/   $
��Wrap=�6C   $��Description��6W   $	��Top=�6j   �� 5   
   $
��Left=�6   $��
94,167,0,0q�6/   $
��Wrap=�6C   $��Category��6W   $	��Top=�6g   �� 5   
   -   5         ��Name$ 	item_name��5      $
��Left=�6%   $��23��6@   $��	63,53,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��120��6�   �� 5   
   -   5        $ 	item_desc��5      $
��Left=�6%   $��23��6@   $��
63,123,0,0q�6L   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��120��6�   �� 5   
   -   5         ��Name$ categories_box��5      $
��Left=�6+   $��
63,188,0,0q�6F   $	��Top=�6Z   $	��120��6r   6~   �� 5   
   -   5        $
��Save��5      $
��Left=�6    $	��Top=�6;   $��75��6S   $��
80,443,0,0q�6^   6r   �� 5   
   $
��Left=�6   $��
93,304,0,0q�6/   $
��Wrap=�6C   $��Quantity��6W   $	��Top=�6g   �� 5   
   $
��Left=�6   $��105,362,0,0q�6/   $
��Wrap=�6D   $��Price��6X   $	��Top=�6e   �� 5   
   -   5        $ item_quantity��5      $
��Left=�6)   $��23��6D   $��
63,334,0,0q�6P   $
 Wrap=�6d   $��0��6x   $	��Top=�6�   $	��120��6�   6�   �� 5   
   -   5        $ 
item_price��5      $
��Left=�6&   $��23��6A   $��
63,383,0,0q�6M   $
 Wrap=�6a   $
��0.00��6u   $	��Top=�6�   $	��120��6�   6�   �� 5   
   $
��Left=�6   $��
89,240,0,0q�6/   $
��Wrap=�6C   $��Subcategory��6W   $	��Top=�6j   �� 5   
   -   5        $��subcategory_container��5      $
��Left=�60   $��
63,261,0,0q�6K   $	��Top=�6_   $	��120��6w   5      5      