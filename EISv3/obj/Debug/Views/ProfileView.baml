   M S B A M L   `   `   `  ����@  <EISv3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:EISv3.ViewModelEISv3.ViewModel  5          EISv3.Views.ProfileView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      &vmclr-namespace:EISv3.ViewModel  5      ���	#FF313E3A$��2q�5
   N   
���   �  � 	Resources  5      &    EISv3.ViewModel.ProfileViewModel   ProfileViewModel��%   &	          5   
   6   5      .��5      � ���	#FFF3F3F3$��30q�6"    � DataContext 6.   ��  �� Source# [   � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $
��Auto �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $K��EUse the form below to update information or insert (for new employee)��6   
���   �$��Center=�5      $��30��6-   $��0��6:   $��Center=�6G   $
��Bold"�6b   �� 5   
   $���{Note : All fields except Date of leaving and Vendor are mandatory. Select whole date and delete it once to clear date value��6   
���   �$��Center=�5      $��30��6-   $��1��6:   $��Center=�6H   �� 5   
   $��2��6   $��Center=�6"   $��Center=�6?    �� Orientation$ 
Horizontal=�6Z   .+�5      �� $ 
Horizontal=�6   $
��Left=�62   .+�5      �� .+�5       �� $
��Name��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5!      $��Email Id��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5#      $��Date Of Birth��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5$      $��Date Of Joining��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5%      $��Date Of Leaving��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5'      $��Vendor��6   $
��Left=�66   $
��Left=�6Q   
���   �y�6�   �� *
VendorGrid+ �� Mode$ TwoWay=�5(      �� 5*      $��Center=�6   .+�5+      �� $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 5,      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 5.      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 5/      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 50      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 52      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �y�6�   �� *
VendorGrid+$ TwoWay=�53      �� 55      .+�56      �� -   5        $��	FirstName��56      $��Center=�6�   $��Center=�6�   $	��150��6  $��0,0,5,20q�6  
���   ���6/   �� *EmpInfo.FirstName+ �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=�$ TwoWay=� � ToolTip 6�   �� *ErrorCollection[FirstName]+6%  �� 58      $��Center=�6�   $��Center=�6�   $	��150��6�   $��0,0,5,20q�6�   
���   ���6   �� *EmpInfo.EmailId+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[EmailId]+6  (  "System.Windows.Controls.DatePicker  5:      )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 
dd-mm-yyyy��6�   
   SelectedDateFormat$
 Short=�6�   $��Center=�6  $��Center=�6  $	��150��6<  $��0,0,5,20q�6H  
���   �   SelectedDate 6!   �� *EmpInfo.DOB+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[DOB]+6Z    5<      $	 
dd-mm-yyyy��6�   $
 Short=�6�   $��Center=�6  $��Center=�6  $	��150��6<  $��0,0,5,20q�6H  
���   � 6!   �� *EmpInfo.DOJ+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[DOJ]+6Z    5>      $	 
dd-mm-yyyy��6�   $
 Short=�6�   $��Center=�6  $��Center=�6  $	��150��6<  $��0,0,5,20q�6H  
���   � 6!   �� *EmpInfo.DOL+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[DOL]+6Z  �� 5@      $��Center=�6�   $��Center=�6  $	��150��6  $��0,0,5,20q�6+  
���   ���6   �� *EmpInfo.Vendor+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[Vendor]+y�6�   �� *
VendorGrid+$ TwoWay=�6=  5B      5C      �� 5E      $ 
Horizontal=�6   $��Right=�62   .+�5F      �� .+�5G      �� $��Center=�6�   $��Center=�6�   $	��150��6�   $��0,0,5,20q�6  
���   ���6   �� *EmpInfo.MiddleName+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[MiddleName]+6  �� 5H      $��Employee Id��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5I      $��Salary��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5J      $
��City��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   �� 5K      $��
Department��6   $
��Left=�66   $��Center=�6Q   $��0,0,0,20q�6l   
���   �6~   5L      �� 5N      .+�5O      �� $�� ��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 5P      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 5Q      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 5R      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   �� 5S      $��:��6   $��Center=�6(   $��Center=�6E   $��0,0,0,20q�6`   
���   �6r   5T      �� 5V      .+�5W      �� $��Center=�6�   $��Center=�6�   $	��150��6�   $��0,0,5,20q�6   
���   ���6   �� *EmpInfo.LastName+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[LastName]+6  �� 5X       �� 
IsReadOnly . $��Center=�6�   $��Center=�6�   $	��150��6  $��0,0,5,20q�6  
���   ���6   �� *EmpInfo.EmpId+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[EmpId]+6!  �� 5Y      $��Center=�6�   $��Center=�6�   $	��150��6�   $��0,0,5,20q�6�   
���   ���6   �� *EmpInfo.Salary+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[Salary]+6  �� 5Z      $��Center=�6�   $��Center=�6�   $	��150��6�   $��0,0,5,20q�6�   
���   ���6   �� *EmpInfo.City+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[City]+6  �� 5[      $��Center=�6�   $	��150��6
  $��0,0,5,20q�6  
���   � �� SelectedItem 6   �� *EmpInfo.Department+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[Department]+��6�   �� *Departments+6(  5^      5_      5a      �� 5c   
   $ 
Horizontal=�6   $��3��6.   $��Center=�6;   .+�5d      �� $��Address��6   $��Center=�6&   $��Center=�6C   $��0,0,0,20q�6^   
���   �6p   �� 5e      $��:��6   $��Center=�6&   $��Center=�6C   $��0,0,0,20q�6^   
���   �6p   �� 5f       �� TextWrapping$
 Wrap=�6    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6?    �� 	IsEnabled . $��Center=�6  $��Center=�67  $��40��6R  $	��300��6a  $��0,0,5,20q�6m  
���   ���6~   �� *EmpInfo.Address+ . $ PropertyChanged=�$ TwoWay=� 6�   �� *ErrorCollection[Address]+6  5g      � 5i   
   $��4��6    � ColumnDefinitions 5j      �� 5k      �� 5l      .+�5o      �� -   5         ��Name$ Submit��5p       $  MaterialDesignRaisedAccentButton#��[ 5o      +  %MaterialDesignThemes.Wpf.RippleAssist   Feedback �	#DD000000$��Update Profile��5p   Z   $��0��5q      $��Center=�6!   $��36��6<   $	��150��6H   {�5p   !   �� *EmpInfo.SubmitEnabled+$ TwoWay=���5r      �� *UpdateProfile+�� 5t      -   5        $ GoBack��5u      #��[ 5t       �	#DD000000$��Cancel��5u   !   $��1��5v      $��Center=�6!   $��36��6<   $	��150��6H   ��5w      �� *GoToHomePage+5x      5y      5z      