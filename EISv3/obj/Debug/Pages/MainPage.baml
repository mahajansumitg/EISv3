   M S B A M L   `   `   `  ����@  <EISv3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:EISv3.PagesEISv3.Pages  5      1clr-namespace:EISv3.PageModelEISv3.PageModel  5	          EISv3.Pages.MainPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      %localclr-namespace:EISv3.Pages  5      &pmclr-namespace:EISv3.PageModel  5	      $	��600��5      $
��1024��6     N� Title$  MainPage��5       N� 
Foreground
 ����� � 	Resources 5      #   EISv3.PageModel.MainPageModel   MainPageModel  OpenMenu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  	CloseMenu��%�  &	        &	      &	 �       5   
   6   �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $ 70Q�63     5      $ 0:0:0.5��6'   $	 200Q�69   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $	 200Q�63     5      $ 0:0:0.5��6'   $ 70Q�69   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ ButtonBase.Click��6   	 :� 
SourceName$	 ButtonOpenMenu��66   .[�5      �� #��[ 6   5       :� 5!   
   $ ButtonBase.Click��6   $	 ButtonCloseMenu��66   .[�5"      �� #��[ 6   5#      .��5&      � ���	#FFDEDEDE
 � DataContext
 6"   ��  �� Source# [  .+�5(   
   �� ��6   �� *
Contents+��5)      �� 5*      .R�5+      � $��GameMain��6   ���	#FF4D4D4D$��	70,60,0,0q�6B   5,      5.      � 50   
   $��60��6   $	��Top=�6   ���	#FF002040 J� Effect 51      3  -System.Windows.Media.Effects.DropShadowEffect  52         
BlurRadius$ 1Q�6#      ShadowDepth$ 1Q�62      Color$ 	#FFACACAC��6B   .+�55      �� $
��Left=�6   $��Center=�63   $��16(�6N   $��80,0,0,0q�6�   ��6\   �� *EmpIDMainPage+ �� Mode$ TwoWay=�6�   �� 56      $��Center=�6   $��Center=�65   $��22(�6P   Employee Information System6{   �� 57      $��Right=�6   $��Center=�64   $��16(�6O   $��0,0,60,0q�6�   ��6]   �� *RoleMainPage+$ TwoWay=�6�   �� 58       �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�59      '  !MaterialDesignThemes.Wpf.PopupBox     PlacementMode$ BottomAndAlignRightEdges=�6*      	StaysOpen .  $��10q�6e      PopupContent. 5:      �� $	��150��6!   .+�5;      �� $��Logout��6!   ��62   �� *Logout+�� 5<      $
��Help��6!   ��60   �� *Help+�� 5=      �� 5>      $
��Exit��6!   ��61   �� *Exit+5?      5@      5A      5C      � 5E   
   -   5         �Name$ GridMenu��5E      $��70��6!   $
��Left=�6,   
���   � 5F        5G      $ 2Q�6#   $ 1Q�62   $ 	#FFAEAEAE��6B   .+�5I      �� .+�5J      � $��60��6    J� RenderTransformOrigin$ 0.5,0.5&�6#    J� OpacityMask �	#FFD87A08���	#FFB8B8B8z�5K      V� 5L      .~�5M      �� �� 5N      �� 5O      T� 5P      5Q      .+�5S      �� -   5         ��Name$ ButtonOpenMenu��5S      $��60��6]   $��60��6i   $��5q�6t   $��Right=�6    0 ,MaterialDesignFloatingActionMiniAccentButton#��[ 6�   
���   �$��1,1,5,1q�6>  y�65   �� *OpenMenuVisibily+��6�   Z� ��6�   Z� ��6!  �� *
OpenMenu+.��5T      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$
 Menu=�62   $��25��6>   $��25��6I   5U      �� 5V      -   5        $ ButtonCloseMenu��5V      $��60��6_   $��60��6k   $��5q�6v   $��Right=�6�   $	��Top=�6�   #��[ 6�   ���	#FF31577Ey�66   �� *CloseMenuVisibily+��6�   Z� ��6  Z� ��6?  �� *	CloseMenu+.��5W        $ 	ArrowLeft=�62   $��25��6C   $��25��6N   5X      5Y      �� 5[      -   5         ��Name$ ListViewMenu��5[      ���	#FF5C99D6$��Disabled=�6H    �� SelectedItem 6~   �� *SelectedItem+ �� UpdateSourceTrigger$ PropertyChanged=�$ TwoWay=�.  (System.Windows.Interactivity.Interaction   Triggers 5\      /  )System.Windows.Interactivity.EventTrigger  5]         	EventName$ SelectionChanged��6)   .	  (System.Windows.Interactivity.TriggerBase 	  Actions. 5^      6
  0System.Windows.Interactivity.InvokeCommandAction
    
  Command  64   �� *SelectionChanged+! 
  CommandParameter! 6Y   �� " �� ElementName$" ListViewMenu��5_      .@�5b      �� -   5        # ��Name$# HomeView��5b   #   $��60��65   y�6A   �� *HomeVisibility+.��5c      �� $ 
Horizontal=�6%   .+�5d        $ HomeOutline=�66   $��25��6I   $��25��6U   $��10q�6`   �� 5e      $
��Home��6(   $��Center=�64   $��20 10q�6O   5f      5g      �� 5i      -   5        $# ProfileView��5i   #   $��60��68   .��5j      �� $ 
Horizontal=�6%   .+�5k        $ Pencil=�66   $��25��6D   $��25��6P   $��10q�6[   �� 5l      $
��Form��6(   $��Center=�64   $��20 10q�6O   5m      5n      �� 5p      -   5        $# DashBoardView��5p   #   $��60��6:   y�6F   �� *DashBoardVisibility+.��5q      �� $ 
Horizontal=�6%   .+�5r        $ ViewDashboard=�66   $��25��6K   $��25��6W   $��10q�6b   �� 5s      $��	DashBoard��6(   $��Center=�69   $��20 10q�6U   5t      5u      5v      5w      5x      5y      5z      