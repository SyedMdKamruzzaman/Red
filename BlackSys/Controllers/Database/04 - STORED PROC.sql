USE REDFINAL;
GO
IF EXISTS (SELECT * FROM sysobjects WHERE NAME ='SP_Load_Menu')
BEGIN
	DROP PROCEDURE dbo.SP_Load_Menu
	PRINT 'EL PROCEDIMIENTO "SP_Load_Menu" SE BORRO CORRECTAMENTE'
END
GO
CREATE PROCEDURE dbo.SP_Load_Menu
@UserID NVARCHAR(128)
AS
	DELETE MenuTemp
	DECLARE @MenuID INT, @DisplayName NVARCHAR(50),	@ParentMenuID INT, @OrderNumber INT,@MenuURL NVARCHAR(100), @MenuIcon NVARCHAR(25)
	DECLARE MENU_CURSOR CURSOR
	FOR SELECT a.MenuID,a.DisplayName,a.ParentMenuID, a.OrderNumber, a.MenuURL,a.MenuIcon  
		FROM Menu a	
		INNER JOIN Permission b ON b.MenuID = a.MenuID
		INNER JOIN AspNetRoles c ON c.Id = b.RoleID		
		INNER JOIN AspNetUserRoles d ON d.RoleId = c.Id and d.UserId= @UserID
		WHERE a.ParentMenuID=0
		
		UNION ALL
		SELECT a.MenuID,a.DisplayName,a.ParentMenuID, a.OrderNumber, a.MenuURL,a.MenuIcon
		FROM Menu a	
		INNER JOIN CustomPermission b ON b.MenuID = a.MenuID
		INNER JOIN AspNetUsers c ON c.Id =b.UserID AND c.Id =@UserID			
		WHERE a.ParentMenuID=0

	OPEN MENU_CURSOR
	FETCH NEXT FROM MENU_CURSOR INTO @MenuID, @DisplayName,	@ParentMenuID, @OrderNumber,@MenuURL, @MenuIcon
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO MenuTemp SELECT @MenuID, @DisplayName,	@ParentMenuID, @OrderNumber,@MenuURL, @MenuIcon
		INSERT INTO MenuTemp SELECT a.MenuID,a.DisplayName,a.ParentMenuID, a.OrderNumber, a.MenuURL,a.MenuIcon FROM Menu a		
		INNER JOIN Permission b ON b.MenuID = a.MenuID
		INNER JOIN AspNetRoles c ON c.Id = b.RoleID		
		INNER JOIN AspNetUserRoles d ON d.RoleId = c.Id and d.UserId=@UserID
		WHERE a.ParentMenuID>0 AND a.ParentMenuID=@MenuID

			UNION ALL
			SELECT a.MenuID,a.DisplayName,a.ParentMenuID, a.OrderNumber, a.MenuURL,a.MenuIcon FROM Menu a	
			INNER JOIN CustomPermission b ON b.MenuID = a.MenuID
			INNER JOIN AspNetUsers c ON c.Id =b.UserID AND c.Id =@UserID
			WHERE a.ParentMenuID>0 AND a.ParentMenuID=@MenuID
			ORDER BY a.OrderNumber
		FETCH NEXT FROM MENU_CURSOR INTO @MenuID, @DisplayName,	@ParentMenuID, @OrderNumber,@MenuURL, @MenuIcon
	END
	SELECT a.MenuID,a.DisplayName,a.ParentMenuID, a.OrderNumber, a.MenuURL,a.MenuIcon FROM MenuTemp a	
	CLOSE MENU_CURSOR
	DEALLOCATE MENU_CURSOR
GO
GO


IF EXISTS(select * from sysobjects where name = 'SP_GetMenu')
BEGIN
	DROP PROCEDURE dbo.SP_GetMenu
	PRINT 'El procedimiento "SP_GetMenu" Fue eliminado correctamente'
