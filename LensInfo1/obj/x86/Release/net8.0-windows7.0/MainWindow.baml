   M S B A M L   `   `   `  ���� D  @LensInfo1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LensInfo1	LensInfo1  ?$clr-namespace:LensInfo1.UserControlsLensInfo1.UserControls      LensInfo1.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     @d2http://schemas.microsoft.com/expression/blend/2008     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:LensInfo1     =UserControls$clr-namespace:LensInfo1.UserControls       � Title$  
MainWindow��$	��600��$	��800��.��� 
������� � RowDefinitions �� $	��30* ��� $	��70* �.+�� $��0�� � ColumnDefinitions �� ��  .+�� $��0��
������� �� $	��60* ��� $	��30* �.+�� $��0�� �� �� .+�)   #LensInfo1.UserControls.TextInputBox  $��0��   	txtholder$ First��  $��1��$	 Sec��� $��1��
������� �� �� �� .+�&    LensInfo1.UserControls.TopButton  -     Name$ 	AddButton��   ContentHolder$	 Add��  $
 Edit��$��1��  $ Delete��$��2��� $��1��.+��  �� �� .+�  -   $ RefreshButton��$��1��$ Refresh��� $��1��$��5q� �� $	��30* ��� $	��80* �.+�� $��0��� $��1��
�������.+�&   System.Windows.Controls.DataGrid  -     Name$ DataGridEmployee��
�������$��Stretch=�$��Stretch=�   AutoGenerateColumns$ False��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��-   EmployeeColumnID��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *IDNum+  $	 
First Name��-   EmployeeColumnFirstName��
 �� *	FirstName+  $	 	Last Name��-   EmployeeColumnLastName��
 �� *
LastName+  $	 Phone Number��-   EmployeeColumnPhoneNum��
 �� *PhoneNumber+  $	 Position��-   EmployeeColumnPosition��
 �� *
Position+  $	 Username��-	   EmployeeColumnUsername��
 �� *
Username+  $	 Password��-
   EmployeeColumnPassword��
 �� *
Password+  -   $ DataGridMovies��
�������$��Stretch=�$��Stretch=�$ False��   $	 ID��
 �� *	IDMovie+  $	 
Movie Name��
 �� *	MovieName+  $	 Suitable Age��
 �� *
AgeLimit+  $	 Movie Duration��
 �� *
Duration+  $	 Movie Description��
 �� *MovieDescription+  $	 
Date Added��
 �� *	DateAdded+  -   $ DataGridCustomer�����#FFDAB9$ False��$��Stretch=�$��Stretch=�   $	 ID��
 �� *
IDCustomer+  $	 
First Name��
 �� *	Firstname+  $	 	Last Name��
 �� *
Lastname+  $		 Age��
 �� *Age+  $	 Username��
 �� *
Username+  $	 Password��
 �� *
Password+� $��0��
������� �� ��  �� �� .+�� $��0��$��0�� �� �� �� .+�  -   $ btnEmployee��$ Employee��$��0��  -   $ 	btnMovies��$ Movies��$��1��  -   $ btnCustomer��$ Customer��$��2��� $��1�� �� �� �� .+�� -    �Name$ EmployeeSubButtonGrid��$��0�� �� �� .+�  -   $ BtnEmployeeSchedule��$��0��$ Schedule��  -   $ BtnEmployeeSalary��$��1��$ Salary��� -   $ MoviesSubButtonGrid��$��1�� �� �� .+�  -   $ BtnMoviesSchedule��$��0��$ Schedule��  -   $ BtnMoviesSale��$��1��$ Sales��� -   $ CustomerSubButtonGrid��$��2�� �� �� .+�  -   $ CustomerSales��$��0��$ Sales��  $��1��