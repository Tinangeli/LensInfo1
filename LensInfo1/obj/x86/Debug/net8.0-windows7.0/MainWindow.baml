   M S B A M L   `   `   `  ����D  @LensInfo1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LensInfo1	LensInfo1  5   	   ?$clr-namespace:LensInfo1.UserControlsLensInfo1.UserControls  5   	       LensInfo1.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:LensInfo1     5   	   =UserControls$clr-namespace:LensInfo1.UserControls     5   	     � Title$  
MainWindow��5	   	   $	��600��6   $	��800��6)   .��5      � 
������� � RowDefinitions 5   
   �� 5      $	��30* �6   �� 5      $	��70* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      �� 5       5      .+�5      � $��0��6   
������� 5       �� 5!      $	��60* �6$   �� 5"      $	��30* �6$   .+�5%      � $��0��6    5&      �� 5'      �� 5(      .+�5*      �� -   5         ��Name$ TimeAndDataRunning��5*   <   $��0��6    $��No Data��6-   $��Center=�6X   $��20(�6s   � 5+      $��1��6    5,      �� 5-      �� 5.      .+�50      �� -   5        $ 
NameOfUser��50   3   $��No Data��6$   $��Center=�6G   $��20(�6b   51      52      � 53      $��1��6   
������� 54      �� 55      �� 56      �� 57      .+�59      &    LensInfo1.UserControls.TopButton  -   5          Name$ 	AddButton��59   A      ContentHolder$	 Add��6-   6T     5:      -   5        $ 
EditButton��5:   R   $
 Edit��6-   $��1��6B   6f     5;      -   5        $ DeleteButton��5;   T   $ Delete��6-   $��2��6D   6j   5<      5>      � 5B      $��1��6   .+�5D      �  5E      �� 5F      �� 5G      5L      5O      5Q      � 5U   
   $��1��6   $��5q�6    5V      �� 5W      $	��30* �6#   �� 5X      $	��80* �6#   .+�5[      � $��0��6   5]      � 5a      $��1��6   
�������.+�5c      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridEmployee��5c      
�������$��Stretch=�6O   $��Stretch=�6k      AutoGenerateColumns$ False��6�      Columns 5d      0  *System.Windows.Controls.DataGridTextColumn  5e      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6-   -   5        EmployeeColumnID��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 5e   S   �� *IDNum+  5f      $	 
First Name��6-   -   5        EmployeeColumnFirstName��
 5f   b   �� *	FirstName+  5g      $	 	Last Name��6-   -	   5        EmployeeColumnLastName��
 5g   `   �� *
LastName+  5h      $	 Phone Number��6-   -
   5        EmployeeColumnPhoneNum��
 5h   c   �� *PhoneNumber+  5i      $	 Position��6-   -   5        EmployeeColumnPosition��
 5i   _   �� *
Position+  5j      $	 Username��6-   -   5        EmployeeColumnUsername��
 5j   _   �� *
Username+  5k      $	 Password��6-   -   5        EmployeeColumnPassword��
 5k   _   �� *
Password+  5l      $	 QRCode��6-   -   5        EmployeeColumnQRCode��
 5l   [   �� *	QRLogin+  5m      $	 Identity Number��6-   -   5        EmployeeIdentityNumber��
 5m   f   �� *EmployeeIdentityNumber+5w        5z      -   5        $ DataGridMovies��5z      
�������$��Stretch=�6J   $��Stretch=�6f   $ False��6�   ��6�   �� *Movies+ 5{        5|      $	 ID��6-   
 69   �� *	IDMovie+  5}      $	 
Movie Name��6-   
 6A   �� *	MovieName+  5~      $	 Suitable Age��6-   
 6C   �� *
AgeLimit+  5      $	 Movie Duration��6-   
 6E   �� *
Duration+  5�      $	 Movie Genre��6-   
 6B   �� *Genre+  5�      $	 
Date Added��6-   
 6A   �� *	DateAdded+  5�      $	 Poster��6-   
 6=   �� *PosterBytes+5�        5�      -   5        $ DataGridCustomer��5�      ���#FFDAB9$ False��6J   $��Stretch=�6f   $��Stretch=�6�    5�        5�      $	 ID��6-   
 69   �� *
IDCustomer+  5�      $	 
First Name��6-   
 6A   �� *	FirstName+  5�      $	 	Last Name��6-   
 6@   �� *
LastName+  5�      $		 Age��6-   
 6:   �� *Age+  5�      $	 Username��6-   
 6?   �� *
Username+  5�      $	 Password��6-   
 6?   �� *
Password+  5�      $	 Identity Number��6-   -   5        CustomerIdentityNumber��
 5�   f   �� *CustomerIdentityNumber+5�      5�      � 5�      $��0��6   
������� 5�      �� 5�      �� 5�       5�      �� 5�      �� 5�      .+�5�      � $��0��6   $��0��6$    5�      �� 5�      �� 5�      �� 5�      .+�5�        -   5        $ btnEmployee��5�   -   $ Employee��6B   $��0��6[   6h     5�      -   5        $ 	btnMovies��5�   -   $ Movies��6@   $��1��6W   6d     5�      -   5        $ btnCustomer��5�   -   $ Customer��6C   $��2��6\   6i   5�      � 5�      $��1��6    5�      �� 5�      �� 5�      �� 5�      .+�5�      � -   5         �Name$ EmployeeSubButtonGrid��5�   (   $��0��6    5�      �� 5�      �� 5�      .+�5�        -   5        $ BtnEmployeeSchedule��5�   >   $��0��61   $ Schedule��6[     5�      -   5        $ BtnEmployeeSalary��5�   >   $��1��61   $ Salary��6Y   5�      � 5�      -   5        $ MoviesSubButtonGrid��5�   (   $��1��6    5�      �� 5�      �� 5�      .+�5�        -   5        $ BtnMoviesSchedule��5�   >   $��0��61   $ Schedule��6Y     5�      -   5        $ BtnMoviesSale��5�   >   $��1��61   $ Sales��6U   5�      � 5�      -   5        $ CustomerSubButtonGrid��5�   (   $��2��6    5�      �� 5�      �� 5�      .+�5�        -   5        $ CustomerSales��5�   >   $��0��61   $ Sales��6U     5�      $��1��61   5�      5�      5�      5�      5�      5�      