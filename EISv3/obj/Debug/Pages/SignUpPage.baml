   M S B A M L   `   `   `  ����@  <EISv3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:EISv3.PageModelEISv3.PageModel  5   	       EISv3.Pages.SignUpPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   )Localclr-namespace:EISv3.PageModel  5   	     N� Title$  
SignUpPage��5   	    � 	Resources 5      %   EISv3.PageModel.SignUpPageModel   vm��%   &	          5   
   6    5      .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $
��Auto �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $
��Auto �6   �� 5      $��1* �6   .+�5   
   � $��1��6   $��1��6   
������� � DataContext 6?   ��  �� Source# [   5      �� 5      �� 5       �� 5!      .+�5$      �� $��0��6   $.��(Fill your details below to ceate account��6!   $��Center=�6U   $��Center=�6r   $��Bookman Old Style)�6�   $��Center=�6�   $��Center=�6�   $
��Bold"�6�   $��	0,10,0,10q�6  �� 5&      $��1��6   $��Center=�6&   $��Center=�6C    �� Orientation$ 
Horizontal=�6^   .+�5(      �� $��5q�6   .+�5)      �� 
���   �$��UserName��6/   $
��Left=�6J   $��Center=�6e   $��0,0,0,20q�6�   �� 5*      
���   �$��Password��6/   $
��Left=�6J   $��Center=�6e   $��0,0,0,20q�6�   �� 5+      
���   �$��Confirm Password��6/   $
��Left=�6J   $��Center=�6e   $��0,0,0,20q�6�   �� 5,      
���   �$
��Role��6/   $
��Left=�6J   $��Center=�6e   $��0,0,0,20q�6�   �� 5-      
���   �$��Sec key��6/   $
��Left=�6J   $��Bottom=�6e   y�6�   �� *Login.HighSecPwdVisibility+ �� Mode$ TwoWay=�5.      �� 50      $��5q�6   .+�51      �� 
���   �$��:��6/   $��Center=�6;   $��Center=�6X   $��0,0,0,20q�6s   �� 52      
���   �$��:��6/   $��Center=�6;   $��Center=�6X   $��0,0,0,20q�6s   �� 53      
���   �$��:��6/   $��Center=�6;   $��Center=�6X   $��0,0,0,20q�6s   �� 54      
���   �$��:��6/   $��Center=�6;   $��Center=�6X   $��0,0,0,20q�6s   �� 55      
���   �$��:��6/   $��Center=�6;   $��Bottom=�6X   y�6s   �� *Login.HighSecPwdVisibility+$ TwoWay=�56      �� 58      $��5q�6   .+�5:      �� -   5        $��UserName��5:      
���   �$	��150��6�   $��Center=�6�   $��Center=�6  ���	#FFFFFBFB$��0,0,5,20q�68  ��6A   �� *Login.UserName+ �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�$ TwoWay=�
 � ToolTip
 6�   �� *ErrorCollection[UserName]+68  �� 5<      -   5        $��Password��5<      
���   �$	��150��6�   $0��*pack://application:,,,/Resources/#password)�6�   $��Center=�6  $��Center=�66  ���	#FFFFFBFB$��0,0,5,20q�6h  ��6A   �� *
Login.PSWD+ . $	 PropertyChanged=�$ TwoWay=�
 6�   �� *ErrorCollection[PSWD]+6h  �� 5>      -   5        $��ConfirmPassword��5>      
���   �$0��*pack://application:,,,/Resources/#password)�6�   $	��150��6"  $��Center=�6.  $��Center=�6K  ���	#FFFFFBFB$��0,0,5,20q�6}  ��6H   �� *Login.ConfirmPSWD+ . $	 PropertyChanged=�$ TwoWay=�
 6�   �� *ErrorCollection[ConfirmPSWD]+6}  �� 5@      -   5        $
��Role��5@      
���   �$	��150��5C      $��Center=�6#   $��Center=�6@   $��0,0,5,20q�6[    �� SelectedItem 5A      �� *
Login.Role+$ TwoWay=�$	 PropertyChanged=���5B      �� *Roles+5C   [   �� 5E      -   5        $��SecKey��5E      
���   �$0��*pack://application:,,,/Resources/#password)�6�   $	��150��6
  $��Center=�6  $��Center=�63  ���	#FFFFFBFB$��0,0,5,0q�6e  ��6?   �� *Login.HighSecPwd+$	 PropertyChanged=�$ TwoWay=�y�6�   �� *Login.HighSecPwdVisibility+$ TwoWay=�6e  5F      5H      � 5J      $��2��6   $��	0,15,0,10q�6     5K      �� 5L      �� 5M      .+�5P      ��  $  MaterialDesignRaisedAccentButton#��[ 6   +  %MaterialDesignThemes.Wpf.RippleAssist   Feedback �	#DD000000$��Sign Up��6�   $��0��6�   $	��100��6�   $��30��6�   $��Center=�6	  {�6�   �� *SignUpEnable+$ TwoWay=���6�   �� *SignUp+6	  �� 5R      #��[ 6    �	#DD000000$��Cancel��6�   $��1��6�   $	��100��6�   $��30��6�   $��Center=�6�   ��6�   �� *Cancel+6�   5S      5U      5V      5W      