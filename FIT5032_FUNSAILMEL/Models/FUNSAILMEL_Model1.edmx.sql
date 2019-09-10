
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/10/2019 20:47:11
-- Generated from EDMX file: D:\IT\Kevin_Git\FIT5032\FIT5032_FUNSAILMEL\FIT5032_FUNSAILMEL\Models\FUNSAILMEL_Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FUNSAILMEL_Database1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BoatOwners'
CREATE TABLE [dbo].[BoatOwners] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [ContactPerson] nvarchar(max)  NOT NULL,
    [ContactPhone] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Boats'
CREATE TABLE [dbo].[Boats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BoatName] nvarchar(max)  NOT NULL,
    [BoatType] nvarchar(max)  NOT NULL,
    [Year] nvarchar(max)  NOT NULL,
    [Colour] nvarchar(max)  NOT NULL,
    [Capacity] nvarchar(max)  NOT NULL,
    [BoatOwnerId] int  NOT NULL,
    [PierId] int  NOT NULL
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [CustomerId] int  NOT NULL,
    [BoatId] int  NOT NULL,
    [Review] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Piers'
CREATE TABLE [dbo].[Piers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PierName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BoatOwners'
ALTER TABLE [dbo].[BoatOwners]
ADD CONSTRAINT [PK_BoatOwners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Boats'
ALTER TABLE [dbo].[Boats]
ADD CONSTRAINT [PK_Boats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Piers'
ALTER TABLE [dbo].[Piers]
ADD CONSTRAINT [PK_Piers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_CustomerBooking]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerBooking'
CREATE INDEX [IX_FK_CustomerBooking]
ON [dbo].[Bookings]
    ([CustomerId]);
GO

-- Creating foreign key on [BoatOwnerId] in table 'Boats'
ALTER TABLE [dbo].[Boats]
ADD CONSTRAINT [FK_BoatBoatOwner]
    FOREIGN KEY ([BoatOwnerId])
    REFERENCES [dbo].[BoatOwners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoatBoatOwner'
CREATE INDEX [IX_FK_BoatBoatOwner]
ON [dbo].[Boats]
    ([BoatOwnerId]);
GO

-- Creating foreign key on [BoatId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_BoatBooking]
    FOREIGN KEY ([BoatId])
    REFERENCES [dbo].[Boats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoatBooking'
CREATE INDEX [IX_FK_BoatBooking]
ON [dbo].[Bookings]
    ([BoatId]);
GO

-- Creating foreign key on [PierId] in table 'Boats'
ALTER TABLE [dbo].[Boats]
ADD CONSTRAINT [FK_BoatPier]
    FOREIGN KEY ([PierId])
    REFERENCES [dbo].[Piers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoatPier'
CREATE INDEX [IX_FK_BoatPier]
ON [dbo].[Boats]
    ([PierId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------