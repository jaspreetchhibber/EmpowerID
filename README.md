# EmpowerID
Select Both "EmpowerID.WebAPI" and "EmpowerID_10Mar" Projects as Startup Project
Need To Run Migrations before starting, by using following command in "Package Console Manager" , must select "EmpowerID.DAL" in Default Project:
update-database
Please add some "Departments" using this script

SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT INTO [dbo].[Departments] ([Id], [DepartmentName]) VALUES (1, N'Accounts')
INSERT INTO [dbo].[Departments] ([Id], [DepartmentName]) VALUES (2, N'Sales')
INSERT INTO [dbo].[Departments] ([Id], [DepartmentName]) VALUES (3, N'IT')
SET IDENTITY_INSERT [dbo].[Departments] OFF
