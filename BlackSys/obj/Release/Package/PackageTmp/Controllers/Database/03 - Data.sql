INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('CRM', 0, 1, '#', 'fa fa-briefcase');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Finance', 0, 2, '#', 'fa fa-credit-card');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Engineering', 0, 3, '#', 'fa fa-gears');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Inventory', 0, 4, '#', 'fa fa-building-o');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Logistic', 0, 5, '#', 'fa fa-truck');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Production', 0, 6, '#', 'fa fa-bar-chart-o');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Sales', 0, 7, '#', 'fa fa-tasks');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('System', 0, 8, '#', 'fa fa-gear');

INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Suppliers',      (SELECT MenuID from Menu Where DisplayName='CRM'), 1, '/Suppliers', 'fa fa-user');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Bill Accounts', (SELECT MenuID from Menu Where DisplayName='Finance'), 1, '/Finance/Bills', 'fa fa-credit-card');

INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Products',   (SELECT MenuID from Menu Where DisplayName='Engineering'), 1, '/Engineering/Product', 'fa fa-gears');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Categories', (SELECT MenuID from Menu Where DisplayName='Engineering'), 2, '/Engineering/Categorie', 'fa fa-gears');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Lines',      (SELECT MenuID from Menu Where DisplayName='Engineering'), 3, '/Engineering/Lines', 'fa fa-gears');

INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Invoices', (SELECT MenuID from Menu Where DisplayName='Inventory'), 1, '/Inventory/Invoices', 'fa fa-building-o');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Components', (SELECT MenuID from Menu Where DisplayName='Inventory'), 2, '/Inventory/Component', 'fa fa-building-o');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Component Category', (SELECT MenuID from Menu Where DisplayName='Inventory'), 3, '/Inventory/CategoryIndex', 'fa fa-building-o');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Warehouses', (SELECT MenuID from Menu Where DisplayName='Inventory'), 5, '/Inventory/Wharehouses', 'fa fa-building-o');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Reports', (SELECT MenuID from Menu Where DisplayName='Inventory'), 6, '/Inventory/Reports', 'fa fa-building-o');

INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Purchase Orders', (SELECT MenuID from Menu Where DisplayName='Production'), 1, '/Production/Orders', 'fa fa-bar-chart-o');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Plants', (SELECT MenuID from Menu Where DisplayName='Production'), 1, '/Production/Plants', 'fa fa-bar-chart-o');

INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Invoices', (SELECT MenuID from Menu Where DisplayName='Sales'), 1, '/Sales/Invoices', 'fa fa-tasks');

INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Users', (SELECT MenuID from Menu Where DisplayName='System'), 1, '/AdminUsers/Index', 'fa fa-gear');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Roles', (SELECT MenuID from Menu Where DisplayName='System'), 2, '/AdminRoles/Index', 'fa fa-gear');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Permission', (SELECT MenuID from Menu Where DisplayName='System'), 3, '/Permissions/Index', 'fa fa-gear');
INSERT INTO Menu (DisplayName, ParentMenuID, OrderNumber, MenuURL, MenuIcon) VALUES ('Colors', (SELECT MenuID from Menu Where DisplayName='System'), 4, '/System/ColorIndex', 'fa fa-gear');



INSERT INTO Permission (MenuID, RoleID) VALUES (1, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (2, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (3, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (4, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (5, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (6, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (7, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (8, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (9, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (10, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (11, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (12, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (13, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (14, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (15, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (16, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (17, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (18, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (19, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (20, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (21, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (22, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (23, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (24, (Select Id from AspNetRoles WHERE Name='Administrador'));
INSERT INTO Permission (MenuID, RoleID) VALUES (25, (Select Id from AspNetRoles WHERE Name='Administrador'));