END
GO
CREATE PROCEDURE dbo.SP_GetMenu
@UserID NVARCHAR(128),@RoleID NVARCHAR(128)
AS	
	IF @RoleID IS NOT NULL
	BEGIN
		WITH QPermission AS (
			SELECT a.MenuID, a.DisplayName, a.ParentMenuID,b.PermissionID FROM Menu a
			LEFT JOIN Permission b ON b.MenuID = a.MenuID and b.RoleID=@RoleID)
			SELECT q.MenuID, q.DisplayName,q.ParentMenuID,PermissionType=0, Permission= CASE WHEN(ISNULL(q.PermissionID,0))=0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END FROM QPermission q
	END	
	ELSE IF @UserID IS NOT NULL
	BEGIN
		WITH QPermission AS (
			SELECT a.MenuID, a.DisplayName, a.ParentMenuID,b.CustomPermissionID FROM Menu a
			LEFT JOIN CustomPermission b ON b.MenuID = a.MenuID and b.UserID=@UserID)
			SELECT q.MenuID, q.DisplayName,q.ParentMenuID,PermissionType=1, Permission= CASE WHEN(ISNULL(q.CustomPermissionID,0))=0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END FROM QPermission q
	END	
GO
GO



IF EXISTS(select * from sysobjects where name = 'uspPreProSalSheet')
BEGIN
	DROP PROCEDURE dbo.uspPreProSalSheet
	PRINT 'El procedimiento "SP_GetMenu" Fue eliminado correctamente'
END
GO
CREATE PROCEDURE dbo.uspPreProSalSheet
--@UserID NVARCHAR(128),@RoleID NVARCHAR(128)
AS	
	BEGIN
			DECLARE @cols AS NVARCHAR(MAX),
			@query  AS NVARCHAR(MAX)

		CREATE TABLE #SalaryData
		(
		   EmployeeId nvarchar(10),
		   AllowDeductId int,
		   AllowDeductName nvarchar(30),
		   AllowDeductAmt decimal(18,2),
		   TotalSalary decimal(18,2)

		)

		INSERT INTO #SalaryData SELECT EmployeeId ,FA.AllowanceId,(SELECT AllowanceName FROM Allowances WHERE Id=FA.AllowanceId AND AllowanceTypeId=1),AllowanceAmount,AllowanceAmount TotalSalary FROM FixedAllowances FA
		INSERT INTO #SalaryData SELECT EmployeeId,VA.AllowanceId,(SELECT AllowanceName FROM Allowances WHERE Id=VA.AllowanceId AND AllowanceTypeId=2),AllowanceAmount,AllowanceAmount TotalSalary FROM VariableAllowances VA
		INSERT INTO #SalaryData SELECT EmployeeId ,FD.DeductionId,(SELECT DeductionName FROM Deductions WHERE Id=FD.DeductionId AND DeductionTypeId=1),DeductionAmount,-DeductionAmount TotalSalary FROM FixedDeductions FD
		INSERT INTO #SalaryData SELECT EmployeeId ,VD.DeductionId,(SELECT DeductionName FROM Deductions WHERE Id=VD.DeductionId AND DeductionTypeId=2),DeductionAmount,-DeductionAmount TotalSalary FROM VariableDeductions VD

		INSERT INTO #SalaryData
		select EmployeeId,'99999','Total Salary',sum(TotalSalary),sum(TotalSalary) FROM #SalaryData group by EmployeeId



		select @cols = STUFF((SELECT ','+ QUOTENAME(AllowDeductId)
							from #SalaryData 
							group by AllowDeductId
							order by AllowDeductId 
					FOR XML PATH(''), TYPE
					).value('.', 'NVARCHAR(MAX)') 
				,1,1,'')

			--	print @cols

		set @query = N'SELECT EmployeeId,' + @cols + N' from 
					 (
						select EmployeeID,AllowDeductAmt, AllowDeductId				     
						from #SalaryData 			
					) x
					pivot 
					(
						max(AllowDeductAmt)
						for AllowDeductId in (' + @cols + N')
					) p '






		exec sp_executesql @query;

		DROP TABLE #SalaryData
	END	
GO
GO


--DBCC checkident ('Employees', reseed, 1000)
