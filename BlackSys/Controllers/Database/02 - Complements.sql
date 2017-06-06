PRINT 'CREANDO REFERENCIAS...'
USE BlackSysDB;
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_RoleID] FOREIGN KEY(RoleId)
REFERENCES [dbo].[AspNetRoles] (Id);
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_MenuID] FOREIGN KEY(MenuID)
REFERENCES [dbo].[Menu] (MenuId);
ALTER TABLE Permission
 ADD CONSTRAINT UK_Permission UNIQUE (RoleID,MenuID)


ALTER TABLE [dbo].[CustomPermission]  WITH CHECK ADD  CONSTRAINT [FK_CustomPermission_RoleID] FOREIGN KEY(UserID)
REFERENCES [dbo].[AspNetUsers] ([Id]);
ALTER TABLE [dbo].[CustomPermission]  WITH CHECK ADD  CONSTRAINT [FK_CustomPermission_MenuID] FOREIGN KEY(MenuID)
REFERENCES [dbo].[Menu] ([MenuId]);
ALTER TABLE CustomPermission
 ADD CONSTRAINT UK_CustomPermission UNIQUE (UserID,MenuID)
 
 --REFERENCES TABLE COMPONENT
ALTER TABLE [dbo].[Component] WITH CHECK ADD CONSTRAINT [FK_Component_CategoryID] FOREIGN KEY (ComponentCategoryID)
REFERENCES [dbo].[ComponentCategory] (ComponentCategoryID);
ALTER TABLE [dbo].[Component] WITH CHECK ADD CONSTRAINT [FK_Component_UomID] FOREIGN KEY (UomID)
REFERENCES [dbo].[UnitMeasure] (UomID);
ALTER TABLE [dbo].[Component] WITH CHECK ADD CONSTRAINT [FK_Component_SupplierID] FOREIGN KEY (SupplierID)
REFERENCES [dbo].[Supplier] (SupplierID);
ALTER TABLE [dbo].[Component] WITH CHECK ADD CONSTRAINT [FK_Component_CustomerID] FOREIGN KEY (CustomerID)
REFERENCES [dbo].[Customer] (CustomerID);
 
PRINT 'REFERENCIAS CREADAS CORRECTAMENTE...'