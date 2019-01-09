
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/30/2018 21:47:29
-- Generated from EDMX file: D:\Github项目备份\Subway-ticketing-system\地铁售票系统\db\MyDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AppDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserDetail_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetail] DROP CONSTRAINT [FK_UserDetail_UserInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CompanyInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyInfo];
GO
IF OBJECT_ID(N'[dbo].[ManagerInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManagerInfo];
GO
IF OBJECT_ID(N'[dbo].[UserDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetail];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ManagerInfoes'
CREATE TABLE [dbo].[ManagerInfoes] (
    [Id] char(10)  NOT NULL,
    [Name] varchar(35)  NOT NULL,
    [Password] varchar(35)  NOT NULL
);
GO

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL,
    [UserName] varchar(9)  NOT NULL,
    [StartStation] nvarchar(30)  NOT NULL,
    [EndStation] nvarchar(30)  NOT NULL,
    [TicketsNum] int  NOT NULL,
    [Time] datetime  NOT NULL,
    [Money] decimal(19,4)  NOT NULL,
    [StartTag] int  NOT NULL,
    [EndTag] int  NOT NULL,
    [TicketsState] nvarchar(6)  NOT NULL,
    [LineNumber] nvarchar(6)  NOT NULL
);
GO

-- Creating table 'UserInfoes'
CREATE TABLE [dbo].[UserInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(9)  NOT NULL,
    [Password] varchar(35)  NOT NULL,
    [Name] nvarchar(10)  NOT NULL,
    [Sex] nvarchar(2)  NOT NULL,
    [IdCard] char(18)  NOT NULL,
    [OpenTime] datetime  NOT NULL,
    [Birthday] nvarchar(10)  NOT NULL,
    [MoneySum] decimal(19,4)  NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [PhoneNumber] varchar(11)  NOT NULL
);
GO

-- Creating table 'CompanyInfoes'
CREATE TABLE [dbo].[CompanyInfoes] (
    [CompanyName] nvarchar(30)  NOT NULL,
    [MoneySum] decimal(19,4)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ManagerInfoes'
ALTER TABLE [dbo].[ManagerInfoes]
ADD CONSTRAINT [PK_ManagerInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserName] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [PK_UserInfoes]
    PRIMARY KEY CLUSTERED ([UserName] ASC);
GO

-- Creating primary key on [Id] in table 'CompanyInfoes'
ALTER TABLE [dbo].[CompanyInfoes]
ADD CONSTRAINT [PK_CompanyInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserName] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_UserDetail_UserInfo]
    FOREIGN KEY ([UserName])
    REFERENCES [dbo].[UserInfoes]
        ([UserName])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetail_UserInfo'
CREATE INDEX [IX_FK_UserDetail_UserInfo]
ON [dbo].[UserDetails]
    ([UserName]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------